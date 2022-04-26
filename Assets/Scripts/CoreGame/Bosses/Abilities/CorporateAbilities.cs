using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorporateAbilities : TheBossClass_Parent
{
    public override void Ability1()
    {
        Debug.Log("I used Corporate ability 1");

        CoreBossManager CoreScript = FindObjectOfType<CoreBossManager>();
        GameObject activeSpot = null;
        int RandomSpot = 0;
        List<GameObject> TowerSpots = CoreScript.AvailableSlots;


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
            RandomSpot = Random.Range(0, TowerSpots.Count);
            while (TowerSpots[RandomSpot].GetComponentInParent<UnitPrototypeScript>())
            {
                RandomSpot = Random.Range(0, TowerSpots.Count);
            }
            activeSpot = TowerSpots[RandomSpot];
        }
        isNotOccupied = false; //Return the bool to false.

        
        if (activeSpot != null)
        {
            GameObject sign = Instantiate(CoreScript.Sign);
            sign.transform.position = activeSpot.transform.position;
            sign.transform.position = new Vector3(activeSpot.transform.position.x, activeSpot.transform.position.y, 0); //An attempt to fix Sign placement.
            sign.transform.SetParent(transform); //Places all sign as children for the Boss.
            Destroy(activeSpot);//This permanently removes the slot from the round.
            TowerSpots.RemoveAt(RandomSpot);//Removes the Towerspot from the list to prevent reruns.
        }
        
    }

    public override void Ability2()
    {
        Debug.Log("I used Corporate ability 2");

        GameObject Snap1 = GameObject.Find("Boss_Corp_Hand_Snap_1");
        GameObject Snap2 = GameObject.Find("Boss_Corp_Hand_Snap_2");

        StartCoroutine(IndicateStockShortage());

        IEnumerator IndicateStockShortage()
        {
            GameObject.Find("ManaCircle Charge").GetComponent<Animator>().SetTrigger("Stock"); //Starts warning animation
            GameObject.Find("Hamster").GetComponent<Animator>().SetTrigger("Panic");
            Snap1.transform.position += new Vector3(0, 0, 30);

            yield return new WaitForSeconds(1.5f);

            Snap1.transform.position += new Vector3(0, 0, -30);
            Snap2.transform.position += new Vector3(0, 0, 30);
            int manaSteal = Random.Range(1, 4);
            ManaSystem.CurrentMana -= manaSteal;
            GameObject.Find("ManaCircle Charge").GetComponent<Animator>().SetTrigger("End");
            GameObject.Find("Hamster").GetComponent<Animator>().SetTrigger("Charge");

            yield return new WaitForSeconds(0.5f);

            Snap2.transform.position += new Vector3(0, 0, -30);
        }
    }

    public override void Ability3()
    {
        Debug.Log("I used Corporate ability 3");

        GameObject CurrentHand = null;
        int failsafe = 0;
        List<GameObject> CorporateHands = FindObjectOfType<CoreBossManager>().GreedyHands;

        for (int loop = 0; loop < 1; loop++)
        {
            failsafe++;//Make sure the loop doesn't get stuck
            int RandomHand = Random.Range(0, 4);

            if (CorporateHands[RandomHand])
            {
                CurrentHand = CorporateHands[RandomHand];
            }

            if (CurrentHand != null && CurrentHand.GetComponent<GreedyOpportunity>().obstacleInTheWay != false)
            {
                CurrentHand.GetComponent<GreedyOpportunity>().obstacleInTheWay = false;
                StartCoroutine(Laughter());
            }
            else if(failsafe < 30)
                loop--;
        }

        IEnumerator Laughter()
        {
            GameObject.Find("Corporate_Laugh").transform.position += new Vector3(0, 0, 320);
            yield return new WaitForSeconds(2);
            GameObject.Find("Corporate_Laugh").transform.position += new Vector3(0, 0, -320);
        }
    }
}
