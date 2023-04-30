//-----------------------------------------------------------------------
// <copyright file="Api.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

namespace Google.XR.Cardboard
{
    using System;
    using System.Runtime.InteropServices;
    using UnityEngine;
	using UnityEngine.InputSystem;
	using UnityEngine.InputSystem.Controls;

	/// <summary>
	/// Cardboard XR Plugin API.
	/// </summary>
	public static class Api
    {
        private static int _deviceParamsCount = -1;
        private static ScreenOrientation _cachedScreenOrientation;
		private static bool _initWarningIssued = false;


        /// <summary>
		/// Gets the touch state.
		/// </summary>
		public static bool IsTouchscreenPressed()
		{
			return (Touchscreen.current != null) && Touchscreen.current.press.IsPressed();
		}

		/// <summary>
		/// Gets the touch position as Vector2Int.
		/// </summary>
		public static Vector2Int GetTouchPosition()
		{
			TouchControl touch = Touchscreen.current.primaryTouch;
			Vector2 touchPosition = new Vector2(touch.position.x.ReadValue(), touch.position.y.ReadValue());
			return Vector2Int.RoundToInt(touchPosition);
		}

		/// <summary>
        /// Gets a value indicating whether the close button is pressed this frame.
        /// </summary>
        public static bool IsCloseButtonPressed
        {
            get
            {
				return XRLoader._isStarted 
					&& IsTouchscreenPressed() 
					&& Widget.CloseButtonRect.Contains(GetTouchPosition());
            }
        }

