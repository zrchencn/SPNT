using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float currentTime;
    public float endTime;
    
    private bool gameEnd;
    private float score;
    private float highScore;
    private float pointsFromCoins;
    private bool dead;

    private GameOverScreen gameOver;
    private AudioSource music;
    [SerializeField] private AudioClip music_menu;


    void Start()
    {
        currentTime = 0f;
        endTime = Single.PositiveInfinity;
        score = 0f;
        // if (MainManager.Instance != null)
        // {
        //     highScore = MainManager.Instance.HighScore;
        // }
        dead = false;
        gameOver = GameObject.Find("Game Over").GetComponent<GameOverScreen>();
        gameOver.Disable();
        music = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            currentTime += Time.deltaTime;
            score += Time.deltaTime;
        }
        // Debug.Log("Level Manager" + currentTime);
        if (currentTime >= endTime && !gameEnd)
        {
            endGame();
        }
    }

    public void endGame()
    {
        gameEnd = true;
        highScore = Math.Max(highScore, score);
        Time.timeScale = 0;
        Debug.Log("GAME ENDED");
        Debug.Log(highScore);
        //MainManager.Instance.updateHighestScore(highScore);
        //SceneManager.LoadScene("GameOver");
        gameOver.Enable();
        gameOver.setScores((int)Math.Floor(pointsFromCoins/5), (int)score, (int)currentTime);
        music.clip = music_menu;
        music.volume = 0.7f;
        music.Play();
    }

    public float getScore()
    {
        return score;
    }

    public void addToScore(float points)
    {
        pointsFromCoins += points;
        score += points;
    }

    public bool isGameOver()
    {
        return gameEnd;
    }

    public float getPointsFromCoins()
    {
        return pointsFromCoins;
    }

    public void kill()
    {
        dead = true;
    }
}
