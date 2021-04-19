using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fallingEggs : MonoBehaviour
{
    private int EggsToSpawn = 10;
    public GameObject egg;
    private float min = -7.97f;
    private float max = 8.01f;
    private Vector2 spawnLocation;
    private int countdownEgg;
    public static int collectedEggs;

    private float timer = 0;
    private float wintimer = 0;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public void Start()
    {
        collectedEggs = 0;
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        StartCoroutine(itsRainingEgg());
    }

    IEnumerator itsRainingEgg()//activate spawning coroutine for falling eggs
    {
        for (int spawnedEggs = 0; spawnedEggs < EggsToSpawn; spawnedEggs++)
        {
            spawnLocation = new Vector2(Random.Range(min, max), 5.46f);//random location (on the screen)
            Instantiate(egg, spawnLocation, transform.rotation);
            countdownEgg++;

            yield return new WaitForSeconds(1.3f);
        }
    }

    public void Update()
    {

        if(countdownEgg == EggsToSpawn)//when you have spawned all of the eggs, initiate endgame sequence
        {
            countdown();
        }

        if (timer >= 4)//countdown for how long the game lasts
            win();

        if (wintimer >= 9)//5 seconds after 'winning', change scene
        {
            if(collectedEggs <= 7)//skip card reward
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
            else//go to card reward
                SceneManager.LoadScene("CardReward");
        }
    }

    private void win()
    {
        if(collectedEggs == 10)//3stars
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if(collectedEggs == 9)//2 stars
        {
            star1.SetActive(true);
            star2.SetActive(true);
        }
        else if (collectedEggs == 8)//1 star
        {
            star1.SetActive(true);
        }
    }

    private void countdown()
    {
        timer += Time.deltaTime;
        wintimer += Time.deltaTime;
    }
}