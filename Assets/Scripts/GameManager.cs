using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;
    public static bool FirstTimee = true;
    public static int LevelsCleared = 1;
    public int Score,Lives=3;
    Text playerScoreText,screenMessageText,LiveText;
    public GameObject playerRef;
    //public Transform LevelStart;
    [SerializeField]
    public Vector2 Checkpoint;
    private Vector2 Start;
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
        Start = GameObject.Find("Start").transform.position;
        screenMessageText = GameObject.Find("ScreenMessage").GetComponent<Text>();
        playerScoreText.text = Score.ToString();
        LiveText.text = "Lives x" + Lives.ToString();
        if(FirstTimee == true)
        {
            Checkpoint = Start;
            FirstTimee = false;
        }
        StartCoroutine(CountdownEvent(3));
        player.gameObject.SetActive(true);
    }
    public void UpdateCheckpoint(Vector2 PlayerCheck)
    {
        Checkpoint = PlayerCheck;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartGame();
    }

    public void AddScorePoints(int points)
    {
        Score += points;
        playerScoreText.text = Score.ToString();
    }

    public void UpdateLives()
    {
        if (Lives > 0)
        {
            Lives -= 1;
            LiveText.text = "Lives x" + Lives.ToString();
            Invoke("Reload", 1f);
        }
        else
        {
            Lives = 3;
            Score = 0;
            Checkpoint = Start;
            Invoke("Reload", 1f);
        }
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