        /// <summary>
        /// Gets a value indicating whether the gear button is pressed this frame.
        /// </summary>
        public static bool IsGearButtonPressed
        {
            get
            {
				return XRLoader._isStarted 
					&& IsTouchscreenPressed()
					&& Widget.GearButtonRect.Contains(GetTouchPosition());
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Cardboard trigger button is pressed this frame.
        /// </summary>
        public static bool IsTriggerPressed
        {
            get
            {
				if (!XRLoader._isStarted || !IsTouchscreenPressed())
                {
                    return false;
                }
				Vector2Int touchPosition = GetTouchPosition();
				return !Widget.CloseButtonRect.Contains(touchPosition)
                    && !Widget.GearButtonRect.Contains(touchPosition);
            }
        }


        /// <summary>
        /// Evaluates whether or not device params are saved in the storage.
        /// </summary>
        ///
        /// <returns>Whether or not device parameters are found.</returns>
        public static bool HasDeviceParams()
        {
            if (!XRLoader._isInitialized)
            {
                return false;
            }

            IntPtr encodedDeviceParams;
            int size;
            CardboardQrCode_getSavedDeviceParams(out encodedDeviceParams, out size);
            if (size == 0)
            {
                Debug.Log("[CardboardApi] No device params found.");
                return false;
            }

            Debug.Log("[CardboardApi] Device params found.");
            CardboardQrCode_destroy(encodedDeviceParams);
            _deviceParamsCount = CardboardQrCode_getDeviceParamsChangedCount();
            return true;
        }

        /// <summary>
        /// Starts QR Code scanning activity.
        /// </summary>
        public static void ScanDeviceParams()
        {
            if (!XRLoader._isInitialized)
            {
                return;
            }

            _deviceParamsCount = CardboardQrCode_getDeviceParamsChangedCount();
            Debug.Log("[CardboardApi] QR Code scanning activity launched.");
            CardboardQrCode_scanQrCodeAndSaveDeviceParams();
        }

        /// <summary>
        /// Saves the encoded device parameters provided by an URI.
        ///
        /// Expected URI format for:
        ///     - Cardboard Viewer v1: https://g.co/cardboard
        ///     - Cardboard Viewer v2: https://google.com/cardboard/cfd?p=deviceParams (for example,
        ///       https://google.com/cardboard/cfg?p=CgZHb29nbGUSEkNhcmRib2FyZCBJL08gMjAxNR0rGBU9JQHegj0qEAAASEIAAEhCAABIQgAASEJYADUpXA89OggeZnc-Ej6aPlAAYAM).
        ///
        /// Redirection is also supported up to a maximum of 5 possible redirects before reaching
        /// the proper pattern.
        ///
        /// Only URIs using HTTPS protocol are supported.
        /// </summary>
        ///
        /// <param name="uri">
        /// The URI string. See above for supported formats.
        /// </param>
        public static void SaveDeviceParams(string uri)
        {
            if (!XRLoader._isInitialized)
            {
                Debug.LogError(
                        "Please initialize Cardboard XR loader before calling this function.");
                return;
            }

            IntPtr rawUri = Marshal.StringToHGlobalAuto(uri);
            CardboardQrCode_saveDeviceParams(rawUri, uri.Length);
            Marshal.FreeHGlobal(rawUri);
        }

        /// <summary>
        /// Evaluates if device parameters changed from last time they were reloaded.
        /// </summary>
        ///
        /// <returns>true when device parameters changed.</returns>
        public static bool HasNewDeviceParams()
        {
            // TODO(b/156501367):  Move this logic to the XR display provider.
            if (!XRLoader._isInitialized || _deviceParamsCount == -1)
            {
                return false;
            }

            return _deviceParamsCount != CardboardQrCode_getDeviceParamsChangedCount();
        }

        /// <summary>
        /// Enables device parameter reconfiguration on next frame update.
        /// </summary>
        public static void ReloadDeviceParams()
        {
            if (!XRLoader._isInitialized)
            {
                return;
            }

            // TODO(b/156501367):  Move this logic to the XR display provider.
            Debug.Log("[CardboardApi] Reload device parameters.");
            _deviceParamsCount = CardboardQrCode_getDeviceParamsChangedCount();
            CardboardUnity_setDeviceParametersChanged();
        }

        /// <summary>
        /// Updates screen parameters. This method must be called at framerate to ensure the current
        /// screen orientation is properly taken into account by the head tracker.
        /// </summary>
        public static void UpdateScreenParams()
        {
            if (!XRLoader._isInitialized)
            {
				if (!_initWarningIssued)
				{
					Debug.LogError("Please initialize Cardboard XR loader before calling this function.");
					_initWarningIssued = true;
				}
                return;
            }

            // Only set viewport orientation if it has changed since the last check.
            if (_cachedScreenOrientation != Screen.orientation)
            {
                _cachedScreenOrientation = Screen.orientation;
                XRLoader.SetViewportOrientation(_cachedScreenOrientation);
                XRLoader.RecalculateRectangles(Screen.safeArea);
                ReloadDeviceParams();
            }
        }

        /// <summary>
        /// Recenters the head tracker.
        /// </summary>
        public static void Recenter()
        {
            if (!XRLoader._isInitialized)
            {
                Debug.LogError(
                        "Please initialize Cardboard XR loader before calling this function.");
                return;
            }

            CardboardUnity_recenterHeadTracker();
        }

        [DllImport(ApiConstants.CardboardApi)]
        private static extern void CardboardQrCode_scanQrCodeAndSaveDeviceParams();

        [DllImport(ApiConstants.CardboardApi)]
        private static extern void CardboardQrCode_saveDeviceParams(
                IntPtr uri, int size);

        [DllImport(ApiConstants.CardboardApi)]
        private static extern void CardboardQrCode_getSavedDeviceParams(
                out IntPtr encodedDeviceParams, out int size);

        [DllImport(ApiConstants.CardboardApi)]
        private static extern void CardboardQrCode_destroy(IntPtr encodedDeviceParams);

        [DllImport(ApiConstants.CardboardApi)]
        private static extern int CardboardQrCode_getDeviceParamsChangedCount();

        [DllImport(ApiConstants.CardboardApi)]
        private static extern void CardboardUnity_setDeviceParametersChanged();

        [DllImport(ApiConstants.CardboardApi)]
        private static extern void CardboardUnity_recenterHeadTracker();
    }
}
