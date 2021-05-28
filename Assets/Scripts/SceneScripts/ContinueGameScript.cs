using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContinueGameScript : MonoBehaviour
{
    private SaveSystem saving;

    private void Awake()
    {
        if (FindObjectOfType<SaveSystem>())
        {
            saving = FindObjectOfType<SaveSystem>();
            //print(saving.data.lastScene);
        }
    }

    private void Update()
    {
        if (FindObjectOfType<SaveSystem>())
        {
            ContinueButtonController();
        }
    }


    private void ContinueButtonController()
    {
        if(saving.data.lastScene == null) //If there is no last scene
        {
            GetComponent<Button>().interactable = false;
            //print("The is no save game to continue");
        }

        if(saving.data.lastScene != null) //If there is a last scene
        {
            GetComponent<Button>().interactable = true;
            //print("The was a save game to continue");
        }
    }

    public void ReloadLastScene()
    {
        print("The saved scene name was " + saving.data.lastScene);
        SceneManager.LoadScene(saving.data.lastScene);
    }
}
