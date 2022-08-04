using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Input Action script
    private PlayerMovement playerMovement;
    private PlayerMovement.PlayerActions OnMove;

    private PlayerLook playerLook;
    //private PlayerController playerController;

    private void Awake()
    {
        playerMovement = new PlayerMovement();
        OnMove = playerMovement.Player;

        // Assigning script to the componet
        playerLook = GetComponent<PlayerLook>();
        //playerController = GetComponent<PlayerController>();

        //OnMove.Jump.performed += ctx => playerController.Jump();
    }

    private void LateUpdate()
    {
        playerLook.ProcessLook(OnMove.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        OnMove.Enable();
    }

    private void OnDisable()
    {
        OnMove.Disable();
    }
}
