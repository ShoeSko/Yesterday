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

    public void LoadNextLevelFromCoreGame()
    {
        string nextLevelName = ""; //Remembers the name



        RemoveButton();
        StartCoroutine(SceneFadeMechanic(nextLevelName));
    }

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


    IEnumerator SceneFadeMechanic(string levelName)
    {
        transitionController.SetTrigger("Start"); //Gives a trigger to the controller to start the leave animation.

        yield return new WaitForSeconds(waitTime); //Waits until animation is done + extra if wanted

        SceneManager.LoadScene(levelName); //Loads planned scene.
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