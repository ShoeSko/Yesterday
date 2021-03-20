using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GroundScript.win = true;
    }
}