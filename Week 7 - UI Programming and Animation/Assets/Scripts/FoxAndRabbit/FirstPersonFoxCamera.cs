using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonFoxCamera : MonoBehaviour
{
    // The camera attached to the player
    public Camera playerCamera;

    // Container variables for the mouse delta values each frame
    private float deltaX;
    private float deltaY;

    // Container variables for the player's rotation around the X and Y axis
    private float xRot; // rotation around the x-axis in degrees
    private float yRot; // rotation around the y-axis in degrees

    // mouse sensitivity
    public float sensitivity = 5f;

    void Start()
    {
        //playerCamera = Camera.main;                 // set player camera, if we had multiple cameras use gameObject.GetComponent<Camera>();
        Cursor.visible = false;                     // hide the cursor
        Cursor.lockState = CursorLockMode.Locked;   // lock the cursor in place
    }

    void Update()
    {
        // Because taken care of in PauseMenu.cs
        //LockAndUnlockCursor();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Keep track of the player's x and y rotation
            yRot += deltaX * sensitivity * Time.deltaTime;
            xRot -= deltaY * sensitivity * Time.deltaTime;

            // Keep the player's x rotation clamped [-90, 90] degrees
            xRot = Mathf.Clamp(xRot, -90f, 90f);

            // rotate the camera around the x-axis
            playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0, 0);
            // rotate the player around the y-axis
            transform.rotation = Quaternion.Euler(0, yRot, 0);
        }
    }

    // OnCameraLook event handler
    public void OnCameraLook(InputValue value)
    {
        // Reading the mouse deltas as a vector 2 (delta X is the x component and delta Y is the y component)
        Vector2 inputVector = value.Get<Vector2>();
        deltaX = inputVector.x;
        deltaY = inputVector.y;
    }

    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    } // lock and unlock cursor
}
