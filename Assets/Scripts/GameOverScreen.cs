using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    
    //[SerializeField] private LevelManager levelManager;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI totalText;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    //Update is called once per frame
    // void Update()
    // {
    //     int numCoins = (int) Math.Floor(levelManager.getPointsFromCoins() / 5);
    //     int totalScore = (int)Math.Floor(levelManager.getScore());
    //     int time = totalScore - (int)levelManager.getPointsFromCoins();
    //     timeText.text = time.ToString();
    //     coinText.text = numCoins.ToString();
    //     totalText.text = totalScore.ToString();
    // }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void setScores(int numCoins, int totalScore, int time)
    {
        gameObject.SetActive(true);
        timeText.text = time.ToString();
        coinText.text = numCoins.ToString();
        totalText.text = totalScore.ToString();
    }
}
