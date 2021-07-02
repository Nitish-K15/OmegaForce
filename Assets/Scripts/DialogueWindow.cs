using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueWindow : MonoBehaviour
{
    public Text message;
    string Currentmessage;
    public int number;
    void Start()
    {
        Currentmessage = "You unlocked a new weapon" + "\n" + "Press " + number + " to select it";
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

    public void NextScene1()
    {
        GameManager.FirstTimee = true;
        GameManager.LevelsCleared++;
        SceneManager.LoadScene("Level_2");
    }

    public void NextScene2()
    {
        GameManager.FirstTimee = true;
        GameManager.LevelsCleared++;
        SceneManager.LoadScene("Level_3");
    }
}
