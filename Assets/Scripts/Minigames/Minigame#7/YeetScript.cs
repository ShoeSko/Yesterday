using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeetScript : MonoBehaviour
{
    private Collider2D Croot1;
    private Collider2D Croot2;
    private Collider2D Croot3;

    public GameObject root1;
    public GameObject root2;
    public GameObject root3;

    private bool triggered1;
    private bool triggered2;
    private bool triggered3;

    public AudioSource CarrotPOP;

    private void Start()
    {
        Croot1 = root1.GetComponent<Collider2D>();
        Croot2 = root2.GetComponent<Collider2D>();
        Croot3 = root3.GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collider)//when the "the roots leave the ground", yeet.
    {
        if(collider == Croot1 && !triggered1)
        {
            CarrotPOP.Play();
            root1.GetComponent<WeedPulling>().PulledOut = true;
            triggered1 = true;
        }
        if (collider == Croot2 && !triggered2)
        {
            CarrotPOP.Play();
            root2.GetComponent<WeedPulling>().PulledOut = true;
            triggered2 = true;
        }
        if (collider == Croot3 && !triggered3)
        {
            CarrotPOP.Play();
            root3.GetComponent<WeedPulling>().PulledOut = true;
            triggered3 = true;
        }
    }
}