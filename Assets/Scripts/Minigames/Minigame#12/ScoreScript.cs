using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    private bool iWon;
    private float timer;
    public float scoreTimer;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

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
            timer += Time.deltaTime;

            if (timer >= 5)//after reaching the goal, change scene after 5 sec
            {
                //transform scene
                Debug.Log("i changed scene");//delete when we implement scenemanager
            }
        }
    }

    public void win()//this is called upon pressing the button in the middle (the goal)
    {
        iWon = true;

        if (scoreTimer <= 10)
        {
            //3stars
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if (scoreTimer <= 20 && scoreTimer > 10)
        {
            //2stars
            star1.SetActive(true);
            star2.SetActive(true);
        }
        else if (scoreTimer <= 30 && scoreTimer > 20)
        {
            //1stars
            star1.SetActive(true);
        }
    }
}
