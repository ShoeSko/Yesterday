using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : MonoBehaviour
{
    private GameObject grave;
    private AudioSource LeafSFX;

    private void Start()
    {
        grave = GameObject.Find("Gravestone");
        LeafSFX = GameObject.Find("LeafAudio").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        grave.GetComponent<GraveCleaning>().score++;
        LeafSFX.Play();
        Destroy(gameObject);
    }
}
