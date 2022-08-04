using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{ 
    Vector3 movementInput = Vector3.zero;

    public float moveSpeed;

    public float JumpHeight;

    private bool canJump;

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 moveVector = Vector3.zero;

        // Add the forward direction of the player multiplied by the user's up/down input.
        moveVector += transform.forward * movementInput.y;

        // Add the right direction of the player multiplied by the user's right/left input.
        moveVector += transform.right * movementInput.x;

        // Apply the movement vector multiplied by movement speed to the player's position.
        transform.position += moveVector * moveSpeed * Time.deltaTime; 
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }

        else if (collision.gameObject.tag == "Platform") 
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
        }

        else if (collision.gameObject.tag == "Platform")
        {
            canJump = false;
        }
    }

    void OnMove(InputValue moveValue)
    {
        movementInput = moveValue.Get<Vector2>();
    }

    void OnJump(InputValue jumpValue)
    {
        if (canJump)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * JumpHeight, ForceMode.Impulse);
            canJump = false;
        }
    }
}