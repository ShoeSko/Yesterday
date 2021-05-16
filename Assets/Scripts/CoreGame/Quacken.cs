using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quacken : MonoBehaviour
{
    public GameObject TutorialHand;

    public GameObject quackenButton;
    public GameObject quacks;
    private int AmountOfQuacks = 20;

    static public bool s_quackenBeenReleased = false; // Prevensts the Quacken from reapering more than it should.

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

        //Tutorial settings
        if (MinigameSceneScript.Tutorial == true)
        {
            TutorialHand.GetComponent<NewCardHandScript>().TutorialQuacken();
        }

        s_quackenBeenReleased = true;
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
        if(s_quackenBeenReleased == false)
            quackenButton.SetActive(true);
    }

    private void Update()
    {
        if (s_quackenBeenReleased)
        {
            quackenButton.SetActive(false);
        }
    }
}