using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool pauseMenuActive;

    // Update is called once per frame
    void Update()
    {
        PauseMenu();
    }
    void PauseMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
            pauseMenuActive = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            pauseMenuActive = false;
        }
    }
}
