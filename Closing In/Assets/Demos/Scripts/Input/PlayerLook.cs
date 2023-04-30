using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float ySensitivity;
    public float xSensitivity;
    private Animator playerAnimation;

    public Camera cam;

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;

        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        if ((xRotation > 60f) || (xRotation < -60f))
        {
            playerAnimation.speed = 0;
        }
        else
        {
            playerAnimation.speed = 1;
        }

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }


}
