using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggCollectionSpawning : MonoBehaviour
{
    [Header("Egg Spawn Controls")]
    [Tooltip("The amount of eggs to spawn")]public int eggSpawnAmount;//The amount of eggs to spawn
    [Tooltip("Place empty objects to mark the spawn locations")]public List<GameObject> eggSpawnLocationsList = new List<GameObject>(); //A list of egg spawning locations through empty objects
    [Tooltip("The clickable Egg prefab here")]public GameObject eggPrefab;//Egg prefab object

    [Range(0, 25)] public float delayBetweenSpawnsMin;//The minimum time between spawns
    [Range(0, 25)] public float delayBetweenSpawnsMax;//The maximum time between spawns

    private float randomDelayForSpawn;
    private int sizeOfEggSpawnLocationsList; //The max size of the eggSpawnLocationList
    private int randomEggSpawnLocation;//Random Index for the location list
    private int lastSpawn = 9999;//The last spawn location, set high to not predetermine first spawn
    public static int score = 0;
    public GameObject wintext;
    public GameObject goaltext;

    private bool victoryEgg;
    public static int eggsSpawned = 0;//keeps track of how many eggs were spawned/destroyed
    private float delay;
    public AudioSource Eggscelent;

    private float StartDelay;
    private bool GameStarted;

    [SerializeField] private List<Image> eggList = new List<Image>();
    private int eggsListedUp;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject blackstar1;
    public GameObject blackstar2;
    public GameObject blackstar3;
    public GameObject FadeboxUI;

    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.
    private int minigameScore;

    //Tutorial stuff
    public List<GameObject> Texts = new List<GameObject>();
    private int Text;
    public GameObject RobotSmile1;
    public GameObject SpeechBubble;
    public GameObject continueText;
    private bool gameHasStarted;

    [Header("Time Limit Minigame")]
    [SerializeField] private float minigameTimeLimit = 30f;

    [Header("Backgrounds")]
    [SerializeField] private GameObject background1;
    [SerializeField] private GameObject background2;
    void Start()
    {
        //Prep
        score = 0;
        wintext.SetActive(false);
        //eggSpawnAmount = 8;  //Things like this in code tends to make it less flexible. Just to let you know.
        eggsSpawned = 0;
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        blackstar1.SetActive(false);
        blackstar2.SetActive(false);
        blackstar3.SetActive(false);
        FadeboxUI.SetActive(false);

        if (MinigameSceneScript.Tutorial == true)
        {
            continueText.SetActive(true);
            Text = 0;
            RobotSmile1.SetActive(true);
            goaltext.SetActive(false);
            Texts[Text].SetActive(true);
            SpeechBubble.SetActive(true);
        }
        else
        {
            GameStarted = true;
        }
    }

    private void RandomBackground()
    {
        int randomNumber = Random.Range(1, 3);

        if(randomNumber == 1)
        {
            background1.SetActive(true);
            background2.SetActive(false);
        }
        else if (randomNumber == 2)
        {
            background1.SetActive(false);
            background2.SetActive(true);
        }
    }
    IEnumerator EggSpawner()
    {
            for (int i = 0; i < eggSpawnAmount; i++)
            {
            if (!victoryEgg)
            {
                randomEggSpawnLocation = Random.Range(0, sizeOfEggSpawnLocationsList);//Gives a random spawn location Index
                if (lastSpawn == randomEggSpawnLocation && randomEggSpawnLocation == sizeOfEggSpawnLocationsList) { randomEggSpawnLocation++; } //If the last spawn was the same as the new random, and it is the highest number -1
                else if (lastSpawn == randomEggSpawnLocation && randomEggSpawnLocation == 0) { randomEggSpawnLocation++; } //If the last spawn was the same as the new random one, and it is the lowest number +1

                Transform spawnLocation = eggSpawnLocationsList[randomEggSpawnLocation].transform; //Grab the transform of the random location from the list
                GameObject Egg = Instantiate(eggPrefab, transform);//Instantiate the Egg prefab
                Egg.transform.position = spawnLocation.position;//Set the Egg location to the random location

                randomDelayForSpawn = Random.Range(delayBetweenSpawnsMin, delayBetweenSpawnsMax);

                lastSpawn = randomEggSpawnLocation;//Set the last spawn to the current latest spawn
                yield return new WaitForSeconds(randomDelayForSpawn);//Wait for random time for next egg
            }
            else eggSpawnAmount = i;
            }
    }

    public void EggCollected()
    {
        for (int eggNumber = 0; eggNumber < eggSpawnAmount; eggNumber++)
        {
            if(eggNumber == eggsListedUp)
            {
                eggList[eggNumber].color = Color.green;
            }
        }
        eggsListedUp++;
    }

    public void EggLost()
    {
        for (int eggNumber = 0; eggNumber < eggSpawnAmount; eggNumber++)
        {
            if(eggNumber == eggsListedUp)
            {
                eggList[eggNumber].color = Color.red;
            }
        }
        eggsListedUp++;
    }

    void Update()
    {
        //Tutorial part

        if(MinigameSceneScript.Tutorial == true && gameHasStarted == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                if(Text != 9)
                {
                    Texts[Text].SetActive(false);
                    Text++;
                }
                if (Text != 6 && Text != 10)
                    Texts[Text].SetActive(true);
                else if(Text == 6)
                {
                    RobotSmile1.SetActive(false);
                    SpeechBubble.SetActive(false);
                    continueText.SetActive(false);
                    goaltext.SetActive(true);
                    GameStarted = true;
                    gameHasStarted = true;
                }
            }

            if(Text == 9)
            {
                nextSceneButton.SetActive(true);
                continueText.SetActive(false);
                levelTransitioner.currentMinigameScore = minigameScore;
            }
        }

        //Actual minigame

        if(GameStarted == true)
            StartDelay += Time.deltaTime;

        if(StartDelay >= 3)
        {
            sizeOfEggSpawnLocationsList = eggSpawnLocationsList.Count;//Grabs the size of the list
            StartCoroutine(EggSpawner());//Starts the spawning coroutine
            GameStarted = false;
            StartDelay = 0;
        }


        if (eggsSpawned == eggSpawnAmount)
            delay += Time.deltaTime;

        if(delay >= 2)
        {
            if(MinigameSceneScript.Tutorial == false)
            {
                if (score == 6)
                {
                    Win();
                    victoryEgg = true;
                    star1.SetActive(true);
                    star2.SetActive(true);
                    star3.SetActive(true);
                    CardReward.Stars = 3;
                    minigameScore = 3;
                }
                else if (score == 5)
                {
                    Win();
                    victoryEgg = true;
                    star1.SetActive(true);
                    star2.SetActive(true);
                    blackstar3.SetActive(true);
                    CardReward.Stars = 2;
                    minigameScore = 2;
                }
                else if (score == 4)
                {
                    Win();
                    victoryEgg = true;
                    star1.SetActive(true);
                    blackstar2.SetActive(true);
                    blackstar3.SetActive(true);
                    CardReward.Stars = 1;
                    minigameScore = 1;
                }
                else if (score <= 3)
                {
                    Win();
                    victoryEgg = true;
                    blackstar1.SetActive(true);
                    blackstar2.SetActive(true);
                    blackstar3.SetActive(true);
                }
            }
            else//Tutorial stuff
            {
                Win();
                victoryEgg = true;
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                CardReward.Stars = 3;
                minigameScore = 3;
            }
            FadeboxUI.SetActive(true);
        }

        if (delay < 2 && delay > 1)
        {
            Eggscelent.Play();
        }
    }
    public void Win()
    {
        if(MinigameSceneScript.Tutorial == false)
        {
            goaltext.SetActive(false);
            wintext.SetActive(true);

            nextSceneButton.SetActive(true);
            levelTransitioner.currentMinigameScore = minigameScore;
        }
        else//Tutorial stuff
        {
            if(Text == 6)
            {
                CardReward.TutorialMission = 1;
                goaltext.SetActive(false);
                RobotSmile1.SetActive(true);
                SpeechBubble.SetActive(true);
                continueText.SetActive(true);
                Text++;
                Texts[Text].SetActive(true);
                gameHasStarted = false;
            }
        }
    }
}