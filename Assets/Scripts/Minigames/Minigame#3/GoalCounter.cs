using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCounter : MonoBehaviour
{
    public static int counter;
    private float timer;
    public GameObject text;//text that pops up when you 'win' the minigame
    private float scoreTimer;
    public AudioSource amoosing;
    private bool win = false;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;


    public void Start()
    {
        counter = 0;
        text.SetActive(false);

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }
    public void Update()
    {
        if(!win)
            scoreTimer += Time.deltaTime;

        if (counter == 4)
        {
            amoosing.Play();
            win = true;
            counter++;
        }

        if (win)//when you have pressed all 4 cow tits:
        {
            timer += Time.deltaTime;//*start countdown, after 5 sec you get transported to the next level
            text.SetActive(true);//*show the win screen text

            if(scoreTimer <= 3)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                //3 stars
                CardReward.Stars = 3;
            }
            else if (scoreTimer >= 3 && scoreTimer < 6)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                //2 stars
                CardReward.Stars = 2;
            }
            else if (scoreTimer >= 6 && scoreTimer < 8)
            {
                star1.SetActive(true);
                //1 stars
                CardReward.Stars = 1;
            }
            else if (scoreTimer >= 8)
            {
                //0 stars
            }
        }

        if (timer >= 5)
        {
            if(scoreTimer >= 8)//skip card reward
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
                    SceneManager.LoadScene("CoreGame");
                }
            }
            else//Go to card reward screen
                SceneManager.LoadScene("CardReward");
        }
    }
}