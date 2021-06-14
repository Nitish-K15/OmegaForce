using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    int Score;
    Text playerScoreText,screenMessageText;
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
        // If there is not already an instance of SoundManager, set it to this
        if (Instance == null)
        {
            Instance = this;
        }
        // If an instance already exists, destroy whatever this object is to enforce the singleton
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        // Set GameManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerScoreText = GameObject.Find("Score").GetComponent<Text>();
        screenMessageText = GameObject.Find("ScreenMessage").GetComponent<Text>();
        SoundManager.Instance.MusicSource.Play();
        StartCoroutine(CountdownEvent(3));
    }
    public void AddScorePoints(int points)
    {
        Score += points;
        playerScoreText.text = Score.ToString();
    }
    IEnumerator CountdownEvent(int count)
    {
        Time.timeScale = 0;
        while (count > 0)
        {
          
            screenMessageText.text = "Ready";
            yield return new WaitForSecondsRealtime(1);
            count--;
        }
        screenMessageText.enabled = false;
        Time.timeScale = 1;
    }
}
