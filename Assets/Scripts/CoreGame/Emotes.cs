using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotes : MonoBehaviour
{
    public GameObject Default_Emote;

    public int EnemiesInStressZone;
    public int EnemiesInPanicZone;

    private bool GameOver;
    private bool GameStarted;
    public bool isEndGame;

    [SerializeField] private Animator animation; //The animator for all the emotions. Run emotions.exe

    private void Start()
    {
        Default_Emote.SetActive(false);
    }

    private void Update()
    {
        //Debug.Log(EnemiesInStressZone);
        //Debug.Log(EnemiesInPanicZone);

        if (GameOver == false && GameStarted == true && !isEndGame)
        {
            if (EnemiesInStressZone >= 1 && EnemiesInPanicZone == 0)
            {
                StressZone();
            }
            else if (EnemiesInPanicZone >= 1)
            {
                PanicZone();
            }
            else if(EnemiesInStressZone == 0 && EnemiesInPanicZone == 0)
            {
                Default();
            }
        }
    }



    public void GameStart()
    {
        Default_Emote.SetActive(true);
        GameStarted = true;
    }

    public void Default()
    {
        if (!isEndGame)
        {
            Default_Emote.SetActive(true);

            animation.SetTrigger("Safe");
        }
    }

    public void StressZone()//When a unit enters the designated "stress zone"
    {
        if (!isEndGame) 
        {
            animation.SetTrigger("Sweat");
        }
    }

    public void PanicZone()//When a unit enters the designated "panic zone"
    {
        if (!isEndGame) 
        {
            animation.SetTrigger("Danger");
        }
    }

    public void WonGame()//When you win a round
    {
        if (!GameOver)
        {
            animation.SetTrigger("Pog");
            print("That is POG!");
            GameOver = true;
        }

    }

    public void LoseGame()//When you lose the game
    {

        animation.SetTrigger("Depressed");
        
        GameOver = true;

    }
}
