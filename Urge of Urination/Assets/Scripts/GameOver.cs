using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Image image;
    public Text text;
    public float fadeSpeed = 0.05f;
    public float fadeDelay = 0.05f;

    void Start()
    {
        // Kezdetben láthatatlan
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        text.text = $"{Dialogues.dialogues[name][0].Name}\n{Dialogues.dialogues[name][0].Text}";
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(FadeInShit());
    }

    IEnumerator FadeInShit()
    {
        // Képernyő fade
        while (image.color.a < 1)
        {
            image.color += new Color(0, 0, 0, fadeSpeed);
            yield return new WaitForSeconds(fadeDelay);
        }

        // Szöveg fade
        while (text.color.a < 1)
        {
            text.color += new Color(0, 0, 0, fadeSpeed);
            yield return new WaitForSeconds(fadeDelay);
        }
    }
}