using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class GameOver : MonoBehaviour
{
    public Image image;
    [SerializeField] private float fadeDuration = 2.0f; // Másodperc
    public void Start()
    {
        image = GameObject.Find("Game Over Screen").GetComponent<Image>();
    }
    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(GameOverScreen());
    }

    IEnumerator GameOverScreen()
    {
        while (image.color.a != 1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.09f);
            yield return new WaitForSeconds(0.2f);
        }
    }
}