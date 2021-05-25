using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEggScript : MonoBehaviour
{
    fallingEggs motherHenScript;
    private bool isCollected;
    private void Awake()
    {
        motherHenScript = FindObjectOfType<fallingEggs>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        fallingEggs.collectedEggs++;//count towards the goal of the minigame
        isCollected = true;
        motherHenScript.AnEggWasCollected();
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        if (!isCollected)
        {
            motherHenScript.AnEggWasLost();
            Destroy(gameObject);
        }
    }
}
