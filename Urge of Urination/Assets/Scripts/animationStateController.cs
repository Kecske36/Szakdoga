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
            animator.SetBool("isRunning", true); //ha a Shift gomb lenyomásra kerül, fut
        }

        if(!PlayerMovement.sprintInput){
            animator.SetBool("isRunning", false); //ha a Shift gombot felengedi, a felhasznló sétál
        }

        if(PlayerMovement.walk){
            animator.SetBool("isWalking", true);
        //ha a mozgásra funkcionális gombok lenyomásra kerülnek, sétál
        }

        if(!PlayerMovement.walk){
            animator.SetBool("isWalking", false);
            //ha a mozgásra funkcionális gombok felengedésre kerülnek, megáll
        }
    }
}
