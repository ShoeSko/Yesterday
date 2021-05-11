using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{
    private bool iWon;
    private float timer;
    public float scoreTimer;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    [Header("Scene Transition")]
    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int score;

    private void Start()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    private void Update()
    {
        scoreTimer += Time.deltaTime;//total time the player takes on this level (counted from the begining of the game)

        if (iWon)
        {
            nextSceneButton.SetActive(true);
            levelTransitioner.currentMinigameScore = score;
        }
    }

    public void win()//this is called upon pressing the button in the middle (the goal)
    {
        iWon = true;

        if (scoreTimer <= 6)
        {
            //3stars
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
            CardReward.Stars = 3;
            score = 3;
        }
        else if (scoreTimer > 6 && scoreTimer <= 10)
        {
            //2stars
            star1.SetActive(true);
            star2.SetActive(true);
            CardReward.Stars = 2;
            score = 2;
        }
        else if (scoreTimer > 10 && scoreTimer <= 15)
        {
            //1stars
            star1.SetActive(true);
            CardReward.Stars = 1;
            score = 1;
        }
    }
}
