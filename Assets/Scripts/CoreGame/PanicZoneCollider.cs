using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicZoneCollider : MonoBehaviour
{
    public GameObject Emote;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Emote.GetComponent<Emotes>().EnemiesInPanicZone++;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Emote.GetComponent<Emotes>().EnemiesInPanicZone--;
        }
    }
}
