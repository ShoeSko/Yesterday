using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMazeSetup : MonoBehaviour
{
    [Header("Setup")]
    public GameObject playerObject;
    [SerializeField] private List<GameObject> playerStandingLocations = new List<GameObject>();
    private int playerStandCount;
    [SerializeField] private List<GameObject> playerSpawnLocations = new List<GameObject>();
    private int playerSpawnCount;

    private int currentStandingIndex;

    private void Awake()
    {
        SetupMazeButtons();
    }

    private void Start()
    {
        SetupPlayerPosition();
        print(currentStandingIndex);
    }


    public void Escape()
    {
        if (FindObjectOfType<ScoreScript>())
        {
            FindObjectOfType<ScoreScript>().win();
        }
        playerObject.SetActive(false); //Turns of the player.

        GameObject currentLocation = playerStandingLocations[currentStandingIndex];

        for (int child = 0; child < currentLocation.transform.childCount; child++) //A loop going through all the children of the location
        {
            currentLocation.transform.GetChild(child).gameObject.SetActive(false); //Turns off the button/children of the location
        }

    }

    private void WipeLastLocation()
    {
        print(currentStandingIndex);
        GameObject currentLocation = playerStandingLocations[currentStandingIndex];

        for (int child = 0; child < currentLocation.transform.childCount; child++) //A loop going through all the children of the location
        {
            currentLocation.transform.GetChild(child).gameObject.SetActive(false); //Turns off the button/children of the location
        }
    }

    public void MoveLocation(int newStandIndexLocation)
    {
        GameObject newLocation = playerStandingLocations[newStandIndexLocation];

        playerObject.transform.position = newLocation.transform.position; //Sends the player to the next location.

        for (int child = 0; child < newLocation.transform.childCount; child++) //A loop going through all the children of the location
        {
            newLocation.transform.GetChild(child).gameObject.SetActive(true); //Turns on the button/children of the location
        }

        WipeLastLocation();

        currentStandingIndex = newStandIndexLocation; //Sets the new location to current location
        print(currentStandingIndex);
    }


    private void SetupMazeButtons() //Turns off all buttons at the start
    {
        playerStandCount = playerStandingLocations.Count;

        for (int index = 0; index < playerStandCount; index++)
        {
            GameObject currentLocation = playerStandingLocations[index];

            for (int child = 0; child < currentLocation.transform.childCount; child++) //A loop going through all the children of the location
            {
                currentLocation.transform.GetChild(child).gameObject.SetActive(false); //Turns off the button/children of the location
            }
        }
    }

    private void SetupPlayerPosition()
    {
        playerSpawnCount = playerSpawnLocations.Count;
        int randomStart = Random.Range(0, playerSpawnCount - 1);
        playerObject.transform.position = playerSpawnLocations[randomStart].transform.position; //Sets the player position to 1 of three locations

        if (randomStart == 0)
        {
            currentStandingIndex = 2;

            GameObject newLocation = playerStandingLocations[currentStandingIndex];

            for (int child = 0; child < newLocation.transform.childCount; child++) //A loop going through all the children of the location
            {
                newLocation.transform.GetChild(child).gameObject.SetActive(true); //Turns on the button/children of the location
            }

        }
        else if (randomStart == 1)
        {
            currentStandingIndex = 32;

            GameObject newLocation = playerStandingLocations[currentStandingIndex];

            for (int child = 0; child < newLocation.transform.childCount; child++) //A loop going through all the children of the location
            {
                newLocation.transform.GetChild(child).gameObject.SetActive(true); //Turns on the button/children of the location
            }
        }
        else if (randomStart == 2)
        {
            currentStandingIndex = 35;

            GameObject newLocation = playerStandingLocations[currentStandingIndex];

            for (int child = 0; child < newLocation.transform.childCount; child++) //A loop going through all the children of the location
            {
                newLocation.transform.GetChild(child).gameObject.SetActive(true); //Turns on the button/children of the location
            }
        }
    }
}
