using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Office_Dialogues : MonoBehaviour
{
    static public Dictionary<string, List<Texts>> dialogues = new Dictionary<string, List<Texts>>();
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;
    public GameObject panel;

    void Start()
    {
        // Beolvasás
        StreamReader sr = new StreamReader("szovegek.txt");
        while(sr.Peek() != -1)
        {
            string[] line = sr.ReadLine().Split(';');
            if(!dialogues.ContainsKey(line[0]))
            {
                dialogues.Add(line[0], new List<Texts>{ new Texts(line[1], line[2]) });
            }
            else
            {
                dialogues[line[0]].Add(new Texts(line[1], line[2]));
            }
        }
        sr.Close();
        
        Debug.Log("beolvasás kész");
        panel.SetActive(true);
        
        // Minden művelet egymás után, egy coroutine-ban
        StartCoroutine(StartSequence());
    }        

    IEnumerator StartSequence()
    {
        // 1. Dialógus megjelenítése
        yield return StartCoroutine(ShowDialogue());
        
        // 2. Várakozás a dialógus után
        yield return new WaitForSeconds(2f);
    
        // 3. Scene betöltése
        SceneManager.LoadSceneAsync("Game");
    }

    private object StartCoroutine(IEnumerable enumerable)
    {
        throw new NotImplementedException();
    }

    IEnumerator ShowDialogue()
    {
        Debug.Log("Név megjelenítése");
        if (dialogues.ContainsKey(name))
        {
            foreach (var dialog in dialogues[name])
            {
                Name.text = dialog.Name;
                yield return StartCoroutine(TypeText(dialog.Text));
                yield return new WaitForSeconds(0.3f);
            }
        }
        panel.SetActive(false);
    }

    IEnumerator TypeText(string message)
    {
        Debug.Log("Szöveg megjelenítése");
        Text.text = "";
        foreach (char c in message)
        {
            Text.text += c;
            yield return new WaitForSeconds(0.1f);
        }
    }
}