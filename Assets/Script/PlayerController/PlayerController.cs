using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{ 
    Vector3 movementInput = Vector3.zero;

    public float moveSpeed;

    public float JumpHeight;

    private bool canJump;

    bool interact = false;
    bool one = false;
    bool two = false;
    bool three = false;

    // Raycast Interaction Distance
    public float interactionDistance;

    private PlayerLook playerlook;
    private Pistol pistol;
    private AK ak;

    //Weapon Inventory
    public GameObject PistolTaken;
    public GameObject AK47Taken;
    public GameObject M4Taken;

    //Weapon Inventory UI
    public TextMeshProUGUI PistolUI;
    public TextMeshProUGUI AK47UI;
    public TextMeshProUGUI M4UI;

    //Pistol HUD
    public TextMeshProUGUI PistolBulletCount;
    public TextMeshProUGUI PistolTotalAmmo;
    public TextMeshProUGUI PistolSlice;

    //AK HUD
    public TextMeshProUGUI AKBulletCount;
    public TextMeshProUGUI AKTotalAmmo;
    public TextMeshProUGUI AKSlice;

    // M4 HUD
    public TextMeshProUGUI M4BulletCount;
    public TextMeshProUGUI M4TotalAmmo;
    public TextMeshProUGUI M4Slice;

    private void Update()
    {
        Movement();
        Raycasting();
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

    private void Raycasting()
    {
        playerlook = GetComponent<PlayerLook>();

        Camera playerCamera = playerlook.PlayerCamera;
        // Draw a line that mimics the raycast. For debugging purposes
        Debug.DrawLine(playerCamera.transform.position, playerCamera.transform.position + (playerCamera.transform.forward * interactionDistance));

        // Create local RaycastHit variable to store the raycast information.
        RaycastHit hitInfo;

        //Check if the ray hits any object 
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, interactionDistance))
        {
            // Print the name of the object hit. For debugging purposes.
            Debug.Log(hitInfo.transform.name);

            if (hitInfo.transform.tag == "Pistol")
            {
                if (interact)
                {
                    Debug.Log("Pistol Taken");
                    hitInfo.transform.gameObject.SetActive(false);
                    PistolTaken.SetActive(true);
                    PistolUI.gameObject.SetActive(true);
                    PistolBulletCount.gameObject.SetActive(true);
                    PistolTotalAmmo.gameObject.SetActive(true);
                    PistolSlice.gameObject.SetActive(true);

                    AK47Taken.gameObject.SetActive(false);
                    AKBulletCount.gameObject.SetActive(false);
                    AKTotalAmmo.gameObject.SetActive(false);
                    AKSlice.gameObject.SetActive(false);

                    M4Taken.gameObject.SetActive(false);
                    M4BulletCount.gameObject.SetActive(false);
                    M4TotalAmmo.gameObject.SetActive(false);
                    M4Slice.gameObject.SetActive(false);
                }
            }
            if (hitInfo.transform.tag == "AK-47")
            {
                if (interact)
                {
                    Debug.Log("AK-47 Taken");
                    hitInfo.transform.gameObject.SetActive(false);
                    AK47Taken.SetActive(true);
                    AK47UI.gameObject.SetActive(true);
                    AKBulletCount.gameObject.SetActive(true);
                    AKTotalAmmo.gameObject.SetActive(true);
                    AKSlice.gameObject.SetActive(true);

                    PistolTaken.gameObject.SetActive(false);
                    PistolBulletCount.gameObject.SetActive(false);
                    PistolTotalAmmo.gameObject.SetActive(false);
                    PistolSlice.gameObject.SetActive(false);

                    M4Taken.gameObject.SetActive(false);
                    M4BulletCount.gameObject.SetActive(false);
                    M4TotalAmmo.gameObject.SetActive(false);
                    M4Slice.gameObject.SetActive(false);
                }
            }

            if (hitInfo.transform.tag == "M4")
            {
                if (interact)
                {
                    Debug.Log("M4 Taken");
                    hitInfo.transform.gameObject.SetActive(false);
                    M4Taken.SetActive(true);
                    M4UI.gameObject.SetActive(true);
                    M4BulletCount.gameObject.SetActive(true);
                    M4TotalAmmo.gameObject.SetActive(true);
                    M4Slice.gameObject.SetActive(true);

                    AK47Taken.gameObject.SetActive(false);
                    AKBulletCount.gameObject.SetActive(false);
                    AKTotalAmmo.gameObject.SetActive(false);
                    AKSlice.gameObject.SetActive(false);

                    PistolTaken.gameObject.SetActive(false);
                    PistolBulletCount.gameObject.SetActive(false);
                    PistolTotalAmmo.gameObject.SetActive(false);
                    PistolSlice.gameObject.SetActive(false);
                }
            }

            if (hitInfo.transform.tag == "Crate")
            {
                if (interact)
                {
                    if (AK47Taken.activeInHierarchy == true)
                    {
                        hitInfo.transform.GetComponent<AKCrate>().Refill(ak);
                    }
                    else if (PistolTaken.activeInHierarchy == true)
                    {
                        hitInfo.transform.GetComponent<PistolCrate>().Refill(pistol);
                    }
                    else if (M4Taken.activeInHierarchy == true)
                    {
                        //hitInfo.transform.GetComponent<M4Crate>().Refill(m4);
                    }
                }
            }
        }
        interact = false;
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

    void OnTake(InputValue takeValue)
    {
        interact = true;
    }


    void OnPistol(InputValue PistolValue)
    {
        one = true;
        if (PistolTaken.activeInHierarchy == true)
        {
            if (one)
            {
                // Show Pistol
                PistolTaken.SetActive(true);
                PistolBulletCount.gameObject.SetActive(true);
                PistolTotalAmmo.gameObject.SetActive(true);
                PistolSlice.gameObject.SetActive(true);

                // Hide Weapon
                AK47Taken.SetActive(false);
                AKBulletCount.gameObject.SetActive(false);
                AKTotalAmmo.gameObject.SetActive(false);
                AKSlice.gameObject.SetActive(false);

                M4Taken.SetActive(false);
                M4BulletCount.gameObject.SetActive(false);
                M4TotalAmmo.gameObject.SetActive(false);
                M4Slice.gameObject.SetActive(false);
            }
        }
    }

    void OnAK47(InputValue AKValue)
    {
        two = true;
        if (AK47Taken.activeInHierarchy == true)
        {
            if(two)
            {
                // Show AK
                AK47Taken.SetActive(true);
                AKBulletCount.gameObject.SetActive(true);
                AKTotalAmmo.gameObject.SetActive(true);
                AKSlice.gameObject.SetActive(true);

                // Hide Weapon
                PistolTaken.SetActive(false);
                PistolBulletCount.gameObject.SetActive(false);
                PistolTotalAmmo.gameObject.SetActive(false);
                PistolSlice.gameObject.SetActive(false);

                M4Taken.SetActive(false);
                M4BulletCount.gameObject.SetActive(false);
                M4TotalAmmo.gameObject.SetActive(false);
                M4Slice.gameObject.SetActive(false);
            }
        }
    }

    void OnM4(InputValue M4Value)
    {
        three = true;
        if(M4Taken.activeInHierarchy == true)
        {
            if (three)
            {
                // Show M4
                M4Taken.SetActive(true);
                M4BulletCount.gameObject.SetActive(true);
                M4TotalAmmo.gameObject.SetActive(true);
                M4Slice.gameObject.SetActive(true);

                // Hide Weapon
                PistolTaken.SetActive(false);
                PistolBulletCount.gameObject.SetActive(false);
                PistolTotalAmmo.gameObject.SetActive(false);
                PistolSlice.gameObject.SetActive(false);

                AK47Taken.SetActive(false);
                AKBulletCount.gameObject.SetActive(false);
                AKTotalAmmo.gameObject.SetActive(false);
                AKSlice.gameObject.SetActive(false);
            }
        }
    }
}