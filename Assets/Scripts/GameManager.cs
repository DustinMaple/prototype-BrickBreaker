using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private int _level = 1;
    [SerializeField] private int _defaultLives = 3;

    private int _score = 0;
    private int _lives = 3;

    private Paddle _paddle;
    private Ball _ball;
    private Brick[] _bricks;
    private int _targetBrickCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SceneManager.sceneLoaded += OnLevelLoaded;

        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        _paddle = FindObjectOfType<Paddle>();
        _ball = FindObjectOfType<Ball>();
        _bricks = FindObjectsByType<Brick>(FindObjectsSortMode.None);

        int count = 0;
        foreach (Brick brick in _bricks)
        {
            if (!brick.UnBreakable)
            {
                count++;
            }
        }

        _targetBrickCount = count;
        Debug.Log($"Target: {_targetBrickCount}");
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        _score = 0;
        _lives = _defaultLives;

        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        _level = level;

        Scene scene = SceneManager.GetSceneByName($"Level{level}");
        if (scene.IsValid())
        {
            SceneManager.LoadScene($"Level{level}");
        }
        else
        {
            Win();
        }
    }

    private void Win()
    {
        Time.timeScale = 0;
        Debug.Log("You Win!!!!!!!!!!!!!!!");
    }

    public void AddScore(int score)
    {
        _score += score;
        _targetBrickCount--;
        Debug.Log($"Cur Score:{_score}");

        if (_targetBrickCount <= 0)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        LoadLevel(_level + 1);
    }

    public void Miss()
    {
        _lives--;
        if (_lives > 0)
        {
            ResetLevel();
        }
        else
        {
            // 如果是编辑器，退出播放模式，否则退出应用
            GameOver();
        }
    }

    private void GameOver()
    {
        NewGame();
// #if UNITY_EDITOR
//         UnityEditor.EditorApplication.isPlaying = false;
// #else
//         Application.Quit();
// #endif
    }

    private void ResetLevel()
    {
        _ball.Reset();
        _paddle.Reset();
    }
}