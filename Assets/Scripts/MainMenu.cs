using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject Menu,Controls;
    private void Start()
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
        SceneManager.LoadScene("Level_1");
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("Level_Select");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
