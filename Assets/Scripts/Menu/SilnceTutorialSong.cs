using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilnceTutorialSong : MonoBehaviour
{
    private void Start()
    {
        GameObject tutorialSong = GameObject.Find("TutorialMusic");
        tutorialSong.GetComponent<AudioSource>().Stop();
    }
}
