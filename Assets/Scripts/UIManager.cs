using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject uiGameOverWindow;
    public GameObject uiPauseWindow;
    public GameObject uiWinMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiPauseWindow.SetActive(!uiPauseWindow.activeSelf);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Load the next scene, which contains the Frogger game
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0); // Load the next scene, which contains the Frogger game
    }

    public void Restart()
    {
        print("Time to restart the scene!");

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit application
    }
}
