using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCollider : MonoBehaviour
{
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject blackstar1;
    public GameObject blackstar2;
    public GameObject blackstar3;

    private float ScoreTimer;

    public bool IWin;
    public int score;

    [Header("Scene Transition")]
    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int gameScore;
    void Start()
    {

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        blackstar1.SetActive(false);
        blackstar2.SetActive(false);
        blackstar3.SetActive(false);
    }
    void Update()
    {
        if (score != 3)
            ScoreTimer += Time.deltaTime;//Overall timer for score


        if (score == 3)//Win Condition
        {

            if (ScoreTimer <= 12)//3 stars
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                CardReward.Stars = 3;
            }
            else if (ScoreTimer > 12 && ScoreTimer <= 18)//2 stars
            {
                star1.SetActive(true);
                star2.SetActive(true);
                blackstar3.SetActive(true);
                CardReward.Stars = 2;
            }
            else if (ScoreTimer > 18 && ScoreTimer <= 25)//1 stars
            {
                star1.SetActive(true);
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
                CardReward.Stars = 1;
            }
            else if (ScoreTimer > 25)
            {
                blackstar1.SetActive(true);
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
            }

            nextSceneButton.SetActive(true);
            levelTransitioner.currentMinigameScore = gameScore;

        }
    }
}