using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private float levelcountdown = 0;
    private bool victoryEgg;
    public static int eggsSpawned = 0;//keeps track of how many eggs were spawned/destroyed
    private float delay;
    public AudioSource Eggscelent;


    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    void Start()
    {
        sizeOfEggSpawnLocationsList = eggSpawnLocationsList.Count;//Grabs the size of the list
        StartCoroutine(EggSpawner());//Starts the spawning coroutine
        score = 0;
        wintext.SetActive(false);
        eggSpawnAmount = 8;
        eggsSpawned = 0;
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
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

    void Update()
    {
        if (eggsSpawned == eggSpawnAmount)
            delay += Time.deltaTime;

        if(delay >= 2)
        {
            if(score == 8)
            {
                Win();
                victoryEgg = true;
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
            }
            else if (score == 7)
            {
                Win();
                victoryEgg = true;
                star1.SetActive(true);
                star2.SetActive(true);
            }
            else if (score == 6)
            {
                Win();
                victoryEgg = true;
                star1.SetActive(true);
            }
            else if (score <= 5)
            {
                Win();
                victoryEgg = true;
            }
        }

        if (delay < 2 && delay > 1)
        {
            Eggscelent.Play();
        }
    }
    public void Win()
    {
        goaltext.SetActive(false);
        wintext.SetActive(true);
        levelcountdown += Time.deltaTime;

        if (levelcountdown >= 5)
        {
            if(MinigameSceneScript.activeMinigame == 1)
            {
                MinigameSceneScript.activeMinigame++;
                SceneManager.LoadScene("Minigame#" + MinigameSceneScript.scene2);
            }
            else if (MinigameSceneScript.activeMinigame == 2)
            {
                MinigameSceneScript.activeMinigame++;
                SceneManager.LoadScene("Minigame#" + MinigameSceneScript.scene3);
            }
            else if (MinigameSceneScript.activeMinigame == 3)
            {
                SceneManager.LoadScene("CoreGame");
            }
        }
    }
}