using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quacken : MonoBehaviour
{
    public GameObject quackenButton;
    public GameObject quacks;
    private int AmountOfQuacks = 20;

    public List<GameObject> spawnLocationList = new List<GameObject>();
    private int sizeOfSpawnLocations;
    private int randomLaneForSpawning;

    private void Start()
    {
        sizeOfSpawnLocations = spawnLocationList.Count;

        quackenButton.SetActive(false);
    }
    public void Release_The_Quacken()
    {
        StartCoroutine(quackenSpawner());
        //do quacken stuff
        quackenButton.SetActive(false);
    }

    IEnumerator quackenSpawner()
    {
        for (int SpawnedQuacks = 0; SpawnedQuacks < AmountOfQuacks; SpawnedQuacks++)
        {
            randomLaneForSpawning = Random.Range(0, sizeOfSpawnLocations);
            Transform spawnLocation = spawnLocationList[randomLaneForSpawning].transform;

            GameObject quackObject = Instantiate(quacks);
            quackObject.transform.position = spawnLocation.position;

            yield return new WaitForSeconds(0.4f);
        }
    }

    public void startGame()
    {
        quackenButton.SetActive(true);
    }
}