using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject Controls, Pause;
    bool Showing = false;
    void Start()
    {
        Controls.SetActive(false);
        Pause.SetActive(false);
    }

    public void ShowControls()
    {
        Pause.SetActive(false);
        Controls.SetActive(true);
    }

    public void BackButton()
    {
        Pause.SetActive(true);
        Controls.SetActive(false);
    }

    public void BackToMenu()
    {
        SoundManager.Instance.StopMusic();
        Time.timeScale = 1;
        SceneManager.LoadScene("Title_Screen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Showing == false)
            {
                Time.timeScale = 0;
                Showing = true;
                Pause.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                Showing = false;
                Pause.SetActive(false);
            }
        }
    }
}
