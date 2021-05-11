using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PillowScript : MonoBehaviour
{
    public GameObject bedSheet;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private Vector3 moveLenght = new Vector3(0.1078f, 0.02f, 0);
    private Vector3 currentPos;

    public float timer;
    private bool iWon;
    private float countdown;

    [Header("Level Transition")]
    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int score;

    private void Start()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    public void MakeBed()
    {
        if (!iWon)
        {
            currentPos = bedSheet.transform.position;//clarify current position of the BedSheet
            bedSheet.transform.position = currentPos + moveLenght;//move the BedSheet the destination of (moveLenght)
        }
    }

    public void Update()
    {
        if(!iWon)//Score timer
            timer += Time.deltaTime;

        if (bedSheet.transform.position.x >= -3.12f)//win condition
        {
            iWon = true;
        }

        if (iWon)
        {
            countdown += Time.deltaTime;

            if(timer <= 7)//3 stars between 0-7 sec
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                CardReward.Stars = 3;
                score = 3;
            }
            else if(timer > 7 && timer <= 12)//2 stars between 7-12 sec
            {
                star1.SetActive(true);
                star2.SetActive(true);
                CardReward.Stars = 2;
                score = 2;
            }
            else if (timer > 12 && timer <= 18)//1 stars between 12-18 sec
            {
                star1.SetActive(true);
                CardReward.Stars = 1;
                score = 1;
            }

            nextSceneButton.SetActive(true);
            levelTransitioner.currentMinigameScore = score;
        }
    }
}
