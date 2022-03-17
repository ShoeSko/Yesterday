using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsInPlay : MonoBehaviour
{

    public void SettingsPressed()
    {
        Time.timeScale = 0; //Frezes time (for everything that cares about time...)

    }

    public void LeftSettings()
    {
        Time.timeScale = 1; //Resets the time
    }

    public void BackToMainMenu()
    {
        if (FindObjectOfType<MinigameSceneScript>())
        {
            if (!GameObject.Find("TutorialMusic"))
            {
                FindObjectOfType<MinigameSceneScript>().Silence();
            }
        }
        SceneManager.LoadScene("MainMenu");
    }
}
