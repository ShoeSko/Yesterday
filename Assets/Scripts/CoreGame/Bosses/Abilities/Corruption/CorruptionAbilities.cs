using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionAbilities : TheBossClass_Parent
{
    [HideInInspector] public int MinimumSpawnDelay = 8;
    [HideInInspector] public int MaximumSpawnDelay = 12;
    private int SuperSecretBalanceMechanic = 3;

    public override void Ability1()
    {
        Debug.Log("I used Corruption ability 1");

        int RandomLane = UnityEngine.Random.Range(0, 4);
        GameObject Lane = GameObject.Find("The Boss").GetComponent<CoreBossManager>().Lanes[RandomLane];
        GameObject CorLane = null;

        switch (RandomLane)//Add more scripts when implementing more bosses
        {
            case 0:
                Lane.SetActive(false);
                CorLane = GameObject.Find("CorruptionLine1");
                break;
            case 1:
                Lane.SetActive(false);
                CorLane = GameObject.Find("CorruptionLine2");
                break;
            case 2:
                Lane.SetActive(false);
                CorLane = GameObject.Find("CorruptionLine3");
                break;
            case 3:
                Lane.SetActive(false);
                CorLane = GameObject.Find("CorruptionLine4");
                break;
        }

        CorLane.GetComponent<CorruptedLane>().ActiveLane = Lane;
        CorLane.GetComponent<CorruptedLane>().Move();
    }

    public override void Ability2()
    {
        Debug.Log("I used Corruption ability 2");

        CardScript UnitToCorrupt = null;
        GameObject SpawnPlace = null;

        for (int loop = 0; loop < 1; loop++)
        {
            UnitToCorrupt = DeckScript.Deck[UnityEngine.Random.Range(0, DeckScript.Deck.Count)];//Random unit from your deck

            if (UnitToCorrupt.manaCost > SuperSecretBalanceMechanic)
                loop--;
            else
                SuperSecretBalanceMechanic += 3;
        }

        //Define what to spawn and where
        int SpawnLocation = UnityEngine.Random.Range(0, 4);

        Sprite UnitToCorruptSprite = UnitToCorrupt.image;

        GameObject.Find("The Boss").GetComponent<CoreBossManager>().CorruptedUnit.GetComponent<SpriteRenderer>().sprite = UnitToCorruptSprite;
        GameObject.Find("Bossfight Dialogues").GetComponent<DialogueCode>().CorruptionSprites.Add(UnitToCorruptSprite);

        switch (SpawnLocation)
        {
            case 0:
                SpawnPlace = GameObject.Find("#1");
                break;
            case 1:
                SpawnPlace = GameObject.Find("#2");
                break;
            case 2:
                SpawnPlace = GameObject.Find("#3");
                break;
            case 3:
                SpawnPlace = GameObject.Find("#4");
                break;
        }

        GameObject Enemy = Instantiate(GameObject.Find("The Boss").GetComponent<CoreBossManager>().CorruptedUnit, SpawnPlace.transform.position, transform.rotation);

        //Enemy.GetComponent<BasicEnemyMovement>().enemy.enemyHealth = 30 + (20 * UnitToCorrupt.manaCost);//Alternative way of balancing this ability
        //Enemy.GetComponent<BasicEnemyMovement>().enemy.attackDamage = 6 + (4 * UnitToCorrupt.manaCost);//Alternative way of balancing this ability
        Enemy.GetComponent<BasicEnemyMovement>().enemy.attackDamage = UnitToCorrupt.punchDamage;
        Enemy.GetComponent<BasicEnemyMovement>().enemy.attackSpeed = UnitToCorrupt.punchRechargeTime;
        int CardHealth = (int)UnitToCorrupt.health;
        Enemy.GetComponent<BasicEnemyMovement>().enemy.enemyHealth = CardHealth;
    }

    public override void Ability3()
    {
        Debug.Log("I used Corruption ability 3");

        CoreBossManager boss = GameObject.Find("The Boss").GetComponent<CoreBossManager>();
        List<GameObject> TowerSpots = boss.AvailableSlots;
        bool isNotOccupied = false;
        GameObject activeSpot = null;

        for (int i = 0; i < TowerSpots.Count; i++)
        {
            if (!TowerSpots[i].GetComponentInParent<UnitPrototypeScript>())
            {
                isNotOccupied = true; //Checks the list for slots that can not be used.
            }
        }
        if (isNotOccupied) //If the For Loop discovered a node that did not have the UnitPrototypeScript, then it will allow this to run. If not, skip it.
        {
            int RandomSpot = UnityEngine.Random.Range(0, TowerSpots.Count);
            while (TowerSpots[RandomSpot].GetComponentInParent<UnitPrototypeScript>())
            {
                RandomSpot = UnityEngine.Random.Range(0, TowerSpots.Count);
            }
            activeSpot = TowerSpots[RandomSpot];
        }

        if (activeSpot != null)
        {
            //Visual hints for the player
            activeSpot.GetComponent<Image>().color = Color.yellow;

            GameObject sanctuary = Instantiate(boss.Sanctuary);
            sanctuary.transform.position = activeSpot.transform.position;
            sanctuary.transform.position = new Vector3(activeSpot.transform.position.x, activeSpot.transform.position.y, 0); //An attempt to fix Sign placement.
            sanctuary.GetComponent<SanctuaryCode>().activeSpot = activeSpot;
            activeSpot.GetComponent<TowerSpotsScript>().IsSanctuary = true;
        }
    }

    public void Desperate_Rage()
    {
        NewCardHandScript HandCode = GameObject.Find("HANDscript").GetComponent<NewCardHandScript>();

        Color CorruptedColor = new Color(0.6315554f, 0, 0.8396226f, 1);
        int randomCard = UnityEngine.Random.Range(0, 5);
        List<GameObject> CardBG = GameObject.Find("The Boss").GetComponent<CoreBossManager>().Cards;

        CardBG[randomCard].GetComponent<Image>().color = CorruptedColor;
        CardBG[randomCard + 5].GetComponent<Image>().color = CorruptedColor;

        switch (randomCard)
        {
            case 0:
                if (HandCode.Card1Corrupted == false)
                {
                    HandCode.card1.GetComponent<CardDisplayer>().artworkImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard1.GetComponent<CardDisplayer>().artworkImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.card1.GetComponent<CardDisplayer>().backgroundImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard1.GetComponent<CardDisplayer>().backgroundImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.card1.GetComponent<CardDisplayer>().iconImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard1.GetComponent<CardDisplayer>().iconImage.GetComponent<Image>().color = CorruptedColor;

                    HandCode.Card1Corrupted = true;
                }
                else Desperate_Rage();
                break;
            case 1:
                if (HandCode.Card2Corrupted == false)
                {
                    HandCode.card2.GetComponent<CardDisplayer>().artworkImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard2.GetComponent<CardDisplayer>().artworkImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.card2.GetComponent<CardDisplayer>().backgroundImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard2.GetComponent<CardDisplayer>().backgroundImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.card2.GetComponent<CardDisplayer>().iconImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard2.GetComponent<CardDisplayer>().iconImage.GetComponent<Image>().color = CorruptedColor;

                    HandCode.Card2Corrupted = true;
                }
                else Desperate_Rage();
                break;
            case 2:
                if (HandCode.Card3Corrupted == false)
                {
                    HandCode.card3.GetComponent<CardDisplayer>().artworkImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard3.GetComponent<CardDisplayer>().artworkImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.card3.GetComponent<CardDisplayer>().backgroundImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard3.GetComponent<CardDisplayer>().backgroundImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.card3.GetComponent<CardDisplayer>().iconImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard3.GetComponent<CardDisplayer>().iconImage.GetComponent<Image>().color = CorruptedColor;

                    HandCode.Card3Corrupted = true;
                }
                else Desperate_Rage();
                break;
            case 3:
                if (HandCode.Card4Corrupted == false)
                {
                    HandCode.card4.GetComponent<CardDisplayer>().artworkImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard4.GetComponent<CardDisplayer>().artworkImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.card4.GetComponent<CardDisplayer>().backgroundImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard4.GetComponent<CardDisplayer>().backgroundImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.card4.GetComponent<CardDisplayer>().iconImage.GetComponent<Image>().color = CorruptedColor;
                    HandCode.Lcard4.GetComponent<CardDisplayer>().iconImage.GetComponent<Image>().color = CorruptedColor;

                    HandCode.Card4Corrupted = true;
                }
                else Desperate_Rage();
                break;
            case 4:
                HandCode.card5.GetComponent<CardDisplayer>().artworkImage.GetComponent<Image>().color = CorruptedColor;
                HandCode.Lcard5.GetComponent<CardDisplayer>().artworkImage.GetComponent<Image>().color = CorruptedColor;
                HandCode.card5.GetComponent<CardDisplayer>().backgroundImage.GetComponent<Image>().color = CorruptedColor;
                HandCode.Lcard5.GetComponent<CardDisplayer>().backgroundImage.GetComponent<Image>().color = CorruptedColor;
                HandCode.card5.GetComponent<CardDisplayer>().iconImage.GetComponent<Image>().color = CorruptedColor;
                HandCode.Lcard5.GetComponent<CardDisplayer>().iconImage.GetComponent<Image>().color = CorruptedColor;

                HandCode.Card5Corrupted = true;
                break;
        }
    }
}