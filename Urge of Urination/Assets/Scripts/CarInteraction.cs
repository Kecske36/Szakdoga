using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

[RequireComponent(typeof(CarController))] // Ensure CarController is present
[RequireComponent(typeof(Collider))]     // Ensure a Collider is present for the trigger
public class CarInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("The key to press to enter/exit the car")]
    public KeyCode interactionKey = KeyCode.E;
    [Tooltip("Tag of the player GameObject")]
    public LayerMask playerTag = 6;

    [Header("References")]
    [Tooltip("The camera used when driving the car. Assign in Inspector.")]
    public Camera carCamera;
    [Tooltip("The point where the player should appear when exiting. Assign in Inspector.")]
    public Transform exitPoint; // Use the 'exitPoint' GameObject from CarController
    public static Transform exitPointStatic;
    public GameObject playerObject; // Reference to the player when nearby
    public static GameObject playerObjectStatic;
    // Private variables
    public CarController carController;
    public PlayerInteraction playerInteraction; // Reference to player's interaction script
    public static PlayerInteraction playerInteractionStatic;
    public static bool isPlayerInsideStatic = false;
    public bool isPlayerInside = false;
    public bool playerNearby = false;
    public static bool playerNearbyStatic = false;
    static bool exitcar;
    void Start()
    {
        //playerObject = GameObject.FindWithTag(playerTag);
        exitPointStatic = exitPoint;
        playerObjectStatic = playerObject;
        playerInteractionStatic = playerInteraction;
    }

    void Awake()
    {
        carController = GetComponent<CarController>();

        // Ensure collider is trigger
        Collider col = GetComponent<Collider>();
        if (!col.isTrigger)
        {
            Debug.LogWarning($"Collider on {gameObject.name} needs 'Is Trigger' enabled for CarInteraction.", this);
            // Optionally force it: col.isTrigger = true;
        }

        // Start with car disabled
        SetCarControl(false);
    }

    void Update()
    {
//        Debug.Log(exitcar);
        if(exitcar)
        {
            playerInteraction = playerInteractionStatic.GetComponent<PlayerInteraction>();
            SetCarControl(false);
            carCamera.gameObject.SetActive(false);
        }
//        Debug.Log($"isPlayerInside: {isPlayerInside}\n" +
//            $"playerNearby: {playerNearby}\n"+
//            $"playerObject: {playerObject.name}");
        // Check for interaction key press
        if (Vector3.Distance(this.transform.position, playerObject.transform.position) < 4.0f)
        {

            playerNearby = true;
            playerNearbyStatic = true;
        }
        else
        {
            playerNearby = false;
            playerNearbyStatic = false;
        }
//        Debug.Log(Vector3.Distance(transform.position, playerObject.transform.position));
        if (Input.GetKeyDown(interactionKey))
        {
            if (!isPlayerInside && playerNearby)
            {
                isPlayerInside = true;
                isPlayerInsideStatic = true;
                EnterCar();
            }
        }
    }

    // Called when an object enters the trigger zone
   /* void OnTriggerEnter(Collider other)
    {        
        if (!isPlayerInside && other.CompareTag(playerTag.ToString()))
        {
            // Player entered the zone
            playerObject = other.gameObject;
            playerInteraction = playerObject.GetComponent<PlayerInteraction>();
            if (playerInteraction != null)
            {
                playerNearby = true;
                // Optional: Show UI prompt like "Press E to Enter"
                Debug.Log("Player nearby, press " + interactionKey + " to enter.");
            }
            else
            {
                Debug.LogWarning($"Object tagged '{playerTag}' entered trigger but lacks PlayerInteraction script.", other.gameObject);
                playerObject = null; // Clear reference if script is missing
            }
        }
    }

    // Called when an object exits the trigger zone
    void OnTriggerExit(Collider other)
    {
        if (!isPlayerInside && other.CompareTag(playerTag.ToString()) && other.gameObject == playerObject)
        {
            // Player left the zone
            playerNearby = false;
            playerObject = null;
            playerInteraction = null;
            // Optional: Hide UI prompt
            Debug.Log("Player moved away.");
        }
    }*/

    void EnterCar()
    {
        if (playerInteraction == null)
        {
            Debug.Log("WATAFAK HAPPENED!");
            return; 
        } // Should not happen if logic is correct, but safety check

        if(playerNearbyStatic)
        {
            Debug.Log("Entering Car");
            isPlayerInside = true;
            isPlayerInsideStatic = true;
            playerNearby = false; // Player is no longer "nearby", they are "inside"

            // Disable Player
            playerInteraction.SetPlayerControl(false);
            playerObject.SetActive(false); // Also deactivate the whole player object

            // Enable Car
            SetCarControl(true);

            // Clear references (we'll find player again on exit if needed, or keep if preferred)
            // playerObject = null;
            // playerInteraction = null;
        }
    }

    public static void ExitCar()
    {

        Debug.Log("Exiting Car");
        isPlayerInsideStatic = false;

        // Enable Player
        // Position player first, then activate/enable components
        if (exitPointStatic != null)
        {
            playerObjectStatic.transform.position = exitPointStatic.position;
            playerObjectStatic.transform.rotation = exitPointStatic.rotation;
        }

        playerObjectStatic.SetActive(true); // Reactivate the player object first
        playerInteractionStatic.SetPlayerControl(true); // Then enable controls

        playerObjectStatic.GetComponentInChildren<Camera>().GetComponent<MouseLook>().ReSync();
        exitcar = true;
    }

    // Helper to enable/disable car components
    void SetCarControl(bool enabled)
    {
        if (carController != null) carController.enabled = enabled;
        if (carCamera != null) carCamera.gameObject.SetActive(enabled); // Assumes camera is a GameObject

        // If you have other car-specific scripts to toggle, do it here
        // e.g., carAudioSource.enabled = enabled;

        // Reset car physics state if disabling? Optional.
        if (!enabled && carController != null)
        {
            Rigidbody rb = carController.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            // Reset wheel colliders? Might be needed depending on CarController setup
            // carController.ResetWheels(); // Add a ResetWheels function to CarController if needed
        }
    }
}