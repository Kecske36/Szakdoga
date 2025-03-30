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
            animator.SetBool("isRunning", true); //ha a Shift gomb lenyom�sra ker�l, fut
        }

        if(!PlayerMovement.sprintInput){
            animator.SetBool("isRunning", false); //ha a Shift gombot felengedi, a felhasznl� s�t�l
        }

        if(PlayerMovement.walk){
            animator.SetBool("isWalking", true);
        //ha a mozg�sra funkcion�lis gombok lenyom�sra ker�lnek, s�t�l
        }

        if(!PlayerMovement.walk){
            animator.SetBool("isWalking", false);
            //ha a mozg�sra funkcion�lis gombok felenged�sre ker�lnek, meg�ll
        }
    }
}
