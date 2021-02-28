using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [Header ("Spawner Details")]
    [Tooltip("Empty gameobjects in the locations of spawning")]public List<GameObject> spawnLocationList = new List<GameObject>();//All lane spawn markers
    [Tooltip("Enemies to be spawned")]public List<GameObject> enemyTypesSpawnList = new List<GameObject>();//All enemy prefabs
    [Tooltip("The amount of enemies the script will spawn")][Range(0,50)]public int amountOfEnemies;//The amount of enemies that is suposed to spawn
    [Range(0,25)]public float delayBetweenSpawnsMin;//The minimum time between spawns
    [Range(0,25)]public float delayBetweenSpawnsMax;//The maximum time between spawns

    private int randomLaneForSpawning; //The index number for the list of Lanes
    private int randomEnemyTypeToSpawn;//The index number for the list of enemy types
    private float randomDelayTime;//The value of the wait time between spawns
    private int loopLimit = 0; //Preventing the loop from continuing after spawning.

    public bool gameStarted = false;//Prevents the spawning before the game should start
    private int sizeOfSpawnLocations;//The highest Index in the list of lanes
    private int sizeOfEnemiesTypes;//The highest Index in the list of enemy types

    private void Start()
    {
        sizeOfSpawnLocations = spawnLocationList.Count;//Get the amount of items in the list of lanes
        sizeOfEnemiesTypes = enemyTypesSpawnList.Count;//Get the amount of items in the list of enemy types
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
        if (loopLimit < amountOfEnemies)//As long as the loop has not looped the amount of enemies to spawn GO.
        {
            for (int i = 0; loopLimit < amountOfEnemies; loopLimit++)
            {
                randomLaneForSpawning = Random.Range(0, sizeOfSpawnLocations); //Gives a random lane spawn everytime
                randomEnemyTypeToSpawn = Random.Range(0, sizeOfEnemiesTypes); //Gives a random enemy type every spawn
                Transform spawnLocation = spawnLocationList[randomLaneForSpawning].transform; //Grabs the transform of the location for spawning

                GameObject enemyObject = Instantiate(enemyTypesSpawnList[randomEnemyTypeToSpawn], transform); //Instantiates a random enemy type from the list.
                enemyObject.transform.position = spawnLocation.position; //Sets the random enemy on a random lane from the list.

                randomDelayTime = Random.Range(delayBetweenSpawnsMin, delayBetweenSpawnsMax); //Gives a random time
                yield return new WaitForSeconds(randomDelayTime);//Wait the random amount of time, before resuming the spawning.
            }
        }   
    }
}
