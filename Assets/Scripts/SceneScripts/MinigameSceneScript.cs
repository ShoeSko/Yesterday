using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MinigameSceneScript : MonoBehaviour
{
    [Header("MinigameList")]
    [Tooltip("The names of the minigame scenes goes into the list")]public List<String> minigameSceneList = new List<String>(); //A list to keep track of all minigame scene names
    int sizeOfMinigameList; //A variable to hold the size of the list

    private void Start()
    {
        sizeOfMinigameList = minigameSceneList.Count; //Grabs the size of the list and sets it.
    }

    public void RandomMinigameScene() //Activate this to start a random scene within the list of minigames
    {
        int randomSceneValue = UnityEngine.Random.Range(0, sizeOfMinigameList -1); //Creates a random int value within the range of the list size.
        string minigameSceneName = minigameSceneList[randomSceneValue]; //Grabs the corresponding string value to the random value chosen.

        SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Single); //Load the chosen scene
    }
    //To note, this code will not accept that there is only 1 string in the list. (A if statement can fix this, but the concept of only having 1 minigame scene makes this script obsolete.


}
