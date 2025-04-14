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
    public static byte fov;
    public float playerHeight;
    public LayerMask whatIsGround;
    public float gravity = -9.81f;
    public float moveSpeed; // max sebess�g
    public float sprintSpeed; // max fut�s sebess�g
    public float currentSpeed;
    public float groundDrag;
    public KeyCode sprintKey = KeyCode.LeftShift;
    private Rigidbody rb;
    //private CharacterController characterController;

    
    void Start()
    {
        playerCam.fieldOfView = fov < 30? 90 : fov > 90? 90 : fov;
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
        if (!EventSystem.pauseMenuActive)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
//            Debug.Log($"X: {x}\tZ: {z}");
            Vector3 inputDirection = new Vector3(x, 0, z);
            inputDirection = Vector3.ClampMagnitude(inputDirection, 1f);
            //= Input.GetKeyDown(sprintKey) ? moveSpeed : sprintSpeed;
            currentSpeed = 0;
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
                /*Ha az aktu�lis sebess�g nagyobb mint 0 akkor
                aktiv�l�dik a s�ta vagy fut�s gyorsas�gt�l f�gg�en*/

                animator.SetBool("isWalking", true); //alap�rtelmezett s�ta
                if (currentSpeed > 10)
                {
                    /*10n�l nagyobb sebess�g eset�n fut�s aktiv�l�d�sa*/
                    animator.SetBool("isRunning", true);
                }
                else
                {
                    /*10n�l kisebb sebess�g eset�n s�ta aktiv�l�d�sa*/
                    animator.SetBool("isRunning", false);
                }
            }
            else
            {
                //0 �rt�k� sebess�g eset�n �ll�s anim�ci� 
                animator.SetBool("isWalking", false);
            }

            Vector3 moveDirection = transform.TransformDirection(inputDirection);
            Vector3 horintalVelocity = moveDirection * currentSpeed;
            //characterController.Move(moveDirection * Time.deltaTime);
            rb.MovePosition(transform.position + horintalVelocity * Time.deltaTime);
            GravityHandle();
        }
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
