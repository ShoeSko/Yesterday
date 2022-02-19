using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCode : MonoBehaviour
{
    //Various Robot visuals
    public List<GameObject> Robot = new List<GameObject>();
    private int RandomOutfit;
    private GameObject CurrentOutfit;

    //Various Businessmen
    public List<GameObject> Businessmen = new List<GameObject>();//Mommy, vi, demonessa

    //Various Beasts
    public List<GameObject> Beasts = new List<GameObject>();//rat, shadowdoggo, cricket, lurker

    //Corrupted Units
    public List<GameObject> Corruptions = new List<GameObject>();//Units defined by your own deck
    public List<Sprite> CorruptionSprites = new List<Sprite>();//Used in "TheCorruption" code

    private List<GameObject> CurrentEnemies = new List<GameObject>();//The currently used list

    private int WhichBoss;//What boss is the player facing
    private int BossStage;//How much damage has the boss taken already

    private bool Prepare;
    private bool movement;

    public GameObject BossDefiner;

    public GameObject Dimmer;
    private float fadefloat;

    public List<GameObject> HUDitems = new List<GameObject>();

    public GameObject RobotDialogue;
    public GameObject EnemyDialogue;
    public Text EnemyDialoguetext;
    private GameObject NewDialogue;
    private List<GameObject> Messages = new List<GameObject>();

    private List<string> Currentdialogue = new List<string>();
    public List<string> Corporatedialogue = new List<string>();//Includes ALL DIALOGUES from corporate fight
    public List<string> Guardiandialogue = new List<string>();//Includes ALL DIALOGUES from guardian fight
    public List<string> Corruptiondialogue = new List<string>();//Includes ALL DIALOGUES from currouption fight
    private int WhichDialogue = 0;
    private Text CurrentText;

    public GameObject Parent;
    public GameObject Papa;
    private GameObject CurrentPapa;
    private Vector3 Direction = new Vector3(0, 10f, 0);
    private int Offset;

    private bool Robotext = true;
    private bool GameResuming;
    private bool CanSkipDialogue;
    private bool GamePaused;

    public GameObject NextButton;

    private Color CorporateColor = new Color(0.8113208f, 0.2104842f, 0.2104842f, 1);
    private Color GuardianColor = new Color(0.2010959f, 0.1251335f, 0.7169812f, 1);
    private Color CorruptionColor = new Color(0.6315554f, 0, 0.8396226f, 1);

    private void Update()
    {
        //cheatcode: Press 0 during a bossfight to activate the dialogue. This will automatically trigger when the boss takes damage (when i implement it)
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PreperationMode();
        }

        if (Prepare)//Dim
        {
            if (fadefloat <= 0.78f)
            {
                fadefloat += Time.deltaTime * 0.5f;

                Color fadecolor = Dimmer.GetComponent<SpriteRenderer>().color;

                fadecolor = new Color(fadecolor.r, fadecolor.g, fadecolor.b, fadefloat);
                Dimmer.GetComponent<SpriteRenderer>().color = fadecolor;
            }
            else//Za Warudo
            {
                for (int i = 0; i < HUDitems.Count; i++)//Turn off HUD
                {
                    HUDitems[i].SetActive(false);
                }

                GamePaused = true;
                RandomOutfit = Random.Range(0, Robot.Count);
                CurrentOutfit = Robot[RandomOutfit];
                CurrentOutfit.GetComponent<Animator>().Play("FarmerIntro");

                if (WhichBoss == 1)
                {
                    Currentdialogue = Corporatedialogue;
                    CurrentEnemies = Businessmen;
                    EnemyDialoguetext.color = CorporateColor;
                }
                else if (WhichBoss == 2)
                {
                    Currentdialogue = Guardiandialogue;
                    CurrentEnemies = Beasts;
                    EnemyDialoguetext.color = GuardianColor;
                }
                else if (WhichBoss == 3)
                {
                    Currentdialogue = Corruptiondialogue;
                    CurrentEnemies = Corruptions;
                    CurrentEnemies[BossStage - 1].GetComponent<SpriteRenderer>().sprite = CorruptionSprites[BossStage - 1];
                    EnemyDialoguetext.color = CorruptionColor;
                }

                for (int i = 0; i < CurrentEnemies.Count; i++)//Turn on correct the enemies
                {
                    CurrentEnemies[i].SetActive(true);
                }

                CurrentEnemies[BossStage - 1].GetComponent<Animator>().Play("EnemyIntro");

                StartCoroutine(Wait());//Wait until displaying the dialogue

                Prepare = false;
            }
        }


        if (movement)
        {
            Parent.transform.Translate(Direction * Time.unscaledDeltaTime, Space.World);

            if(Parent.transform.position.y >= WhichDialogue * 2.5f + Offset)
            {
                if (!GameResuming)
                {
                    CurrentPapa = Instantiate(Papa);
                    CurrentPapa.transform.parent = Parent.transform;
                    NewDialogue.transform.parent = CurrentPapa.transform;
                }
                StartCoroutine(ButtonDelay());
                movement = false;
            }
        }

        if (CanSkipDialogue && !GameResuming)
            NextButton.SetActive(true);
        else
            NextButton.SetActive(false);

        if (GamePaused)
            Time.timeScale = 0;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3);

        NewDialogue = Instantiate(EnemyDialogue);
        Messages.Add(NewDialogue);
        CurrentText = NewDialogue.transform.GetChild(0).gameObject.GetComponent<Text>();
        CurrentText.text = Currentdialogue[WhichDialogue];
        NewDialogue.GetComponent<Animator>().Play("DialogueBoxIntro");

        GameResuming = false;
        CanSkipDialogue = false;
        movement = true;
    }

    IEnumerator Move()
    {
        GameResuming = false;
        CanSkipDialogue = false;
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
    }

    IEnumerator ButtonDelay()
    {
        yield return new WaitForSecondsRealtime(0.6f);

        CanSkipDialogue = true;
    }


    public void PreperationMode()//This is triggered by the boss
    {
        CanSkipDialogue = false;
        WhichBoss = BossDefiner.GetComponent<NewCardHandScript>().RandomBoss;
        BossStage++;
        Robotext = true;

        Prepare = true;
    }


    IEnumerator ResumeGame()
    {
        GameResuming = true;
        Offset += 8;
        CanSkipDialogue = false;
        movement = true;
        CurrentOutfit.GetComponent<Animator>().Play("FarmerOutro");
        CurrentEnemies[BossStage - 1].GetComponent<Animator>().Play("EnemyOutro");

        yield return new WaitForSecondsRealtime(2);

        fadefloat = 0;

        Color fadecolor = Dimmer.GetComponent<SpriteRenderer>().color;

        fadecolor = new Color(fadecolor.r, fadecolor.g, fadecolor.b, fadefloat);
        Dimmer.GetComponent<SpriteRenderer>().color = fadecolor;

        for (int i = 0; i < HUDitems.Count; i++)//Turn off HUD (remember to turn it back on
        {
            HUDitems[i].SetActive(true);
        }

        WhichDialogue++;

        GamePaused = false;
        Time.timeScale = 1;
    }

    public void DialogueManager()
    {
        if (WhichDialogue == 3 || WhichDialogue == 7 || WhichDialogue == 11 || WhichDialogue == 15)//Input any number after which the dialogue should end for each character in the Corporate battle
        {
            //End dialogue
            StartCoroutine(ResumeGame());
        }
        else
        {
            WhichDialogue++;
            StartCoroutine(Move());

            Robotext = !Robotext;//Swaps between wether its the dialogue or the oponent's turn to speak
        }
    }

    //I could also have a "Guardiandialogue" and "Corruptiondiologue" voids here but currently there's no reason to and this one void works fine
}
