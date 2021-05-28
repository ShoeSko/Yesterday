using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCounter : MonoBehaviour
{
    public static int counter;
    public GameObject text;//text that pops up when you 'win' the minigame
    public GameObject objective;
    private float scoreTimer;
    public AudioSource amoosing;
    private bool win = false;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject blackstar1;
    public GameObject blackstar2;
    public GameObject blackstar3;

    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.
    private int score;

    [Header("Time Limit Minigame")]
    [SerializeField] private float minigameTimeLimit = 30f;
    private bool timeIsUp;

    public void Start()
    {
        counter = 0;
        text.SetActive(false);
        objective.SetActive(true);

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        blackstar1.SetActive(false);
        blackstar2.SetActive(false);
        blackstar3.SetActive(false);

        StartCoroutine(TimerForMinigame());
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

        if (win || timeIsUp)//when you have pressed all 4 cow tits:
        {
            text.SetActive(true);//*show the win screen text
            objective.SetActive(false);

            if (scoreTimer <= 3)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                //3 stars
                CardReward.Stars = 3;
                score = 3;
            }
            else if (scoreTimer >= 3 && scoreTimer < 6)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                blackstar3.SetActive(true);
                //2 stars
                CardReward.Stars = 2;
                score = 2;
            }
            else if (scoreTimer >= 6 && scoreTimer < 8)
            {
                star1.SetActive(true);
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
                //1 stars
                CardReward.Stars = 1;
                score = 1;
            }
            else if (scoreTimer >= 8)
            {
                blackstar1.SetActive(true);
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
                //0 stars
                score = 0;
            }
            nextSceneButton.SetActive(true);
            levelTransitioner.currentMinigameScore = score;
        }
    }

    IEnumerator TimerForMinigame()
    {
        yield return new WaitForSeconds(minigameTimeLimit);

        timeIsUp = true;
    }
}