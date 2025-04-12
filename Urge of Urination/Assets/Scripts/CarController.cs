using UnityEngine;
using System.Collections.Generic; // Needed for Lists if using arrays/lists for wheels

[RequireComponent(typeof(Rigidbody))] // Ensure Rigidbody is present
public class CarController : MonoBehaviour
{
    [Header("Car Stats")]
    [Tooltip("Maximum speed the car can reach (visual approximation, physics can overshoot)")]
    public float maxSpeed = 120f; // Example: km/h or mph depending on how you interpret it
    [Tooltip("Maximum angle the front wheels can turn")]
    public float maxSteeringAngle = 30f;
    [Tooltip("How quickly the wheels turn towards the target steering angle")]
    public float steeringSpeed = 5f;
    [Tooltip("The force applied to the drive wheels for acceleration")]
    public float motorForce = 1500f;
    [Tooltip("The force applied when braking")]
    public float brakeForce = 3000f;
    [Tooltip("How much the car slows down naturally (higher = more friction/air resistance)")]
    [Range(0.001f, 0.1f)] public float naturalDeceleration = 0.01f; // Represents drag/friction
/*
    [Header("Wheel Colliders")]
    [Tooltip("Wheel Collider for the front left wheel")]
    public WheelCollider frontLeftWheelCollider;
    [Tooltip("Wheel Collider for the front right wheel")]
    public WheelCollider frontRightWheelCollider;
    [Tooltip("Wheel Collider for the rear left wheel")]
    public WheelCollider rearLeftWheelCollider;
    [Tooltip("Wheel Collider for the rear right wheel")]
    public WheelCollider rearRightWheelCollider;
    // --- OR use arrays (uncomment below and comment out individuals) ---
    // public List<WheelCollider> frontWheels;
    // public List<WheelCollider> rearWheels;

    [Header("Wheel Visuals (Optional)")]
    [Tooltip("Visual mesh/transform for the front left wheel")]
    public Transform frontLeftWheelTransform;
    [Tooltip("Visual mesh/transform for the front right wheel")]
    public Transform frontRightWheelTransform;
    [Tooltip("Visual mesh/transform for the rear left wheel")]
    public Transform rearLeftWheelTransform;
    [Tooltip("Visual mesh/transform for the rear right wheel")]
    public Transform rearRightWheelTransform;
*/
    // --- OR use arrays (uncomment below and comment out individuals) ---
    // public List<Transform> frontWheelTransforms;
    // public List<Transform> rearWheelTransforms;

    [Header("Enter/Exit Logic")]
    [Tooltip("The GameObject representing the player when not in the car")]
    public GameObject playerModel; // Keep this, but movement script doesn't use it
    [Tooltip("Where the player should appear when exiting the car")]
    public GameObject exitPoint;   // Keep this, but movement script doesn't use it

