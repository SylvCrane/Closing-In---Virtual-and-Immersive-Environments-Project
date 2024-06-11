using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerLooker playerLooker;
    private PlayerLooker.LookerActions looker;
    private PlayerLook look;

    // Start is called before the first frame update
    void Awake()
    {
        playerLooker = new PlayerLooker();
        looker = playerLooker.Looker;
        look = GetComponent<PlayerLook>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        look.ProcessLook(looker.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        looker.Enable();
    }

    private void OnDisable()
    {
        looker.Disable();
    }
}
