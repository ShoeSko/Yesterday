using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : MonoBehaviour
{
    private GameObject grave;

    private void Start()
    {
        grave = GameObject.Find("Grave");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        grave.GetComponent<GraveCleaning>().score++;
        Destroy(gameObject);
    }
}
