using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Label highestScoreLabel;

    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        highestScoreLabel = root.Q<Label>("HighestScore");
        if (MainManager.Instance != null)
        {
            highestScoreLabel.text = "Highest Score: " + MainManager.Instance.HighScore.ToString("0");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Scenes/SampleScene");
            Time.timeScale = 1;
        }
    }
}