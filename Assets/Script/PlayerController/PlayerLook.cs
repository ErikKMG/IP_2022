using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public Camera PlayerCamera;

    private float xRotation;

    // Sensitivity of Player Rotation
    public float xSen;
    public float ySen;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ProcessLook(Vector2 input) 
    {
        float mouseX = input.x;
        float mouseY = input.y;

        // Calculate camera roatation for looking UP and DOWN
        xRotation -= mouseY * Time.deltaTime * ySen;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // Apply this to the camera transform
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0 ,0);

        // Rotate player to look Left and Right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSen);
    }
}
