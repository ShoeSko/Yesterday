using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCorporate : MonoBehaviour
{
    private List<string> Abilities = new List<string>();

    public List<GameObject> TowerSpots = new List<GameObject>();
    public List<GameObject> DeactivateTowerSpots = new List<GameObject>();

    private int AbilityNumber;
    private GameObject currentTowerSpot;

    //setup
    private Rigidbody2D rb;
    public bool IsActive;
    private Vector2 NewPos;
    private float speed;
    public int Health;//boss' health

    //this will keep track of each ability's cooldown
    private int CD1;
    private int CD2;
    private int CD3;

    private int RandomAbility;

    //Property_Business
    private int RandomSpot;
    private GameObject activeSpot;

    //Stock_Shortage
    private int manaSteal;

    //Greedy_Opportunity

    private float timer;
    private int loop;

    public void Activate()//Do this at the start
    {
        rb = GetComponent<Rigidbody2D>();
        NewPos = new Vector2(8.44f, 1.34f);
        speed = 0.8f;

        //timer = 10;
        CD3 = 1;
        CD2 = 1;

        Abilities.Add("Property_Business");
        Abilities.Add("Stock_Shortage");
        Abilities.Add("Greedy_Opportunity");

        AbilityNumber = Abilities.Count;

        for (int deactivated = 0; deactivated < DeactivateTowerSpots.Count; deactivated++)//make space for the boss by destroying the last row
        {
            currentTowerSpot = DeactivateTowerSpots[deactivated];
            Destroy(currentTowerSpot);
        }
        Health = 500;
    }

    void Update()
    {
        if (IsActive)//when the boss is meant to be active
        {
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, NewPos, speed * Time.deltaTime);//move dramatically to position

            timer += Time.deltaTime;

            if(timer >= 20)
            {
                for (loop = 0; loop < 1; loop++)
                {
                    RandomAbility = Random.Range(0, AbilityNumber+1);//choose one random ability
                    if (RandomAbility == 1)
                        Property_Business();
                    else if (RandomAbility == 2)
                        Stock_Shortage();
                    else if (RandomAbility == 3)
                        Greedy_Opportunity();
                }
                CD1--;
                CD2--;
                CD3--;
                timer = 0;
            }
        }
    }


    void Property_Business()//Ability 1
    /* The Corporate controls the property market. He will 'buy out' your land right under your nose. 
     * The Corporate will mark an empty unit slot on the map and render it permanently unusuable. 
     * The player cannot place units in the slot with the Corporate mark. */
    {
        if (CD1 <= 0)//Do this ability
        {
            RandomSpot = Random.Range(0, TowerSpots.Count+1);
            activeSpot = TowerSpots[RandomSpot];
            //instantiate a sign on the active spot (when we get art of the sign)
            Destroy(activeSpot);//This permanently removes the slot from the round.

            //  CD1 = 2;//Set cooldown
        }
        else
            loop--;
    }

    void Stock_Shortage()//Ability 2
    /*The Corporate has full control over the stock market, therefore he controls the prices. When The Corporate uses 'Stock Shortage', 
    your mana is decreased by a random number between 1-3, making risky investments all the more riskier.*/
    {
        if (CD2 <= 0)//Do this ability
        {
            manaSteal = Random.Range(1, 4);
            ManaSystem.CurrentMana -= manaSteal;

            CD2 = 3;//set cooldown
        }
        else
            loop--;
    }

    void Greedy_Opportunity()//Ability 3
    /* The Corporate's greed is immeasurable as he will take any opportunity he sees.The Corporate will sometimes try to take one of your units to sell it. 
     * He will reach out his hand on one of the lanes to try to take a unit from that lane. While he reaches out, his hand will be vulrnurable. 
     * If the hand takes a total of 150 damage it will go back. All damage done to the hand is also dealt to The Corporate. 
     * If the hand reaches a unit, it will start taking it. After 5 seconds if the hand still persists, the hand will snatch that unit, destroying it. 
     * It will then become invincible and go back into hiding. */
    {
        if (CD3 <= 0)//Do this ability
        {


            CD3 = 2;//set cooldown
        }
        else
            loop--;
    }
}
