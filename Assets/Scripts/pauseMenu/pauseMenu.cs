using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pauseMenu : MonoBehaviour
{
    public PlayerController controller;


    public void pauseGame()
    {
        Time.timeScale = 0f;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (controller.gameIsPaused)
        {
            pauseGame();
        }
        else
        {
            resumeGame();
        }
    }
}
