using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject YouLost;
    public GameObject emote;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
            lose();
    }

    private void lose()
    {
        YouLost.SetActive(true);
        Time.timeScale = 0;
        emote.GetComponent<Emotes>().LoseGame();
    }

    //public void returnHome()
    //{
    //    //Time.timeScale = 1;
    //    Quacken.s_quackenBeenReleased = false; //Resets the Quacken.
    //    //SceneManager.LoadScene("MainMenu");
    //}
}
