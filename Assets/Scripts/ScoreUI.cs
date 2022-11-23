using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{

    private LevelManager levelManager;
    [SerializeField] private TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int curScore = (int)Math.Floor(levelManager.getScore());
        int numLeadingZeros = Math.Max(0, 6 - curScore.ToString().Length);
        string leadingZeros = "";
        for (int i = 0; i < numLeadingZeros; i++)
        {
            leadingZeros = leadingZeros + "0";
        }
        scoreText.text = "SCORE: " + leadingZeros + curScore.ToString("0");
        
    }
}
