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
    public GameObject blackstar1;
    public GameObject blackstar2;
    public GameObject blackstar3;

    public int score;//how many leaves have been removed
    private float timer;

    private int LeavesToSpawn = 4; //how many leaves to spawn

    //these values are used in the leaves spawner 'for loop'
    private float randomX;
    private float randomY;
    private int randomRotation;
    private Vector3 spawnLocation;

    [Header("Level transition")]
    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int minigameScore;

    [Header("Time Limit Minigame")]
    [SerializeField] private float minigameTimeLimit = 30f;
    private bool timeIsUp;

    private void Start()
    {
        score = 0;

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        blackstar1.SetActive(false);
        blackstar2.SetActive(false);
        blackstar3.SetActive(false);

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

        StartCoroutine(TimerForMinigame());
    }

    private void Update()
    {

        if(score == LeavesToSpawn || timeIsUp)
        {
            if (timer <= 3)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                CardReward.Stars = 3;
                minigameScore = 3;
            }
            else if(timer > 3 && timer <= 8)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                blackstar3.SetActive(true);
                CardReward.Stars = 2;
                minigameScore = 2;
            }
            else if (timer > 8 && timer <= 15)
            {
                star1.SetActive(true);
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
                CardReward.Stars = 1;
                minigameScore = 1;
            }
            else if (timer > 15)
            {
                blackstar1.SetActive(true);
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
            }

            nextSceneButton.SetActive(true);
            levelTransitioner.currentMinigameScore = minigameScore;
        }
        else
            timer += Time.deltaTime;


    }

    IEnumerator TimerForMinigame()
    {
        yield return new WaitForSeconds(minigameTimeLimit);

        timeIsUp = true;
    }
}