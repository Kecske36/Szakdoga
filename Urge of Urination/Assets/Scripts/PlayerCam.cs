using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;

    float xRotaion;
    float yRotaion;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        yRotaion += mouseX;
        xRotaion -= mouseY;
        
        xRotaion = Mathf.Clamp(xRotaion, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotaion, yRotaion, 0);
        orientation.rotation = Quaternion.Euler(0, yRotaion, 0);
    }
}
