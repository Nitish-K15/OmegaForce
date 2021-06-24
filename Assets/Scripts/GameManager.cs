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
    int Score,Lives=3;
    Text playerScoreText,screenMessageText,LiveText;
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
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called when the game is terminated
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void StartGame()
    {
        playerScoreText = GameObject.Find("Score").GetComponent<Text>();
        LiveText = GameObject.Find("Lives").GetComponent<Text>();
        SoundManager.Instance.MusicSource.Play();
        screenMessageText = GameObject.Find("ScreenMessage").GetComponent<Text>();
        playerScoreText.text = Score.ToString();
        LiveText.text = "Lives x" + Lives.ToString();
        StartCoroutine(CountdownEvent(3));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartGame();
    }
    public void NewScene()
    {
        StartCoroutine(CountdownEvent(3));
    }
    public void AddScorePoints(int points)
    {
        Score += points;
        playerScoreText.text = Score.ToString();
    }
    public void updateLives()
    {
        Lives -= 1;
        LiveText.text = "Lives x" + Lives.ToString();
        Invoke("Reload", 0.75f);
    }
    IEnumerator CountdownEvent(int count)
    {
        screenMessageText.gameObject.SetActive(true);
        Time.timeScale = 0;
        while (count > 0)
        {
            screenMessageText.text = "Ready";
            yield return new WaitForSecondsRealtime(1);
            count--;
        }
        screenMessageText.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
