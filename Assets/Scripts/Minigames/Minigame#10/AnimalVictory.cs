using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalVictory : MonoBehaviour
{
    [Header("Stars")]
    public List<GameObject> stars = new List<GameObject>(); //A list of the stars
    private int starLenght;//How long is the star list
    private float scoreTimer; //How long it takes to do the game
    public static int _animalPensFilled; //How many pens have been filled.

    public GameObject blackstar1;
    public GameObject blackstar2;
    public GameObject blackstar3;

    public int star1Time;
    public int star2Time;
    public int star3Time;

    [Header("Level Transition")]
    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int score;

    private bool GameStarted;

    //Tutorial
    [Header("Tutorial")]
    public GameObject ObjectiveText;
    public GameObject minigameAnimals;
    public GameObject minigamePens;
    public GameObject BotWave;
    public GameObject SpeechBubble;
    public GameObject ContinueText;
    public List<GameObject> Texts = new List<GameObject>();
    private int Text;

    [Header("Time Limit Minigame")]
    [SerializeField] private float minigameTimeLimit = 30f;
    private bool timeIsUp;

    private void Awake()
    {
        _animalPensFilled = 0; //Resets the game
    }

    private void Start()
    {
        starLenght = stars.Count;
        for (int i = 0; i < starLenght; i++)
        {
            stars[i].SetActive(false);
        }
        blackstar1.SetActive(false);
        blackstar2.SetActive(false);
        blackstar3.SetActive(false);

        if (MinigameSceneScript.Tutorial == true)
        {
            GameStarted = false;

            ObjectiveText.SetActive(false);
            minigameAnimals.SetActive(false);
            minigamePens.SetActive(false);

            BotWave.SetActive(true);
            SpeechBubble.SetActive(true);
            ContinueText.SetActive(true);

            Text = 0;
            Texts[Text].SetActive(true);
        }
        else
            GameStarted = true;

        if (!MinigameSceneScript.Tutorial)
        {
            StartCoroutine(TimerForMinigame());
        }
    }
    private void Update()
    {
        if(GameStarted == true)//if its not tutorial
            AllAnimalsInPens();

        RunTutorial();

    }

    private void RunTutorial()
    {
        if (MinigameSceneScript.Tutorial == true)//tutorial settings
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (GameStarted == false)
                {
                    Texts[Text].SetActive(false);
                    Text++;

                    if (Text != 2)
                        Texts[Text].SetActive(true);
                    else
                    {
                        ObjectiveText.SetActive(true);
                        minigameAnimals.SetActive(true);
                        minigamePens.SetActive(true);

                        GameStarted = true;

                        BotWave.SetActive(false);
                        SpeechBubble.SetActive(false);
                        ContinueText.SetActive(false);
                    }
                }
            }
        }
    }
    private void AllAnimalsInPens()
    {
        if(_animalPensFilled != 4)
            scoreTimer += Time.deltaTime;

        if (_animalPensFilled == 4 || timeIsUp)
        {
            if(MinigameSceneScript.Tutorial == false)
            {
                if (scoreTimer <= star1Time)//define score for this minigame
                {
                    for (int i = 0; i < starLenght; i++)
                    {
                        stars[i].SetActive(true);
                    }
                    //3 stars
                    CardReward.Stars = 3;
                    score = 3;
                }
                else if (scoreTimer > star1Time && scoreTimer <= star2Time)
                {
                    for (int i = 0; i < starLenght - 1; i++)
                    {
                        stars[i].SetActive(true);
                    }
                    blackstar3.SetActive(true);
                    //2 stars
                    CardReward.Stars = 2;
                    score = 2;
                }
                else if (scoreTimer > star2Time && scoreTimer <= star3Time)
                {
                    for (int i = 0; i < starLenght - 2; i++)
                    {
                        stars[i].SetActive(true);
                    }
                    blackstar2.SetActive(true);
                    blackstar3.SetActive(true);
                    //1 star
                    CardReward.Stars = 1;
                    score = 1;
                }
                else if (scoreTimer > star3Time)
                {
                    blackstar1.SetActive(true);
                    blackstar2.SetActive(true);
                    blackstar3.SetActive(true);
                    //0 stars
                }

                nextSceneButton.SetActive(true);
                levelTransitioner.currentMinigameScore = score;
            }
            else//Tutorial Victory
            {
                for (int i = 0; i < starLenght - 1; i++)
                {
                    stars[i].SetActive(true);
                }
                blackstar3.SetActive(true);
                //2 stars
                CardReward.Stars = 2;
                score = 2;

                BotWave.SetActive(true);
                SpeechBubble.SetActive(true);
                Texts[Text].SetActive(true);

                ObjectiveText.SetActive(false);
                //minigameAnimals.SetActive(false);  //What is the reason to turn of the animals upon victory??

                CardReward.TutorialMission = 2;
                nextSceneButton.SetActive(true);
                levelTransitioner.currentMinigameScore = score;
            }
        }
    }
    private void OnDestroy()
    {
        _animalPensFilled = 0; //Resets the game
    }

    IEnumerator TimerForMinigame()
    {
        yield return new WaitForSeconds(minigameTimeLimit);

        timeIsUp = true;
    }
}
