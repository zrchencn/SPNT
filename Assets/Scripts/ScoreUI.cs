using System.Collections;
using System.Collections.Generic;
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
        scoreText.text = "SCORE: " + levelManager.getScore().ToString("0");
        
    }
}
