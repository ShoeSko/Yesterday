using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fallingEggs : MonoBehaviour
{
    private int EggsToSpawn = 8;
    public GameObject egg;
    private float min = -7.97f;
    private float max = 8.01f;
    private Vector2 spawnLocation;
    private int countdownEgg;
    public static int collectedEggs;
    private bool MakeItRain;
    private float startDelay;

    [SerializeField] private List<Image> eggsImage = new List<Image>();
    private int CurrentEgg;

    private float timer = 0;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject blackstar1;
    public GameObject blackstar2;
    public GameObject blackstar3;
    public GameObject FadeboxUI;

    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int score;

    [Header("Time Limit Minigame")]
    [SerializeField] private float minigameTimeLimit = 30f;
    private bool timeIsUp;

    public void Start()
    {
        collectedEggs = 0;
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        blackstar1.SetActive(false);
        blackstar2.SetActive(false);
        blackstar3.SetActive(false);
        FadeboxUI.SetActive(false);

        MakeItRain = true;

        StartCoroutine(TimerForMinigame());
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

        if (timer >= 4 || timeIsUp)//countdown for how long the game lasts
            win();
    }

    public void AnEggWasLost()
    {
        for (int eggNumber = 0; eggNumber < EggsToSpawn; eggNumber++)
        {
            if(eggNumber == CurrentEgg)
            {
                eggsImage[eggNumber].color = Color.red; //Can be adjusted later for specific red.
            }
        }
        CurrentEgg++;
    }

    public void AnEggWasCollected()
    {
        for (int eggNumber = 0; eggNumber < EggsToSpawn; eggNumber++)
        {
            if(eggNumber == CurrentEgg)
            {
                eggsImage[eggNumber].color = Color.green; //Can be adjusted later for specific green
            }
        }
        CurrentEgg++;
    }

    private void win()
    {
        if(collectedEggs == 8)//3stars
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            CardReward.Stars = 3;
            score = 3;
        }
        else if(collectedEggs == 7)//2 stars
        {
            star1.SetActive(true);
            star2.SetActive(true);
            blackstar3.SetActive(true);
            CardReward.Stars = 2;
            score = 2;
        }
        else if (collectedEggs == 6)//1 star
        {
            star1.SetActive(true);
            blackstar2.SetActive(true);
            blackstar3.SetActive(true);
            CardReward.Stars = 1;
            score = 1;
        }
        else if (collectedEggs < 8)
        {
            blackstar1.SetActive(true);
            blackstar2.SetActive(true);
            blackstar3.SetActive(true);
        }
        FadeboxUI.SetActive(true);
        nextSceneButton.SetActive(true);
        levelTransitioner.currentMinigameScore = score;
    }

    private void countdown()
    {
        timer += Time.deltaTime;
    }

    IEnumerator TimerForMinigame()
    {
        yield return new WaitForSeconds(minigameTimeLimit);

        timeIsUp = true;
    }
}