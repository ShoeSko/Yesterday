using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEggScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        fallingEggs.collectedEggs++;
        Destroy(gameObject);
    }
}
