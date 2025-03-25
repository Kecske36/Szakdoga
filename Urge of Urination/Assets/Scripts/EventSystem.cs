using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        PauseMenu();
    }
    void PauseMenu()
    {
        if(Input.GetKeyDown("Escape") && !pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
        }
        else if(Input.GetKeyDown("Escape") && pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
    }
}
