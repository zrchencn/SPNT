using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GUIManager : MonoBehaviour
{
    private GroupBox reportGB;
    private Button restartButton;
    
    // Start is called before the first frame update
    void Start()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        reportGB = root.Q<GroupBox>("report");
        restartButton = root.Q<Button>("Restart");
        
        restartButton.clicked += RestartButtonPressed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RestartButtonPressed()
    {
        SceneManager.LoadScene("Scenes/Start");
    }
}
