using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject Menu,Controls;
    void Start()
    {
        Menu.SetActive(false);
        Controls.SetActive(false);
        Invoke("ActivatePanel", 3f);
    }

    private void ActivatePanel()
    {
        Menu.SetActive(true);
    }
    public void ShowControls()
    {
        Menu.SetActive(false);
        Controls.SetActive(true);
    }
    
    public void BackButton()
    {
        Controls.SetActive(false);
        Menu.SetActive(true);
    }
    public void Play()
    {
        GameManager.FirstTimee = true;
        GameManager.Instance.Lives = 3;
        GameManager.Instance.Score = 0;
        SceneManager.LoadScene(1);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("Level_Select");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
