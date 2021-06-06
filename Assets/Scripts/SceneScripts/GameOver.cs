using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject YouLost;
    public GameObject emote;
    public GameObject HandCode;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            if (MinigameSceneScript.Tutorial == false)
                lose();
            else
            {
                Destroy(collider.gameObject); //Destroys the Enemy, as you can not win if they are still on the screen.
                StartCoroutine(CheckIfRobotNeedToGiveTutorialHelp());
            }
        }
    }
    IEnumerator CheckIfRobotNeedToGiveTutorialHelp()
    {
        yield return new WaitForSeconds(0.1f);
        if (FindObjectsOfType<BasicEnemyMovement>().Length > 0)
        {
            HandCode.GetComponent<NewCardHandScript>().TutorialLose();
        }
    }

    private void lose()
    {
        emote.GetComponent<Emotes>().LoseGame();
        YouLost.SetActive(true);
        Time.timeScale = 0;
    }

    //public void returnHome()
    //{
    //    //Time.timeScale = 1;
    //    Quacken.s_quackenBeenReleased = false; //Resets the Quacken.
    //    //SceneManager.LoadScene("MainMenu");
    //}
}
