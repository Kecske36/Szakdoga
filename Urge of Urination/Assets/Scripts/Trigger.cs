using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Collider player;
    void OnTriggerEnter(Collider other)
    {
        if(other == player)
        {
            Dialogues.TriggerDialogue(name);
            GetComponent<BoxCollider>().enabled = false;   
        }
    }
}
