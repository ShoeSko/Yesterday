using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceScore : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        GroundScript.score++;
        print("Triggered");
        Destroy(this);
    }
}
