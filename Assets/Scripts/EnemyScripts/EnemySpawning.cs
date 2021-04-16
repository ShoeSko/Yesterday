using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [Header ("Spawner Details")]
    [Tooltip("Empty gameobjects in the locations of spawning")]public List<GameObject> spawnLocationList = new List<GameObject>();//All lane spawn markers
    [Tooltip("The amount of enemies the script will spawn")][Range(0,50)]public int amountOfEnemies;//The amount of enemies that is suposed to spawn
    [Range(0,25)]public float delayBetweenSpawnsMin;//The minimum time between spawns
    [Range(0,25)]public float delayBetweenSpawnsMax;//The maximum time between spawns


    private int randomLaneForSpawning; //The index number for the list of Lanes
    private int randomEnemyTypeToSpawn;//The index number for the list of enemy types
    private float randomDelayTime;//The value of the wait time between spawns
    private int loopLimit = 0; //Preventing the loop from continuing after spawning.

    public bool gameStarted = false;//Prevents the spawning before the game should start //THIS SHOULD POTENTIALY BE REMOVED UNLESS WE STILL WANT TO CONFIRM THE START OF MAIN GAME:

    [SerializeField]private List<GameObject> weakEnemyTypesSpawnList = new List<GameObject>();//Becomes the Weak Enemylist.
    [SerializeField]private List<GameObject> strongEnemyTypesSpawnList = new List<GameObject>();//Becomes the Strong Enemylist.
    [Tooltip("How many weak enemies are to spawn before the strong enemies can spawn?")][Range(0, 50)] public int strongEnemySpawnWait;//The minimum amount of enemies that spawn before strong enemies can spawn.
    private int sizeOfSpawnLocations;//The highest Index in the list of lanes
    private int sizeOfWeakEnemyTypes;//The highest Index in the list of weak enemy types
    private int sizeOfStrongEnemyTypes;//The highest Index in the list of strong enemy types


    [Tooltip("Weak Enemies to be spawned")][SerializeField] private EnemyList weakEnemyList;
    [Tooltip("Strong Enemies to be spawned")][SerializeField] private EnemyList strongEnemyList;
    private void Start()
    {
        ListNewModelMake(); //Runs script to make all scripts belong to this script for the run.
        RemodelingOfLists(); //Restructures the lists to fit the current run spawn wish. (Removes enemies that are not to spawn)
        Failsafe(); //In case setting that can be wrong without breaking the code occur.
        sizeOfSpawnLocations = spawnLocationList.Count;//Get the amount of items in the list of lanes

    }
    private void Update()
    {
        if (gameStarted)//If the game has started
        {
            StartCoroutine(EnemySpawnDelayTimer());//Start the coroutine
            gameStarted = false;//Stop the coroutine from being repeatedly started
        }
    }

    IEnumerator EnemySpawnDelayTimer()
    {
        if (loopLimit < amountOfEnemies)//As long as the loop has not looped the amount of enemies to spawn GO. Might be obsolete
        {
            int strongSpawnNumber = Random.Range(strongEnemySpawnWait, amountOfEnemies); //When to spawn the strong enemy

            for (int loopLimit = 0; loopLimit < amountOfEnemies; loopLimit++)
            {
                randomLaneForSpawning = Random.Range(0, sizeOfSpawnLocations); //Gives a random lane spawn everytime
                randomEnemyTypeToSpawn = Random.Range(0, sizeOfWeakEnemyTypes); //Gives a random enemy type of weak every spawn
                Transform spawnLocation = spawnLocationList[randomLaneForSpawning].transform; //Grabs the transform of the location for spawning

                if (loopLimit == strongSpawnNumber)
                {
                    GameObject enemyObject = Instantiate(strongEnemyTypesSpawnList[0], transform); //Instantiates a random enemy type from the list.
                    enemyObject.transform.position = spawnLocation.position; //Sets the random enemy on a random lane from the list.
                }
                else
                {
                GameObject enemyObject = Instantiate(weakEnemyTypesSpawnList[randomEnemyTypeToSpawn], transform); //Instantiates a random enemy type from the list.
                enemyObject.transform.position = spawnLocation.position; //Sets the random enemy on a random lane from the list.
                }

                randomDelayTime = Random.Range(delayBetweenSpawnsMin, delayBetweenSpawnsMax); //Gives a random time
                yield return new WaitForSeconds(randomDelayTime);//Wait the random amount of time, before resuming the spawning.
            }
        }   
    }

    public void SpawnEnemies()
    {
        gameStarted = true;
    }

    private void RemodelingOfLists()
    {
        int enemyToRemove = Random.Range(0, sizeOfWeakEnemyTypes - 1); //What enemy to remove from the list
        weakEnemyTypesSpawnList.Remove(weakEnemyTypesSpawnList[enemyToRemove]); //Remove the enemy
        sizeOfWeakEnemyTypes = weakEnemyTypesSpawnList.Count;//Get the amount of items in the list of weak enemy types after the removal

        int enemyToKeep = Random.Range(0, sizeOfStrongEnemyTypes - 1); //What enemy to keep on the list
        for (int i = 0; i < sizeOfStrongEnemyTypes-1; i++)
        {
            if (i != enemyToKeep)
            {
                strongEnemyTypesSpawnList.Remove(strongEnemyTypesSpawnList[i]); //Remove all but the 1 strong enemy
            }
        }
        sizeOfStrongEnemyTypes = strongEnemyTypesSpawnList.Count; //Get the amount of items in the list of strong enemy types after the removal
    }

    private void ListNewModelMake()
    {
        sizeOfWeakEnemyTypes = weakEnemyList.Enemies.Count;
        for (int i = 0; i < sizeOfWeakEnemyTypes; i++) //Adds the scriptable object items to a list in this script for editing
        {
            weakEnemyTypesSpawnList.Add(weakEnemyList.Enemies[i]);
        }

        sizeOfStrongEnemyTypes = strongEnemyList.Enemies.Count;
        for (int i = 0; i < sizeOfStrongEnemyTypes-1; i++)
        {
            strongEnemyTypesSpawnList.Add(strongEnemyList.Enemies[i]);
        }
    }

    private void Failsafe()
    {
        if(amountOfEnemies < strongEnemySpawnWait)
        {
            print("StrongEnemySpawnWait variable is " + strongEnemySpawnWait + " which is bigger than amountOfEnemies which is " + amountOfEnemies + " please fix this, as that should not be the case");
            strongEnemySpawnWait = amountOfEnemies-2;
        }
    }
}
