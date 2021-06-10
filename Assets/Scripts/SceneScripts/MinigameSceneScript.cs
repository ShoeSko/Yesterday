using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class MinigameSceneScript : MonoBehaviour
{
    int sizeOfMinigameList = 13; //A variable to hold the size of the list - 1

    public static bool Tutorial;//Changes how the game plays (turns on tutorial mode)

    static public int scene1;
    static public int scene2;
    static public int scene3;
    
    static public int activeMinigame = 1;

    private int randomSceneValue;

    private GameObject MinigameMusic;
    private GameObject TutorialMusic;

    private AudioSource MinigameOST;
    private AudioSource TutorialOST;

    [SerializeField] private LevelTransitionSystem levelTransition;

    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject tutorialButton;
    [SerializeField] private GameObject tutorialPlayButton;

    
    private void Start()
    {
        MinigameMusic = GameObject.Find("MinigameMusic");
        TutorialMusic = GameObject.Find("TutorialMusic");

        if (MinigameMusic)
        {
            MinigameOST = MinigameMusic.GetComponent<AudioSource>();
            MinigameOST.Stop();
        }

        if (TutorialMusic && Tutorial == false)
        {
            TutorialOST = TutorialMusic.GetComponent<AudioSource>();
            TutorialOST.Stop();
        }

        CheckIfTutorialHasRun();
    }


    public void RandomMinigameScene() //Activate this to start a random scene within the list of minigames
    {
        if(NewCardHandScript.Stage != 4)
        {
            GameObject.Find("MinigameMusic").GetComponent<AudioSource>().Play();
            activeMinigame = 1;
            StartCoroutine(minigameRandomizer());
        }
    }

    public void TutorialMinigameScene()
    {
        Tutorial = true;

        scene1 = 2; //Introduces tapping
        scene2 = 10;//Introduces dragging
        scene3 = 6; //Introduces Tilting

        TutorialOST.Play();
        //DontDestroyOnLoad(TutorialMusic);
        FirstTimeTutorial(); //Stores in the save that tutorial has been started once.
        levelTransition.LoadFirstMiniGame();
    }

    private void FirstTimeTutorial()
    {
        if (FindObjectOfType<SaveSystem>())
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>();
            if (saving.data.hasPlayedTutorial == false)
            {
                saving.data.hasPlayedTutorial = true;
            }
        }
    }

    private void CheckIfTutorialHasRun()
    {
        if (FindObjectOfType<SaveSystem>() && playButton)
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>();
            if (saving.data.hasPlayedTutorial == false)
            {
                playButton.SetActive(false);
                tutorialButton.SetActive(false);
                tutorialPlayButton.SetActive(true);
            }
            if (saving.data.hasPlayedTutorial == true)
            {
                playButton.SetActive(true);
                tutorialButton.SetActive(true);
                tutorialPlayButton.SetActive(false);
            }
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
                MinigameMusic.SetActive(true);
                //DontDestroyOnLoad(MinigameMusic);

                //TAKE THIS ONE!!!
                levelTransition.LoadFirstMiniGame();
                //SceneManager.LoadScene("Minigame#" + scene1);//it's important that all minigames are called the same
            }

            yield return new WaitForSeconds(0);
        }
    }

    public void DeckClear()
    {
        Tutorial = false;
        Quacken.s_quackenBeenReleased = false; //Resets the Quacken.
        DeckScript.Deck.Clear();
        NewCardHandScript.Stage = 1;
    }

    public void Silence()
    {
        MinigameMusic = GameObject.Find("MinigameMusic");
        TutorialMusic = GameObject.Find("TutorialMusic");

        if (MinigameMusic)
            Destroy(MinigameMusic);

        if (TutorialMusic)
            Destroy(TutorialMusic);
    }
}