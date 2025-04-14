using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    // --- FRONT WHEELS ---
    public GameObject wheel_FL;
    public GameObject wheel_FR;

    // --- BACK WHEELS ---
    public GameObject wheel_RL;
    public GameObject wheel_RR;

    public float rotationTimeBase;

    public float steeringAngle = 30f; // Left -1 Right +1
    public float steeringSpeed;

    public bool isBraking;
    public bool isSteering;

    public Quaternion _targetRot;
    private Rigidbody rb;
    public float currentSpeed;
    public LayerMask whatIsGround;
    public float groundDrag;
    // Start is called before the first frame update
    private void Awake()
    {
        _targetRot = transform.rotation;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (steeringAngle > 30)
        {
            steeringAngle = 30;
        }
        if (rotationTimeBase > 360)
        {
            rotationTimeBase = 360;
        }
    }
    
    void Update()
    {
        if(!EventSystem.pauseMenuActive && CarInteraction.isPlayerInsideStatic)
        {
            CarMove();
            WheelSteer();
        }
    }
    void CarMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
//        Debug.Log($"X: {x}\tZ: {z}");
        Vector3 inputDirection = new Vector3(0, 0, z);
        inputDirection = Vector3.ClampMagnitude(inputDirection, 1f);
        //= Input.GetKeyDown(sprintKey) ? moveSpeed : sprintSpeed;
        currentSpeed = z != 0 ? 60 : 0;

        Vector3 moveDirection = transform.TransformDirection(inputDirection);
        Vector3 horintalVelocity = moveDirection * currentSpeed;
        //characterController.Move(moveDirection * Time.deltaTime);
        rb.MovePosition(transform.position + horintalVelocity * Time.deltaTime);
        rb.MoveRotation(transform.rotation * Quaternion.Euler(Vector2.up * x));
    }
    void WheelSteer()
    {
            //wheel_FL.transform.rotation.Set(0, steeringAngle*-1, 0, 0);
            if (Input.GetAxis("Horizontal") < 0 && wheel_FL.transform.rotation.y <= 0)
            {
                wheel_FL.transform.rotation = wheel_FL.transform.parent.rotation * Quaternion.Euler(0, steeringAngle * Input.GetAxis("Horizontal"), 0);
                wheel_FR.transform.rotation = wheel_FR.transform.parent.rotation * Quaternion.Euler(0, steeringAngle * Input.GetAxis("Horizontal"), 0);
            }
            else if (Input.GetAxis("Horizontal") > 0 && wheel_FL.transform.rotation.y >= 0)
            {
                wheel_FL.transform.rotation = wheel_FL.transform.parent.rotation * Quaternion.Euler(0, steeringAngle * Input.GetAxis("Horizontal"), 0);
                wheel_FR.transform.rotation = wheel_FR.transform.parent.rotation * Quaternion.Euler(0, steeringAngle * Input.GetAxis("Horizontal"), 0);
            }
            else
            {
                wheel_FL.transform.rotation = wheel_FL.transform.parent.rotation * Quaternion.Euler(wheel_FL.transform.rotation.x, steeringAngle * Input.GetAxis("Horizontal"), wheel_FL.transform.rotation.z);
                wheel_FR.transform.rotation = wheel_FR.transform.parent.rotation * Quaternion.Euler(wheel_FR.transform.rotation.x, steeringAngle * Input.GetAxis("Horizontal"), wheel_FR.transform.rotation.z);
            }
    }
}
