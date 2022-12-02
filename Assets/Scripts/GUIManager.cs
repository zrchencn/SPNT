using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GUIManager : MonoBehaviour
{
    private GroupBox reportGB;
    private Button restartButton;
    private LevelManager levelManager;
    private Label scoreLabel;
    private Label highestScoreLabel;
    private Label endLabel;
    private Label coinScoreLabel;
    
    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        reportGB = root.Q<GroupBox>("report");
        restartButton = root.Q<Button>("Restart");
        scoreLabel = root.Q<Label>("ScoreLabel");
        highestScoreLabel = root.Q<Label>("HighestScoreLabel");
        endLabel = root.Q<Label>("EndLabel");
        coinScoreLabel = root.Q<Label>("CoinScoreLabel");
        
        

        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        
        restartButton.clicked += RestartButtonPressed;

    }

    // Update is called once per frame
    void Update()
    {
        float currentScore = levelManager.getScore();
        scoreLabel.text = "SCORE: " + currentScore.ToString("0");
        highestScoreLabel.text = "HIGHEST SCORE: " + MainManager.Instance.HighScore.ToString("0");
        coinScoreLabel.text = "POINTS FROM COINS: " + levelManager.getPointsFromCoins().ToString("0");
        
        if (levelManager.isGameOver())
        {
            reportGB.visible = true;
        }
        else
        {
            reportGB.visible = false;
        }
    }

    private void RestartButtonPressed()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
    }
}
