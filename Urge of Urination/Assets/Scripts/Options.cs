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
    // Start is called before the first frame update
    private void Update()
    {
        fovText.text = fovSlider.value.ToString();
    }
    
}
