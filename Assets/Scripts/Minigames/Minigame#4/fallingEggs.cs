using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingEggs : MonoBehaviour
{
    private int EggsToSpawn = 10;
    public GameObject egg;
    private float min = -7.97f;
    private float max = 8.01f;
    private Vector2 spawnLocation;
    private int countdownEgg;
    public static int collectedEggs;
    private bool MakeItRain;
    private float startDelay;

    private float timer = 0;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int score;

    public void Start()
    {
        collectedEggs = 0;
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        MakeItRain = true;
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
        if(MakeItRain == true)
        {
            startDelay += Time.deltaTime;
        }

        if(startDelay >= 3)
        {
            MakeItRain = false;
            StartCoroutine(itsRainingEgg());
            startDelay = 0;
        }

        if(countdownEgg == EggsToSpawn)//when you have spawned all of the eggs, initiate endgame sequence
        {
            countdown();
        }

        if (timer >= 4)//countdown for how long the game lasts
            win();
    }

    private void win()
    {
        if(collectedEggs == 10)//3stars
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            CardReward.Stars = 3;
            score = 3;
        }
        else if(collectedEggs == 9)//2 stars
        {
            star1.SetActive(true);
            star2.SetActive(true);
            CardReward.Stars = 2;
            score = 2;
        }
        else if (collectedEggs == 8)//1 star
        {
            star1.SetActive(true);
            CardReward.Stars = 1;
            score = 1;
        }
        nextSceneButton.SetActive(true);
        levelTransitioner.currentMinigameScore = score;
    }

    private void countdown()
    {
        timer += Time.deltaTime;
    }
}