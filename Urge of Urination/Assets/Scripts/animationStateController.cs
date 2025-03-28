using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerMovement.sprintInput){
            animator.SetBool("isRunning", true);
        }

        if(!PlayerMovement.sprintInput){
            animator.SetBool("isRunning", false);
        }

        if(PlayerMovement.walk){
            animator.SetBool("isWalking", true);
        }

        if(!PlayerMovement.walk){
            animator.SetBool("isWalking", false);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }
        
        /*
        if (Math.Abs(rb.velocity.magnitude) > 0.5)
        {
            if (Math.Abs(rb.velocity.magnitude) >= 10 || Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetBool("isRunning", true);
                //animator.SetBool("isWalking",false);
            }
            else if (Math.Abs(rb.velocity.magnitude) < 10 || !Input.GetKeyDown(KeyCode.LeftShift)) {
                animator.SetBool("isRunning", false);
                animator.CrossFade("isWalking", 0.5f);
                animator.SetBool("isWalking", true);
            }
        }
        else {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        

        }
        */
    }
}
