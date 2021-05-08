using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class MinigameSceneScript : MonoBehaviour
{
    int sizeOfMinigameList = 13; //A variable to hold the size of the list - 1

    static public int scene1;
    static public int scene2;
    static public int scene3;
    
    static public int activeMinigame = 1;

    private int randomSceneValue;

    private GameObject MinigameMusic;

    
    private void Start()
    {
        MinigameMusic = GameObject.Find("MinigameMusic");
        MinigameMusic.SetActive(false);
    }

    public void RandomMinigameScene() //Activate this to start a random scene within the list of minigames
    {
        if(NewCardHandScript.Stage != 4)
        {
            activeMinigame = 1;
            StartCoroutine(minigameRandomizer());
        }
    }

    IEnumerator minigameRandomizer()
    {
        for (int randomize = 0; randomize < 1; randomize++)
        {
            randomSceneValue = UnityEngine.Random.Range(1, sizeOfMinigameList); //Creates a random int value within the range of the list size.
            scene1 = randomSceneValue;

            randomSceneValue = UnityEngine.Random.Range(1, sizeOfMinigameList); //Creates a random int value within the range of the list size.
            scene2 = randomSceneValue;

            randomSceneValue = UnityEngine.Random.Range(1, sizeOfMinigameList); //Creates a random int value within the range of the list size.
            scene3 = randomSceneValue;

            if (scene2 == scene1)
                randomize--;
            else if (scene3 == scene1)
                randomize--;
            else if (scene2 == scene3)
                randomize--;

            Debug.Log(scene1);
            Debug.Log(scene2);
            Debug.Log(scene3);

            if (randomize == 0)
            {
                print("i load scene");

                MinigameMusic.SetActive(true);
                DontDestroyOnLoad(MinigameMusic);

                SceneManager.LoadScene("Minigame#" + scene1);//it's important that all minigames are called the same
            }

            yield return new WaitForSeconds(0);
        }
    }

    public void DeckClear()
    {
        DeckScript.Deck.Clear();
        NewCardHandScript.Stage = 1;
    }
}