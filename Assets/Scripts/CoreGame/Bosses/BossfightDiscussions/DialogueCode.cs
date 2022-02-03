using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool movement;

    public GameObject BossDefiner;

    public GameObject Dimmer;
    private float fadefloat;

    public List<GameObject> HUDitems = new List<GameObject>();

    public GameObject RobotDialogue;
    public GameObject EnemyDialogue;
    private GameObject NewDialogue;
    private List<GameObject> Messages = new List<GameObject>();

    private List<string> Currentdialogue = new List<string>();
    public List<string> Corporatedialogue = new List<string>();//Includes ALL DIALOGUES from corporate fight
    public List<string> Guardiandialogue = new List<string>();//Includes ALL DIALOGUES from guardian fight
    public List<string> Corruptiondialogue = new List<string>();//Includes ALL DIALOGUES from currouption fight
    private int WhichDialogue = 0;
    private Text CurrentText;


    //private Vector3 FriendlyDialogueSpawn = new Vector3(-9.52f, 0.07f, -16);
    //private Vector3 EnemyDialogueSpawn = new Vector3(-9.52f, 0.07f, -16);

    public GameObject Parent;
    public GameObject Papa;
    private GameObject CurrentPapa;
    private Vector3 Direction = new Vector3(0, 0.018f, 0);

    private bool Robotext = true;

    private void Update()
    {
        //cheatcode: Press 0 during a bossfight to activate the dialogue. This will automatically trigger when the boss takes damage (when i implement it)
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PreperationMode();
        }

        if (Prepare)//Dim
        {
            if(fadefloat <= 0.78f)
            {
                fadefloat += Time.deltaTime * 0.5f;

                Color fadecolor = Dimmer.GetComponent<Renderer>().material.color;

                fadecolor = new Color(fadecolor.r, fadecolor.g, fadecolor.b, fadefloat);
                Dimmer.GetComponent<Renderer>().material.color = fadecolor;
            }
            else//Za Warudo
            {
                for(int i = 0; i < HUDitems.Count; i++)//Turn off HUD (remember to turn it back on
                {
                    HUDitems[i].SetActive(false);
                }

                Time.timeScale = 0;
                Robot_Farmer.GetComponent<Animator>().Play("FarmerIntro");

                if (WhichBoss == 1)
                {
                    Currentdialogue = Corporatedialogue;
                    Businessmen[BossStage - 1].GetComponent<Animator>().Play("EnemyIntro");
                }
                else if (WhichBoss == 2)
                {
                    Currentdialogue = Guardiandialogue;
                    Beasts[BossStage - 1].GetComponent<Animator>().Play("EnemyIntro");
                }

                StartCoroutine(Wait());//Wait until displaying the dialogue

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


        if (movement)
        {
            Parent.transform.Translate(Direction, Space.World);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3);

        NewDialogue = Instantiate(EnemyDialogue);
        Messages.Add(NewDialogue);
        CurrentText = NewDialogue.transform.GetChild(0).gameObject.GetComponent<Text>();
        CurrentText.text = Currentdialogue[WhichDialogue];
        NewDialogue.GetComponent<Animator>().Play("DialogueBoxIntro");

        CurrentPapa = Instantiate(Papa);
        CurrentPapa.transform.parent = Parent.transform;
        NewDialogue.transform.parent = CurrentPapa.transform;
    }

    IEnumerator Move()
    {
        movement = true;

        if(Robotext)
            NewDialogue = Instantiate(RobotDialogue);
        else
            NewDialogue = Instantiate(EnemyDialogue);

        CurrentText = NewDialogue.transform.GetChild(0).gameObject.GetComponent<Text>();
        CurrentText.text = Currentdialogue[WhichDialogue];
        Messages.Add(NewDialogue);
        NewDialogue.GetComponent<Animator>().Play("DialogueBoxIntro");

        yield return new WaitForSecondsRealtime(0.3f);

        CurrentPapa = Instantiate(Papa);
        CurrentPapa.transform.parent = Parent.transform;
        NewDialogue.transform.parent = CurrentPapa.transform;
        movement = false;
    }


    public void PreperationMode()//This is triggered by the boss
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
            if (Input.GetKeyDown(KeyCode.Space))//have this be a button not a random key
            {
                if(WhichDialogue == 3)//Input any number after which the dialogue should end for each character in the Corporate battle
                {
                    //End dialogue
                }
                else
                {
                    WhichDialogue++;
                    //Currentdialogue = Corporatedialogue;
                    StartCoroutine(Move());

                    Robotext = !Robotext;//This SHOULD swap the value of robotext
                }
            }
        }
        //I dont think i need these anymore but ill delete them when im sure
        /*
        else if(BossStage == 2)//Vi
        {

        }
        else//Stage 3 - Demoness
        {

        }
        */
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
