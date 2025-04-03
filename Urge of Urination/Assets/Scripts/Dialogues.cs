using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    public Dictionary<string, Texts> dialogues;
    public  Text Name;
    public Text Text;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        dialogues.Add("ugat", new Texts("Kutya", "vau"));
        dialogues.Add("nyavog", new Texts("macska", "miau"));
        // StreamReader sr = new StreamReader("szovegek.txt");
        // while(sr.Peek() != -1)
        // {
        //     string[] line = sr.ReadLine().Split();
        //     dialogues.Add(line[0], new Texts(line[1], line[2]));
        // }
    }

    public void OnTriggerEnter(Collider other) {
        panel.SetActive(true);
        Name.text = dialogues[name].Name;
        foreach(var item in dialogues[name].Text)
        {
            Text.text += item;
        }
    }
}
