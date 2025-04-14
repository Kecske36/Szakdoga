using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class Dialogues : MonoBehaviour
{
    static public Dictionary<string, List<Texts>> dialogues = new Dictionary<string, List<Texts>>();
    
    // Non-static UI elemek (Inspector-ban beállítandó)
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;
    public GameObject panel;
    
    // Static referenciák
    private static TextMeshProUGUI _nameStatic;
    private static TextMeshProUGUI _textStatic;
    private static GameObject _panelStatic;
    private static MonoBehaviour _monoBehaviourInstance;

    void Start()
    {
        // Static referenciák inicializálása
        _nameStatic = Name;
        _textStatic = Text;
        _panelStatic = panel;
        _monoBehaviourInstance = this;
        
        // Beolvasás
        StreamReader sr = new StreamReader("szovegek.txt");
        while(sr.Peek() != -1)
        {
            string[] line = sr.ReadLine().Split(';');
            string key = line[0].Trim();
            if(!dialogues.ContainsKey(key))
            {
                dialogues.Add(key, new List<Texts>());
            }
            dialogues[key].Add(new Texts(line[1].Trim(), line[2].Trim()));
        }
        sr.Close();
        
        Debug.Log("Beolvasás kész");
    }

    static public void TriggerDialogue(string triggerName)
    {
        if(_monoBehaviourInstance != null)
        {
            _monoBehaviourInstance.StartCoroutine(ShowDialogueCoroutine(triggerName));
        }
        else
        {
            Debug.LogError("Dialogues instance not initialized!");
        }
    }

    static private IEnumerator ShowDialogueCoroutine(string triggerName)
    {
        if(!dialogues.ContainsKey(triggerName))
        {
            Debug.LogWarning($"No dialogue found for trigger: {triggerName}");
            yield break;
        }

        _panelStatic.SetActive(true);
        
        foreach(var dialog in dialogues[triggerName])
        {
            _nameStatic.text = dialog.Name;
            yield return TypeTextCoroutine(dialog.Text);
            yield return new WaitForSeconds(0.3f);
        }
        
        _panelStatic.SetActive(false);
    }

    static private IEnumerator TypeTextCoroutine(string message)
    {
        _textStatic.text = "";
        foreach(char c in message)
        {
            _textStatic.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}