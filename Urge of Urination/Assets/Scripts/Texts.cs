using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texts : MonoBehaviour
{
    public string Name {get; private set;}
    public string Text {get; private set;}
    public Texts (string name, string text)
    {
        Name = name;
        Text = text;
    }
}
