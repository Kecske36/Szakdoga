using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody targetRigidbody;
    public Camera playerCam;
    public float playerHeight;
    public LayerMask whatIsGround;
    public float gravity = -9.81f;
    public float moveSpeed; // max sebesség
    public float sprintSpeed; // max futás sebesség
    public float currentSpeed;
    public float groundDrag;
    public KeyCode sprintKey = KeyCode.LeftShift;


    private Rigidbody rb;
    //private CharacterController characterController;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCam = GetComponentInChildren<Camera>();
        currentSpeed = 0;
    }

    void Awake()
    {
       /* characterController = GetComponent<CharacterController>();

        if (characterController == null)
        {
            Debug.LogError("CharacterController component not found on this GameObject. Please attach one.", this);
            enabled = false;
        }*/
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Debug.Log($"X: {x}\tZ: {z}");
        Vector3 inputDirection = new Vector3(x, 0, z);
        inputDirection = Vector3.ClampMagnitude(inputDirection, 1f);
        //= Input.GetKeyDown(sprintKey) ? moveSpeed : sprintSpeed;
        if (x != 0 || z != 0)
        {           
            if (Input.GetKey(sprintKey))
            {
                currentSpeed = sprintSpeed;                
            }
            else
            {
                currentSpeed = moveSpeed;                
            }
        }


        if (currentSpeed > 0)
        {
            /*Ha az aktuális sebesség nagyobb mint 0 akkor
            aktiválódik a séta vagy futás gyorsaságtól függõen*/

            animator.SetBool("isWalking", true); //alapértelmezett séta
            if (currentSpeed > 10)
            {
                /*10nél nagyobb sebesség esetén futás aktiválódása*/
                animator.SetBool("isRunning", true);
            }
            else
            {
                /*10nél kisebb sebesség esetén séta aktiválódása*/
                animator.SetBool("isRunning", false);
            }
        }
        else
        {
            //0 értékû sebesség esetén állás animáció 
            animator.SetBool("isWalking", false);
        }

        Vector3 moveDirection = transform.TransformDirection(inputDirection);
        Vector3 horintalVelocity = moveDirection * currentSpeed;
        //characterController.Move(moveDirection * Time.deltaTime);
        rb.MovePosition(transform.position + moveDirection * Time.deltaTime);
        GravityHandle();

        
    }

    void GravityHandle()
    {
        if (Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround))
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    void LookAround(Vector3 pos)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = mousePosWorld - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}
