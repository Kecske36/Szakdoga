using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider fovSlider;
    public TextMeshProUGUI fovText;
    public Slider mouseSlider;
    public TextMeshProUGUI mouseText;
    // Start is called before the first frame update
    private void Update()
    {
        fovText.text = fovSlider.value.ToString();
        PlayerMovement.fov = (byte) fovSlider.value;

        mouseText.text = mouseSlider.value.ToString();
        MouseLook.mouseSensitivity = (byte) mouseSlider.value;
    }  
}
