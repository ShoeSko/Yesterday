using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDialogue : MonoBehaviour
{
    public static int Boss;//0 = Corp, 1 = Moth, 2 = Corruption

    public List<GameObject> bossObject = new List<GameObject>();
    public GameObject Robot;

    private bool movement;

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

    public GameObject Parent;
    public GameObject Papa;
    private GameObject CurrentPapa;
    private Vector3 Direction = new Vector3(0, 10f, 0);
    private int Offset;

    private bool Robotext = true;
    private bool CanSkipDialogue;

    public GameObject NextButton;
    public GameObject FinishButton;


    void Start()
    {
        //cheatcode for testing
        Boss = 1;

        bossObject[Boss].SetActive(true);

        bossObject[Boss].GetComponent<Animator>().Play("EnemyIntro");
        Robot.GetComponent<Animator>().Play("FarmerIntro");

        PreperationMode();
    }

    private void Update()
    {
        if (movement)
        {
            Parent.transform.Translate(Direction * Time.unscaledDeltaTime, Space.World);

            if (Parent.transform.position.y >= WhichDialogue * 2.5f + Offset)
            {
                CurrentPapa = Instantiate(Papa);
                CurrentPapa.transform.parent = Parent.transform;
                NewDialogue.transform.parent = CurrentPapa.transform;

                StartCoroutine(ButtonDelay());
                movement = false;
            }
        }

        if (CanSkipDialogue)
            NextButton.SetActive(true);
        else
            NextButton.SetActive(false);


        if (WhichDialogue == 5)
        {
            bossObject[Boss].GetComponent<Animator>().Play("EnemyOutro");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(3);

        NewDialogue = Instantiate(EnemyDialogue);
        Messages.Add(NewDialogue);
        CurrentText = NewDialogue.transform.GetChild(0).gameObject.GetComponent<Text>();
        Debug.Log("I got here4");
        CurrentText.text = Currentdialogue[WhichDialogue];
        NewDialogue.GetComponent<Animator>().Play("DialogueBoxIntro");

        CanSkipDialogue = false;
        movement = true;
    }

    IEnumerator Move()
    {
        CanSkipDialogue = false;
        movement = true;

        if (Robotext)
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
        if (Boss == 0)
        {
            Currentdialogue = Corporatedialogue;
        }
        else if (Boss == 1)
        {
            Currentdialogue = Guardiandialogue;
        }
        else if (Boss == 2)
        {
            Currentdialogue = Corruptiondialogue;
        }

        Robotext = true;

        StartCoroutine(Wait());//Wait until displaying the dialogue
    }

    public void DialogueManager()
    {
        if (WhichDialogue == 5)//Input any number after which the dialogue should end for each character in the Corporate battle
        {
            //End dialogue
            Robot.GetComponent<Animator>().Play("FarmerOutro");
            FinishButton.GetComponent<LevelTransitionSystem>().VictoryButtonPress();
        }
        else
        {
            WhichDialogue++;
            StartCoroutine(Move());

            Robotext = !Robotext;//Swaps between wether its the dialogue or the oponent's turn to speak
        }
    }

}
