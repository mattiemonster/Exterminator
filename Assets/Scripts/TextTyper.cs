using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTyper : MonoBehaviour
{
    public List<string> entries;
    public TextMeshProUGUI text;

    public bool AnyEntriesLeft
    {
        get { return (stringIndex != entries.Count - 1) ? true : false; }
    }

    private int stringIndex = 0;
    private int charIndex;
    private int maxChar;

    void Start()
    {
        text.text = "";
        if (entries.Count == 0) Debug.LogError("No text items");
        LoadString(0);
        InvokeRepeating("Type", 0f, 0.05f);
    }

    void Type()
    {
        if (charIndex == maxChar) return;
        text.text = text.text + entries[stringIndex][charIndex];
        charIndex++;
    }

    void LoadString(int index)
    {
        text.text = "";
        maxChar = entries[stringIndex].Length;
        charIndex = 0;
    }

    public void LoadNextString()
    {
        stringIndex++;
        LoadString(stringIndex);
    }
}
