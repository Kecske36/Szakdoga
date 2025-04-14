using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public static float mouseSensitivity; // Controls how fast the camera moves

    public Transform playerBody; // Assign the parent Player object (the one that moves)

    private float xRotation = 0f; // Stores the current vertical rotation (pitch)

    void Start()
    {
        if (mouseSensitivity < 1 || mouseSensitivity > 120)
        {
            mouseSensitivity = 100;
        }
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; // Optional: explicitly hide cursor icon
    }

    // Use LateUpdate for cameras to ensure player movement has already happened
    void LateUpdate()
    {
        if(!EventSystem.pauseMenuActive)
        {
            // 1. Get Mouse Input (Delta)
            // Input.GetAxis returns the mouse movement since the last frame
            // Multiply by sensitivity and time delta for frame-rate independence
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;
            //Debug.Log($"MouseX Input: {Input.GetAxis("Mouse X")}, Calculated Yaw Delta: {mouseX}"); // Add this line

            // 2. Calculate Pitch (Vertical Rotation - Up/Down)
            // Subtract mouseY because positive Y input means mouse moved up,
            // but we need negative rotation around the X-axis to look up.
            xRotation -= mouseY;

            // Clamp the vertical rotation to prevent looking too far up or down (e.g., flipping over)
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp between -90 and +90 degrees

            // 3. Apply Pitch Rotation to the Camera
            // We only rotate the camera up/down locally.
            // Use Quaternion.Euler to create the rotation from the angle.
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // 4. Apply Yaw Rotation (Horizontal Rotation - Left/Right) to the Player Body
            // Rotate the entire player body around the world's Y-axis.
            // This makes the player turn left/right.
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void ReSync()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        playerBody.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
