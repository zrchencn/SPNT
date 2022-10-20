using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float currentTime;
    public float endTime;
    public bool gameEnd; 
    void Start()
    {
        currentTime = 0f;
        endTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        Debug.Log("Level Manager" + currentTime);
        if (currentTime >= endTime && !gameEnd)
        {
            endGame();
        }
    }

    public void endGame()
    {
        gameEnd = true;
        Time.timeScale = 0;
        Debug.Log("GAME ENDED");
    }

    public float getScore()
    {
        return currentTime;
    }
}
