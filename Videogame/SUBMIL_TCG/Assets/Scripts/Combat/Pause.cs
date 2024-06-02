// Fernando Fuentes
// 15/05/2024
// Script that handles the pause menu

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isPaused = false;

    void Update()
    {
        // Recieves input from the player to pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the game is paused, resume it. If it is not, pause it.
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        // Pauses the game and shows the pause panel
        pausePanel.SetActive(true);
        /* Time.timeScale = 0f; */
        isPaused = true;
    }

    public void ResumeGame()
    {
        // Resumes the game and hides the pause panel
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
