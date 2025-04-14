using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireBlowOut : MonoBehaviour
{
    public Collider car;
    public Collider mobile;
    public Collider wheel;
    void OnTriggerEnter(Collider other)
    {
        if(other == car)
        {
            CarInteraction.playerNearbyStatic = false;
            CarInteraction.ExitCar();
            mobile.gameObject.SetActive(true);
            wheel.gameObject.SetActive(true);
        }
    }
}
