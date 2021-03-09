using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCounter : MonoBehaviour
{
    public static int counter;
    private float timer;
    public GameObject text;//text that pops up when you 'win' the minigame

    public void Start()
    {
        counter = 0;
        text.SetActive(false);
    }
    public void Update()
    {
        if (counter == 4)//when you have pressed all 4 cow tits:
        {
            timer += Time.deltaTime;//*start countdown, after 5 sec you get transported to the next level
            text.SetActive(true);//*show the win screen text
        }

        if (timer >= 5)
            SceneManager.LoadScene("HandPrototype");
    }
}
