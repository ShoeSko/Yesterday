using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    public GameObject minigameOST;
    public GameObject tutorialOST;

    void Start()
    {
        minigameOST.GetComponent<AudioSource>().Stop();
        tutorialOST.GetComponent<AudioSource>().Stop();

        DontDestroyOnLoad(minigameOST);
        DontDestroyOnLoad(tutorialOST);
    }

}