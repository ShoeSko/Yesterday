using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCounter : MonoBehaviour
{
    public static int counter;
    private float timer;
    public GameObject text;//text that pops up when you 'win' the minigame
    private float scoreTimer;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;


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
        scoreTimer += Time.deltaTime;

        if (counter == 4)//when you have pressed all 4 cow tits:
        {
            timer += Time.deltaTime;//*start countdown, after 5 sec you get transported to the next level
            text.SetActive(true);//*show the win screen text

            if(scoreTimer >= 2)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                //3 stars
            }
            else if (scoreTimer >= 4 && scoreTimer < 6)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                //2 stars
            }
            else if (scoreTimer >= 6 && scoreTimer < 8)
            {
                star1.SetActive(true);
                //1 stars
            }
            else if (scoreTimer >= 8)
            {
                //0 stars
            }
        }

        if (timer >= 5)
            SceneManager.LoadScene("HandPrototype");
    }
}
