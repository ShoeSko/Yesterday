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
    public GameObject FadeboxUI;

    private float ScoreTimer;

    public bool IWin;
    public int score;

    [Header("Scene Transition")]
    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int gameScore;

    [Header("Time Limit Minigame")]
    [SerializeField] private float minigameTimeLimit = 30f;
    private bool timeIsUp;

    void Start()
    {

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        blackstar1.SetActive(false);
        blackstar2.SetActive(false);
        blackstar3.SetActive(false);
        FadeboxUI.SetActive(false);

        StartCoroutine(TimerForMinigame());
    }
    void Update()
    {
        if (score != 3)
            ScoreTimer += Time.deltaTime;//Overall timer for score


        if (score == 3 || timeIsUp)//Win Condition
        {

            if (ScoreTimer <= 14)//3 stars
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                CardReward.Stars = 3;
                gameScore = 3;
            }
            else if (ScoreTimer > 14 && ScoreTimer <= 19)//2 stars
            {
                star1.SetActive(true);
                star2.SetActive(true);
                blackstar3.SetActive(true);
                CardReward.Stars = 2;
                gameScore = 2;
            }
            else if (ScoreTimer > 19 && ScoreTimer <= 26)//1 stars
            {
                star1.SetActive(true);
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
                CardReward.Stars = 1;
                gameScore = 1;
            }
            else if (ScoreTimer > 26)
            {
                blackstar1.SetActive(true);
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
            }
            FadeboxUI.SetActive(true);
            nextSceneButton.SetActive(true);
            levelTransitioner.currentMinigameScore = gameScore;

        }
    }

    IEnumerator TimerForMinigame()
    {
        yield return new WaitForSeconds(minigameTimeLimit);

        timeIsUp = true;
    }
}