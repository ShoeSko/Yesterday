using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalVictory : MonoBehaviour
{

    public List<GameObject> stars = new List<GameObject>(); //A list of the stars
    private int starLenght;//How long is the star list
    private float scoreTimer; //How long it takes to do the game
    public static int _animalPensFilled; //How many pens have been filled.

    public int star1Time;
    public int star2Time;
    public int star3Time;

    [Header("Level Transition")]
    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int score;

    private bool GameStarted;

    //Tutorial
    public GameObject ObjectiveText;
    public GameObject minigameAnimals;
    public GameObject minigamePens;
    public GameObject BotWave;
    public GameObject SpeechBubble;
    public GameObject ContinueText;
    public List<GameObject> Texts = new List<GameObject>();
    private int Text;



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
    }
    private void Update()
    {
        if(GameStarted == true)//if its not tutorial
            AllAnimalsInPens();

        if(MinigameSceneScript.Tutorial == true)//tutorial settings
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                if(GameStarted == false)
                {
                    Texts[Text].SetActive(false);
                    Text++;

                    if(Text != 2)
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

        if (_animalPensFilled == 4)
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
                    //1 star
                    CardReward.Stars = 1;
                    score = 1;
                }
                else if (scoreTimer > 15)
                {
                    //0 stars
                }

                nextSceneButton.SetActive(true);
                levelTransitioner.currentMinigameScore = score;
            }
            else//Tutorial settings
            {
                for (int i = 0; i < starLenght - 1; i++)
                {
                    stars[i].SetActive(true);
                }
                //2 stars
                CardReward.Stars = 2;
                score = 2;

                BotWave.SetActive(true);
                SpeechBubble.SetActive(true);
                ContinueText.SetActive(true);
                Texts[Text].SetActive(true);

                ObjectiveText.SetActive(false);
                minigameAnimals.SetActive(false);

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
}
