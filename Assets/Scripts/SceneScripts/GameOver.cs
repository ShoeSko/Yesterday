using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject YouLost;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
            lose();
    }

    private void lose()
    {
        YouLost.SetActive(true);
        Time.timeScale = 0;
    }

    public void returnHome()
    {
        SceneManager.LoadScene("MainMenu");
    }
}