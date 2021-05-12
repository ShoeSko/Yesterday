using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressZoneCollider : MonoBehaviour
{
    public GameObject Emote;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            Emote.GetComponent<Emotes>().EnemiesInStressZone++;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Emote.GetComponent<Emotes>().EnemiesInStressZone--;
        }
    }
}
