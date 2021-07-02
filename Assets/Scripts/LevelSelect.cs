using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public Button[] buttons;
    int LevelsUnlocked;
    private void Start()
    {
        LevelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);
        for(int i = 0; i< buttons.Length;i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < LevelsUnlocked; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void LoadLevel(int Levelindex)
    {
        GameManager.FirstTimee = true;
        GameManager.Instance.Lives = 3;
        GameManager.Instance.Score = 0;
        SceneManager.LoadScene(Levelindex);
    }

    public void ResetLevels()
    {
        PlayerPrefs.DeleteKey("LevelsUnlocked");
        GameManager.LevelsCleared = 1;
        Start();
    }
}
