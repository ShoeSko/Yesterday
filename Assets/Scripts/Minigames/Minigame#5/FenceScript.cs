using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceScript : MonoBehaviour
{
    public AudioSource VictorySFX;
    public AudioSource DeathSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GroundScript.win = true;

        if (GroundScript.score < 3)
            DeathSFX.Play();
        else
            VictorySFX.Play();
    }
}