using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporateAbilities : TheBossClass_Parent
{
    public override void Ability1()
    {
        Debug.Log("I used Corporate ability 1");
        //WORK IN PROGRRES
        /*
        GameObject activeSpot;

        bool isNotOccupied = false;
        for (int i = 0; i < TowerSpots.Count; i++)
        {
            if (!TowerSpots[i].GetComponentInParent<UnitPrototypeScript>())
            {
                isNotOccupied = true; //Checks the list for slots that can not be used.
            }
        }
        if (isNotOccupied) //If the For Loop discovered a node that did not have the UnitPrototypeScript, then it will allow this to run. If not, skip it.
        {
            int RandomSpot = Random.Range(0, TowerSpots.Count);
            while (TowerSpots[RandomSpot].GetComponentInParent<UnitPrototypeScript>())
            {
                RandomSpot = Random.Range(0, TowerSpots.Count);
            }
            activeSpot = TowerSpots[RandomSpot];
        }
        isNotOccupied = false; //Return the bool to false.

        if (activeSpot != null)
        {
            GameObject sign = Instantiate(CorporateSign);
            sign.transform.position = activeSpot.transform.position;
            sign.transform.position = new Vector3(activeSpot.transform.position.x, activeSpot.transform.position.y, 0); //An attempt to fix Sign placement.
            sign.transform.SetParent(transform); //Places all sign as children for the Boss.
            placedCorporateSigns.Add(sign);
            Destroy(activeSpot);//This permanently removes the slot from the round.
            TowerSpots.RemoveAt(RandomSpot);//Removes the Towerspot from the list to prevent reruns.

            CD1 = 3;//Set cooldown
        }
        */
    }

    public override void Ability2()
    {
        Debug.Log("I used Corporate ability 2");
    }

    public override void Ability3()
    {
        Debug.Log("I used Corporate ability 3");
    }
}
