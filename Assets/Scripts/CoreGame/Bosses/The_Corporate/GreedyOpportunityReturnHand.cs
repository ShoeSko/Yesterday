using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyOpportunityReturnHand : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        print("I am hidden, like a ninja");
        if (transform.parent.gameObject.GetComponent<GreedyOpportunity>())
        {
            transform.parent.gameObject.GetComponent<GreedyOpportunity>().HandHasReturned();
        }
    }
}
