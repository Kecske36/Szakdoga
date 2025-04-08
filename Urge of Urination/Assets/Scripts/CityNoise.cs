using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNoise : MonoBehaviour
{

    public Collider Area;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        Vector3 closestPoint = Area.ClosestPoint(player.transform.position);
        transform.position = closestPoint;
    }
}

public class Footsteps : MonoBehaviour 
{
    public GameObject player;

    void Update()
    {

    }
}
