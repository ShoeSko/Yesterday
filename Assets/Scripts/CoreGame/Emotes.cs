using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emotes : MonoBehaviour
{
    public GameObject Default_Emote;
    public GameObject Stress_Emote;
    public GameObject Panic_Emote;
    public GameObject Sad_Emote;
    public GameObject POG_Emote;

    public int EnemiesInStressZone;
    public int EnemiesInPanicZone;

    private bool GameOver;
    private bool GameStarted;

    private void Start()
    {
        Default_Emote.SetActive(false);
        Stress_Emote.SetActive(false);
        Panic_Emote.SetActive(false);
        Sad_Emote.SetActive(false);
        POG_Emote.SetActive(false);
    }

    private void Update()
    {
        Debug.Log(EnemiesInStressZone);
        Debug.Log(EnemiesInPanicZone);

        if (GameOver == false && GameStarted == true)
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
        Default_Emote.SetActive(true);

        POG_Emote.SetActive(false);
        Stress_Emote.SetActive(false);
        Panic_Emote.SetActive(false);
        Sad_Emote.SetActive(false);
    }

    public void StressZone()//When a unit enters the designated "stress zone"
    {
        Stress_Emote.SetActive(true);

        Default_Emote.SetActive(false);
        Panic_Emote.SetActive(false);
    }

    public void PanicZone()//When a unit enters the designated "panic zone"
    {
        Panic_Emote.SetActive(true);

        Default_Emote.SetActive(false);
        Stress_Emote.SetActive(false);
    }

    public void WonGame()//When you win a round
    {
        POG_Emote.SetActive(true);

        Default_Emote.SetActive(false);
        Stress_Emote.SetActive(false);
        Panic_Emote.SetActive(false);
        Sad_Emote.SetActive(false);

        GameOver = true;
    }

    public void LoseGame()//When you lose the game
    {
        Sad_Emote.SetActive(true);

        Default_Emote.SetActive(false);
        Stress_Emote.SetActive(false);
        Panic_Emote.SetActive(false);
        POG_Emote.SetActive(false);

        GameOver = true;
    }
}
