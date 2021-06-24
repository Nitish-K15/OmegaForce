using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    public Text message;
    public string Currentmessage;
    void Start()
    {
        Currentmessage = "You unlocked a new weapon"+"\n" +"Press 2 to select it";
        StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText()
    {
        message.text = "";
        foreach(char c in Currentmessage.ToCharArray())
        {
            message.text += c;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        yield return null;
    }
}
