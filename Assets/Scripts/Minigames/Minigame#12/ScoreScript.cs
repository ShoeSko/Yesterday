using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    private bool iWon;
    private float timer;
    public float scoreTimer;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private void Start()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    private void Update()
    {
        scoreTimer += Time.deltaTime;//total time the player takes on this level (counted from the begining of the game)

        if (iWon)
        {
            timer += Time.deltaTime;

            if (timer >= 5)//after reaching the goal, change scene after 5 sec
            {
                if (scoreTimer > 30)//skip card reward
                {
                    if (MinigameSceneScript.activeMinigame == 1)
                    {
                        MinigameSceneScript.activeMinigame++;
                        SceneManager.LoadScene("Minigame#" + MinigameSceneScript.scene2);
                    }
                    else if (MinigameSceneScript.activeMinigame == 2)
                    {
                        MinigameSceneScript.activeMinigame++;
                        SceneManager.LoadScene("Minigame#" + MinigameSceneScript.scene3);
                    }
                    else if (MinigameSceneScript.activeMinigame == 3)
                    {
                        SceneManager.LoadScene("CoreGame2");
                    }
                }
                else//go to card reward
                    SceneManager.LoadScene("CardReward");
            }
        }
    }

    public void win()//this is called upon pressing the button in the middle (the goal)
    {
        iWon = true;

        if (scoreTimer <= 10)
        {
            //3stars
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            CardReward.Stars = 3;
        }
        else if (scoreTimer > 10 && scoreTimer <= 20)
        {
            //2stars
            star1.SetActive(true);
            star2.SetActive(true);
            CardReward.Stars = 2;
        }
        else if (scoreTimer > 20 && scoreTimer <= 30)
        {
            //1stars
            star1.SetActive(true);
            CardReward.Stars = 1;
        }
    }
}
