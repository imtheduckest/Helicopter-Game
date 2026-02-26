using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject panel;
    private bool isPaused = false;

    public void PauseGame()
    {
        isPaused = !isPaused;
        
        if (isPaused == true)
        {
            panel.SetActive(isPaused);
            Time.timeScale = 0;
        }
        
        else
        {
            panel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
