using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject SettingsPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PausePanel.activeSelf)
        {
            if (SettingsPanel.activeSelf)
            {
                PausePanel.SetActive(true);
                SettingsPanel.SetActive(false);
            }
            else Pause();
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && PausePanel.activeSelf)
        {
            Continue();
        }
    }

    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
