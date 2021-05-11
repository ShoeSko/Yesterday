using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCounter : MonoBehaviour
{
    public static int counter;
    public GameObject text;//text that pops up when you 'win' the minigame
    private float scoreTimer;
    public AudioSource amoosing;
    private bool win = false;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.
    private int score;

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
            text.SetActive(true);//*show the win screen text

            if(scoreTimer <= 3)
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
                //2 stars
                CardReward.Stars = 2;
                score = 2;
            }
            else if (scoreTimer >= 6 && scoreTimer < 8)
            {
                star1.SetActive(true);
                //1 stars
                CardReward.Stars = 1;
                score = 1;
            }
            else if (scoreTimer >= 8)
            {
                //0 stars
                score = 0;
            }
            nextSceneButton.SetActive(true);
            levelTransitioner.currentMinigameScore = score;
        }
    }
}