    // Private variables
    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private float currentSteeringAngle;
    private float currentBrakeForce;
    private bool isBraking;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Optional: Lower center of mass for stability
        // rb.centerOfMass = new Vector3(rb.centerOfMass.x, -0.5f, rb.centerOfMass.z); // Adjust Y value as needed
    }

    void Update()
    {
        GetInput();
        // Optional: Update visual wheels here if you prefer slightly less accuracy than FixedUpdate
        // HandleVisualWheels();
    }

    void FixedUpdate() // Physics calculations should happen in FixedUpdate
    {
        HandleMotor();
        HandleSteering();
        ApplyBraking();
        ApplyNaturalDeceleration(); // Apply drag/friction
        //UpdateVisualWheels();      // Update visual wheels based on physics state
        LimitSpeed();             // Optional: Add a hard speed limit if needed
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        verticalInput = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow

        // Check for braking input (e.g., Space bar)
        isBraking = Input.GetKey(KeyCode.Space);
    }

    void HandleMotor()
    {
        // Apply motor torque to drive wheels (Rear wheel drive example)
        // For AWD, apply to front wheels too. For FWD, apply only to front.
        float targetMotorTorque = verticalInput * motorForce;

        //rearLeftWheelCollider.motorTorque = targetMotorTorque;
       // rearRightWheelCollider.motorTorque = targetMotorTorque;

        // --- If using arrays ---
        // foreach (var wheel in rearWheels) { wheel.motorTorque = targetMotorTorque; }
    }

    void HandleSteering()
    {
        // Calculate the target steering angle
        float targetSteeringAngle = maxSteeringAngle * horizontalInput;

        // Smoothly interpolate towards the target angle
        currentSteeringAngle = Mathf.Lerp(currentSteeringAngle, targetSteeringAngle, Time.fixedDeltaTime * steeringSpeed);

        // Apply steering angle to front wheels
        //frontLeftWheelCollider.steerAngle = currentSteeringAngle;
        //frontRightWheelCollider.steerAngle = currentSteeringAngle;

        // --- If using arrays ---
        // foreach (var wheel in frontWheels) { wheel.steerAngle = currentSteeringAngle; }
    }

    void ApplyBraking()
    {
        // Apply brake force if Space is pressed OR if vertical input is opposite to current direction
        bool applyBrakes = isBraking ||
                           (verticalInput < -0.1f && rb.velocity.z > 0.1f) || // Braking while moving forward
                           (verticalInput > 0.1f && rb.velocity.z < -0.1f); // Braking while moving backward (if reversing counts as braking)


        currentBrakeForce = applyBrakes ? brakeForce : 0f;

        // Apply brake force to all wheels
        /*frontLeftWheelCollider.brakeTorque = currentBrakeForce;
        frontRightWheelCollider.brakeTorque = currentBrakeForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;*/

        // --- If using arrays ---
        // float brakeForcePerWheel = currentBrakeForce / (frontWheels.Count + rearWheels.Count);
        // foreach (var wheel in frontWheels) { wheel.brakeTorque = brakeForcePerWheel; }
        // foreach (var wheel in rearWheels) { wheel.brakeTorque = brakeForcePerWheel; }

        // If braking, reduce motor torque significantly or zero it out
        if (applyBrakes)
        {
            /*rearLeftWheelCollider.motorTorque = 0;
            rearRightWheelCollider.motorTorque = 0;*/
            // --- If using arrays ---
            // foreach (var wheel in rearWheels) { wheel.motorTorque = 0; }
        }
    }

    void ApplyNaturalDeceleration()
    {
        // Apply drag only when not accelerating hard and not braking hard
        if (Mathf.Abs(verticalInput) < 0.1f && !isBraking)
        {
            rb.velocity *= (1.0f - naturalDeceleration); // Reduce velocity slightly each physics step
        }
    }


    void UpdateVisualWheels()
    {
        // Update individual wheels
        /*UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);*/

        // --- OR ---

        // --- If using arrays ---
        // UpdateWheelList(frontWheels, frontWheelTransforms);
        // UpdateWheelList(rearWheels, rearWheelTransforms);
    }

    // Updates the position and rotation of a single wheel mesh to match the WheelCollider
    void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        if (wheelTransform == null) return; // Skip if no visual transform assigned

        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot); // Get current position and rotation from collider
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    // --- Helper if using arrays ---
    // void UpdateWheelList(List<WheelCollider> colliders, List<Transform> transforms)
    // {
    //     if (colliders.Count != transforms.Count)
    //     {
    //         Debug.LogWarning("Wheel Collider and Transform lists do not match in size!");
    //         return;
    //     }

    //     for(int i = 0; i < colliders.Count; i++)
    //     {
    //         UpdateSingleWheel(colliders[i], transforms[i]);
    //     }
    // }

    // Optional: Add a hard speed limit
    void LimitSpeed()
    {
        float currentSpeed = rb.velocity.magnitude * 3.6f; // Convert m/s to km/h (use 2.237 for mph)
        if (currentSpeed > maxSpeed)
        {
            // Simple approach: Scale down velocity
            rb.velocity = rb.velocity.normalized * (maxSpeed / 3.6f);
            // Note: More complex physics might require adjusting motor torque instead
        }
    }

    //--- The minSpeed variable isn't used here as its purpose wasn't clear ---
    // If it was meant as a minimum threshold for movement or an idle speed,
    // the logic would need to be added in HandleMotor or ApplyNaturalDeceleration.
    // If it's a max reverse speed, add logic in LimitSpeed or HandleMotor.
}