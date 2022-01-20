using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCode : MonoBehaviour
{
    //Various Robot visuals
    public GameObject Robot_Farmer;
    public GameObject Robot_Armor;
    public GameObject Robot_Suit;
    public GameObject Robot_Casual;

    //Various Businessmen
    public List<GameObject> Businessmen = new List<GameObject>();//Mommy, vi, demonessa

    //Various Beasts
    public List<GameObject> Beasts = new List<GameObject>();//rat, shadowdoggo, cricket, lurker

    private int WhichBoss;//What boss is the player facing
    private int BossStage;//How much damage has the boss taken already

    private bool Prepare;
    private bool ConversationMode;

    public GameObject BossDefiner;


    public GameObject Dimmer;
    private float fadefloat;

    public List<GameObject> HUDitems = new List<GameObject>();

    private void Update()
    {
        //cheatcode: Press 0 during a bossfight to activate the dialogue. This will automatically trigger when the boss takes damage (when i implement it)
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PreperationMode();
        }

        if (Prepare)//Dim
        {
            if(fadefloat <= 0.5f)
            {
                fadefloat += Time.deltaTime * 0.5f;

                Color fadecolor = Dimmer.GetComponent<Renderer>().material.color;

                fadecolor = new Color(fadecolor.r, fadecolor.g, fadecolor.b, fadefloat);
                Dimmer.GetComponent<Renderer>().material.color = fadecolor;
            }
            else//Time stop
            {
                for(int i = 0; i < HUDitems.Count; i++)//Turn off HUD (remember to turn it back on
                {
                    HUDitems[i].SetActive(false);
                }

                Time.timeScale = 0;
                Robot_Farmer.GetComponent<Animator>().Play("FarmerIntro");

                if (WhichBoss == 1)
                    Businessmen[BossStage - 1].GetComponent<Animator>().Play("EnemyIntro");
                else if (WhichBoss == 2)
                    Beasts[BossStage - 1].GetComponent<Animator>().Play("EnemyIntro");

                ConversationMode = true;
            }
        }

        if (ConversationMode)//Trigger the correct void
        {
            Prepare = false;

            if (WhichBoss == 1)
                CorporateDialogue();

            if (WhichBoss == 2)
                WardenDialogue();

            if (WhichBoss == 3)
                CorruptionDialogue();
        }
    }

    public void PreperationMode()
    {
        WhichBoss = BossDefiner.GetComponent<NewCardHandScript>().RandomBoss;
        BossStage++;

        Prepare = true;
    }

    private void CorporateDialogue()
    {
        //BossStage++; Trigger this from the boss

        if(BossStage == 1)//Mommy
        {
            Debug.Log("Now this works and i can do stuff here");
        }
        else if(BossStage == 2)//Vi
        {

        }
        else//Stage 3 - Demoness
        {

        }
    }


    private void WardenDialogue()
    {
        BossStage++;

        if (BossStage == 1)
        {

        }
        else if (BossStage == 2)
        {

        }
        else if (BossStage == 3)
        {

        }
        else//Stage 4
        {

        }
    }


    private void CorruptionDialogue()
    {
        BossStage++;

        if (BossStage == 1)
        {

        }
        else if (BossStage == 2)
        {

        }
        else//Stage 3
        {

        }
    }
}
