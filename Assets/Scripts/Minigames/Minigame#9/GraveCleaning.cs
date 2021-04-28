using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GraveCleaning : MonoBehaviour
{
    public GameObject leaf;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public int score;//how many leaves have been removed
    private float timer;
    private float endgameTimer;

    private int LeavesToSpawn = 4; //how many leaves to spawn

    //these values are used in the leaves spawner 'for loop'
    private float randomX;
    private float randomY;
    private int randomRotation;
    private Vector3 spawnLocation;

    private void Start()
    {
        score = 0;

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        for (int spawnedLeaves = 0; spawnedLeaves < LeavesToSpawn; spawnedLeaves++)//spawn leaves randomly on the grave
        {
            randomX = Random.Range(-2.62f, 2.83f);
            randomY = Random.Range(-3.7f, 1.45f);
            randomRotation = Random.Range(0, 361);

            spawnLocation = new Vector3(randomX, randomY, -2.25f);

            GameObject leafObject = Instantiate(leaf);
            leafObject.transform.position = spawnLocation;
            leafObject.transform.rotation = Quaternion.Euler(0, 0, randomRotation);
        }       
    }

    private void Update()
    {

        if(score == LeavesToSpawn)
        {
            endgameTimer += Time.deltaTime;

            if (timer <= 3)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                CardReward.Stars = 3;
            }
            else if(timer > 3 && timer <= 8)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                CardReward.Stars = 2;
            }
            else if (timer > 8 && timer <= 15)
            {
                star1.SetActive(true);
                CardReward.Stars = 1;
            }
        }
        else
            timer += Time.deltaTime;

        if(endgameTimer >= 5)//change scenes after 5 sec
        {
            if (timer > 15)//skip card reward
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
}