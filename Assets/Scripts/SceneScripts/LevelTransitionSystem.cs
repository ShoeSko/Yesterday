using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransitionSystem : MonoBehaviour
{
    [SerializeField] private int waitTime;
    [SerializeField] private Animator transitionController;
    [SerializeField] private GameObject nextLevelButton;

    [HideInInspector] public int currentMinigameScore;
    [SerializeField] private Color hideButtonColour;

    #region Core Game
    public void VictoryButtonPress()
    {
        string nextLevelName = ""; //Remembers the name

        Time.timeScale = 1;
        NewCardHandScript.Stage++;
        Debug.Log(NewCardHandScript.Stage);

        if (NewCardHandScript.Stage == 4)
            nextLevelName = "MainMenu";

        StartCoroutine(SceneFadeMechanic(nextLevelName));
    }
    public void GameOverButtonPress() // You lost. Straight to the main menu for a quick and easy reset.
    {
        string nextLevelName = "";

        Time.timeScale = 1;
        Quacken.s_quackenBeenReleased = false; //Resets the Quacken.
        nextLevelName = "MainMenu";

        StartCoroutine(SceneFadeMechanic(nextLevelName));
    }
    public void LoadFirstMiniGame() //Sends you to the loading scene
    {

        StartCoroutine(SceneFadeMechanic("LoadingScene"));
    }

    public void LoadNextLevelFromCoreGame() //Runs the first minigame from the Core game after loading scene
    {
        string nextLevelName = ""; //Remembers the name

        nextLevelName = "Minigame#" + MinigameSceneScript.scene1;

        StartCoroutine(SceneFadeMechanic(nextLevelName));
    }
    #endregion
    #region Reward Screen
    public void LoadLevelFromRewardScreen()
    {
        string nextLevelName = ""; //Remembers the name

        if (MinigameSceneScript.activeMinigame == 1)
        {
            MinigameSceneScript.activeMinigame++;
            nextLevelName = "Minigame#" + MinigameSceneScript.scene2;
        }
        else if (MinigameSceneScript.activeMinigame == 2)
        {
            MinigameSceneScript.activeMinigame++;
            nextLevelName= "Minigame#" + MinigameSceneScript.scene3;
        }
        else if (MinigameSceneScript.activeMinigame == 3)
        {
            nextLevelName = "CoreGame";
        }
        RemoveButton();
        StartCoroutine(SceneFadeMechanic(nextLevelName));
    }
    #endregion
    #region Minigames
    public void LoadLevelFromMinigame()
    {
        string nextLevelName = ""; //Remembers the name

            if (currentMinigameScore == 0)//skip card reward
            {
                if (MinigameSceneScript.activeMinigame == 1)
                {
                    MinigameSceneScript.activeMinigame++;
                    nextLevelName = "Minigame#" + MinigameSceneScript.scene2;
                }
                else if (MinigameSceneScript.activeMinigame == 2)
                {
                    MinigameSceneScript.activeMinigame++;
                    nextLevelName= "Minigame#" + MinigameSceneScript.scene3;
                }
                else if (MinigameSceneScript.activeMinigame == 3)
                {
                    nextLevelName = "CoreGame";
                }
            }
            else//Go to card reward screen
                nextLevelName = "CardReward";

        RemoveButton();
        StartCoroutine(SceneFadeMechanic(nextLevelName)); //Runs Coroutine
    }
    #endregion
    #region Save on Load
    private void SaveGameOnLoad()
    {
        if (FindObjectOfType<SaveSystem>())
        {
            FindObjectOfType<SaveSystem>().SaveTheData(); //Saves the data.
        }
    }
    #endregion


    IEnumerator SceneFadeMechanic(string levelName)
    {
        SaveGameOnLoad(); //Saves game when loading a new scene.

        transitionController.SetTrigger("Start"); //Gives a trigger to the controller to start the leave animation.

        yield return new WaitForSeconds(waitTime); //Waits until animation is done + extra if wanted

        print("Scene name to be loaded this time is " + levelName);

        if(levelName != "")
        {
        SceneManager.LoadScene(levelName); //Loads planned scene.
        }
        else
        {
            print("Failed to load scene. Not sure why. SEND HELP!");
        }
    }

    private void RemoveButton()
    {
        nextLevelButton.GetComponent<Image>().color = hideButtonColour;
    }
}


/*      [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
        [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  

        levelTransitioner.currentMinigameScore = score;
*/