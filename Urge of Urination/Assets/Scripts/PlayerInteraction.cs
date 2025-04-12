using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Assign these in the Inspector
    public MonoBehaviour playerMovementScript; // Drag your player movement script component here
    public MonoBehaviour mouseLookScript;      // Drag your MouseLook script component here
    public Camera playerCamera;               // Drag your first-person camera here

    // You might also need to add references to other player-specific things
    // like UI elements, weapon scripts etc. if they should be disabled.

    /*public void SetPlayerControl(bool enabled) // OLD
    {
        if (playerMovementScript != null) playerMovementScript.enabled = enabled;
        if (mouseLookScript != null) mouseLookScript.enabled = enabled;
        if (playerCamera != null) playerCamera.gameObject.SetActive(enabled); // Enable/disable camera GO
        // --- Add lines here to disable/enable other player components ---
        // Example: if (weaponScript != null) weaponScript.enabled = enabled;

        // Optional: Lock/Unlock cursor based on state
        if (enabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        // No else here, as cursor might be handled by the car or a pause menu
    }*/
    public void SetPlayerControl(bool enabled)
    {
        if (playerMovementScript != null) playerMovementScript.enabled = enabled;
        if (mouseLookScript != null) mouseLookScript.enabled = enabled;
        if (playerCamera != null) playerCamera.gameObject.SetActive(enabled);
        //if (characterController != null) characterController.enabled = enabled;

        // --- Add lines here to disable/enable other player components ---

        if (enabled)
        {
            // **** THE FIX: Reset Camera's Local Rotation ****
            // Ensures the camera is looking perfectly straight forward
            // relative to the player body *immediately* upon enabling.
            if (playerCamera != null)
            {
                // Option A: Reset completely (simplest, resets pitch momentarily)
                playerCamera.transform.localRotation = Quaternion.identity;

                // Option B: Try to preserve pitch (requires accessing xRotation from MouseLook)
                // if (mouseLookScript != null) {
                //     // Assuming you add a public getter `CurrentPitch` to MouseLook returning xRotation
                //     float currentPitch = mouseLookScript.CurrentPitch;
                //     playerCamera.transform.localRotation = Quaternion.Euler(currentPitch, 0, 0);
                // } else {
                //     // Fallback if MouseLook script not accessible
                //     playerCamera.transform.localRotation = Quaternion.identity;
                // }
            }

            // Reset MouseLook's internal pitch if desired (Optional)
            // If using Option A above, MouseLook might need to reset its xRotation too,
            // or it might re-apply the old pitch on the next frame, causing a slight jump.
            // You could add a ResetPitch() method to MouseLook.
            // if (mouseLookScript != null) mouseLookScript.ResetPitch();


            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        // No else for cursor lock here
    }

}