using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowTitButton : MonoBehaviour
{
    public GameObject tit;//choose corresponding tit to the button (tit 2 to button 2)
    private bool pullDown;
    private float speed = 0.7f;
    private Vector2 startingPos;
    private Vector2 down;
    public float timer;
    private int counterPress;//this is used to make it so the game doesn't count the same button more than once for the win-condition

    public void buttonPressed() //when you press on the tit, do everything in update section
    {
        pullDown = true;
        if (counterPress == 0)
            GoalCounter.counter++;
        counterPress++;
    }

    public void Start()
    {
        startingPos = tit.transform.position; //define original position
        down = new Vector2(0, -0.7f);//define how far down the tit goes when clicked
    }

    public void Update()
    {
        if(pullDown == true)//when you have pressed the button (the tit)
        {
            timer += Time.deltaTime;

            if(timer <= 1f)//move down
            tit.transform.position = Vector2.MoveTowards(tit.transform.position, startingPos + down, speed * Time.deltaTime);

            if(timer >= 1f)//move back up
                tit.transform.position = Vector2.MoveTowards(tit.transform.position, startingPos, speed * Time.deltaTime);
        }
    }
}