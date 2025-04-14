using System;

[Serializable]
public class Texts
{
    public string Name { get; private set; }
    public string Text { get; private set; }
    
    public Texts(string name, string text)
    {
        Name = name;
        Text = text;
    }
}