using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoombaPower : MonoBehaviour
{
    #region Variables
    [Header("Material Looks")]
    public Color powerOn;
    [Header("List of the wires to be used")]
    public List<GameObject> connection0 = new List<GameObject>();
    public List<GameObject> connection1 = new List<GameObject>();
    public List<GameObject> connection2 = new List<GameObject>();
    public List<GameObject> connection3 = new List<GameObject>();

    [Header("Switches being used")]
    public List<GameObject> switches1 = new List<GameObject>();
    public GameObject switch2;
    public List<GameObject> switches3 = new List<GameObject>();
    private float flipStrenght = 270;

    private bool firstSwitch;
    private bool secondSwitch;
    private bool thirdSwitch;

    [Header("Star system")]
    public List<GameObject> stars = new List<GameObject>(); //A list of the stars
    public GameObject blackstar1;
    public GameObject blackstar2;
    public GameObject blackstar3;
    public GameObject fadeUIBox;
    private int starLenght;//How long is the star list
    private float scoreTimer; //How long it takes to do the game
    public int star1Time;
    public int star2Time;
    public int star3Time;

    [Header("The Roomba")]
    public GameObject roombaObject;
    public int roombaSpeed;
    private int roombaflipInt = 1;

    private int randomiser1;
    private int randomiser2;

    private bool win;
    private float delay;
    private int score;
    private bool onlyOnce2ndSwithc;
    private bool onlyOnce3rdSwithc;

    [Header("Scene Transition")]
    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  

    [Header("Time Limit Minigame")]
    [SerializeField] private float minigameTimeLimit = 30f;
    private bool timeIsUp;

    [Header("Audio")]
    private bool audio1Played = false;
    public AudioSource RoombaSound;
    #endregion

    private void Start()
    {
        win = false;
        score = 0;

        blackstar1.SetActive(false);
        blackstar2.SetActive(false);
        blackstar3.SetActive(false);
        fadeUIBox.SetActive(false);
        FirstLineInitialization();

        starLenght = stars.Count;
        for (int i = 0; i < starLenght; i++)
        {
            stars[i].SetActive(false);
        }

        StartCoroutine(TimerForMinigame());
    }

    private void FirstLineInitialization()
    {
        randomiser1 = Random.Range(0, 3);
        randomiser2 = Random.Range(0, 3);
        connection0[randomiser1].transform.GetChild(0).gameObject.SetActive(false); //Turns of the Line that is a child of the wire
        connection0[randomiser1].GetComponent<SpriteRenderer>().color = powerOn; //Choses the first wire.
        connection0[randomiser1].GetComponent<Animator>().enabled = true; //Activates the animation that will loop eternally
        for (int i = 0; i < switches1.Count; i++)
        {
            switches1[i].transform.GetComponentInChildren<Animator>().SetTrigger("Start"); //Starts animation on first layer
        }
    }

    private void Update()
    {
        if(!win)
            scoreTimer += Time.deltaTime;

        WhichSwitch();

        if (win || timeIsUp)
        {
            WinSound();
            nextSceneButton.SetActive(true);
            levelTransitioner.currentMinigameScore = score;
        }
    }

    private void WhichSwitch()
    {
        //Switch 1
        if(switches1[randomiser1].transform.eulerAngles.z == flipStrenght)
        {
            ActivateFirstConnection();
            firstSwitch = true;

            for (int i = 0; i < switches1.Count; i++)
            {
                switches1[i].layer = 2; //Gives the object ignore raycast, prevents more flipping.
                switches1[i].transform.GetComponentInChildren<Animator>().SetTrigger("End"); //Stops the last animation run.
            }
            if (!onlyOnce2ndSwithc)
            {
                switch2.transform.GetComponentInChildren<Animator>().SetTrigger("Start"); //Start the new animation run.
                onlyOnce2ndSwithc = true;
            }
        }

        //Switch 2
        if (switch2.transform.eulerAngles.z == flipStrenght && firstSwitch)
        {
            ActivateSecondConnection();
            secondSwitch = true;
            switch2.layer = 2;
            switch2.transform.GetComponentInChildren<Animator>().SetTrigger("End"); //Ends the second animation run
            if (!onlyOnce3rdSwithc) //Prevents this part of the code to be contionously sent
            {
                for (int i = 0; i < switches3.Count; i++)
                {
                    switches3[i].transform.GetComponentInChildren<Animator>().SetTrigger("Start"); //Starts the last animation run
                }
                onlyOnce3rdSwithc = true;
            }

        }

        //Switch 3
        if (switches3[randomiser2].transform.eulerAngles.z == flipStrenght && secondSwitch)
        {
            ActivateThirdConnection();

            RoombaTime();

            for (int i = 0; i < switches3.Count; i++)
            {
                switches3[i].layer = 2;
                switches3[i].transform.GetComponentInChildren<Animator>().SetTrigger("End"); //Ends the last animation run
            }
        }
    }

    private void RoombaTime()
    {
        if (!thirdSwitch || timeIsUp)
        {
            win = true;
            if (scoreTimer <= star1Time)//define score for this minigame
            {
                for (int i = 0; i < starLenght; i++)
                {
                    stars[i].SetActive(true);
                }
                score = 3;
                //3 stars
                CardReward.Stars = 3;
            }
            else if (scoreTimer > star1Time && scoreTimer <= star2Time)
            {
                for (int i = 0; i < starLenght - 1; i++)
                {
                    stars[i].SetActive(true);
                }
                blackstar3.SetActive(true);
                score = 2;
                //2 stars
                CardReward.Stars = 2;
            }
            else if (scoreTimer > star2Time && scoreTimer <= star3Time)
            {
                for (int i = 0; i < starLenght - 2; i++)
                {
                    stars[i].SetActive(true);
                }
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
                score = 1;
                //1 star
                CardReward.Stars = 1;
            }
            else if (scoreTimer > star3Time)
            {
                blackstar1.SetActive(true);
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
                score = 0;
                //0 stars
            }
        }
        fadeUIBox.SetActive(true);
        RoombaMove();

        thirdSwitch = true;//Prevents star script from repeating.
    }

    private void RoombaMove() //Tiny piece to make the roomba goe back and forth after completing the circuit
    {
        Rigidbody2D roombaRD= roombaObject.GetComponent<Rigidbody2D>();
        roombaRD.velocity = new Vector2(roombaSpeed * roombaflipInt, 0);
        if (roombaObject.transform.position.x > 7.5)
        {
            roombaflipInt = -1;
        }
        else if (roombaObject.transform.position.x < -7.5)
        {
            roombaflipInt = 1;
        }
    }
    #region Connections
    private void ActivateFirstConnection() //Turns the wires on (Might just be a object with 2 anim states instead
    {
        connection1[randomiser1].transform.GetChild(0).gameObject.SetActive(false);
        connection1[randomiser1].GetComponent<SpriteRenderer>().color = powerOn;
        connection1[randomiser1].GetComponent<Animator>().enabled = true;
    }
    private void ActivateSecondConnection()
    {
        connection2[randomiser2].transform.GetChild(0).gameObject.SetActive(false);
        connection2[randomiser2].GetComponent<SpriteRenderer>().color = powerOn;
        connection2[randomiser2].GetComponent<Animator>().enabled = true;
    }
    private void ActivateThirdConnection()
    {
        connection3[randomiser2].transform.GetChild(0).gameObject.SetActive(false);
        connection3[randomiser2].GetComponent<SpriteRenderer>().color = powerOn;
        connection3[randomiser2].GetComponent<Animator>().enabled = true;
    }
    #endregion

    IEnumerator TimerForMinigame()
    {
        yield return new WaitForSeconds(minigameTimeLimit);

        timeIsUp = true;
        RoombaTime();
    }

    private void WinSound()
    {
        if (!audio1Played)
        {
            RoombaSound.Play();
            audio1Played = true;
        }

    }
}
