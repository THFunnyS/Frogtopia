using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject PausePanel;
    private bool isPause = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            Pause();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            Continue();
        }
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        isPause = true;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        isPause = false;
        Time.timeScale = 1;
    }
}
