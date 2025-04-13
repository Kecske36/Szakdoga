using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class Dialogues : MonoBehaviour
{
    static public Dictionary<string, List<Texts>> dialogues = new Dictionary<string, List<Texts>>();
    public  Text Name;
    public Text Text;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        StreamReader sr = new StreamReader("szovegek.txt");
        while(sr.Peek() != -1)
        {
            string[] line = sr.ReadLine().Split(";");
            if(!dialogues.ContainsKey(line[0]))
            {
                dialogues.Add(line[0], new List<Texts>{new Texts(line[1], line[2])});
            }
            else
            {
                dialogues[line[0]].Add(new Texts(line[1], line[2]));
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        panel.SetActive(true);
        foreach(var dialog in dialogues[name])
        {
            Name.text = dialogues[name][0].Name;
            StartCoroutine(TypeText(dialogues[name][0].Text));
        }

        IEnumerator TypeText(string message)
        {
            Text.text = "";
            foreach (char c in message)
            {
                Text.text += c;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(5);
        }
        panel.SetActive(false);
    }
}
