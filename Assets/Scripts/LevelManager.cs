using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float currentTime;
    public float endTime;
    
    private bool gameEnd;
    private float score;
    private float highScore;
    private float pointsFromCoins;
    void Start()
    {
        currentTime = 0f;
        endTime = Single.PositiveInfinity;
        score = 0f;
        if (MainManager.Instance != null)
        {
            highScore = MainManager.Instance.HighScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        score += Time.deltaTime;
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
        MainManager.Instance.updateHighestScore(highScore);
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
}
