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
    private int counterPress;

    public void buttonPressed() //when you press on the tit, do everything in update section
    {
        pullDown = true;
        if (counterPress == 0)
            GoalCounter.counter++;
        counterPress++;
    }

    public void Start()
    {
        startingPos = tit.transform.position;
        down = new Vector2(0, -0.7f);
    }

    public void Update()
    {
        if(pullDown == true)
        {
            timer += Time.deltaTime;

            if(timer <= 1f)
            tit.transform.position = Vector2.MoveTowards(tit.transform.position, startingPos + down, speed * Time.deltaTime);

            if(timer >= 1f)
                tit.transform.position = Vector2.MoveTowards(tit.transform.position, startingPos, speed * Time.deltaTime);
        }
    }
}