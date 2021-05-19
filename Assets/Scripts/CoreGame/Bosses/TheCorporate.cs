using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheCorporate : MonoBehaviour
{
    private List<string> Abilities = new List<string>();
    public List<GameObject> CorporateHands = new List<GameObject>();

    public List<GameObject> TowerSpots = new List<GameObject>();
    public List<GameObject> DeactivateTowerSpots = new List<GameObject>();

    private GameObject currentTowerSpot;
    public GameObject WinCondition;
    public GameObject BossHealthbar;
    public Slider BossHealth;
    public GameObject manaBarStockIndicator;

    //Animations & poses
    public Sprite Default;
    public Sprite Damaged1;
    public Sprite Damaged2;
    public Sprite pepsi;

    public GameObject Snap1;
    public GameObject Snap2;

    public GameObject Laugh;

    //setup
    private Rigidbody2D rb;
    public bool IsActive;
    private Vector2 NewPos;
    private float speed;
    public int Health = 4;//boss' health
    public float stockShortageWarningTime;

    //this will keep track of each ability's cooldown
    private int CD1;
    private int CD2;
    private int CD3;

    private int RandomAbility;

    //Property_Business
    private int RandomSpot;
    private GameObject activeSpot;
    public GameObject CorporateSign;

    //Stock_Shortage
    private int manaSteal;

    //Greedy_Opportunity
    private int RandomHand;
    private GameObject CurrentHand;

    private float timer;
    private int loop;

    //Death confirmation
    private bool isDead;

    private void Start()
    {
        Health = 4;
        BossHealthbar.SetActive(false);//Sets his healthbar to inactive

        CorporateHands[0].GetComponent<GreedyOpportunity>().obstacleInTheWay = true;
        CorporateHands[1].GetComponent<GreedyOpportunity>().obstacleInTheWay = true;
        CorporateHands[2].GetComponent<GreedyOpportunity>().obstacleInTheWay = true;
        CorporateHands[3].GetComponent<GreedyOpportunity>().obstacleInTheWay = true;

        Snap1.SetActive(false);
        Snap2.SetActive(false);
        Laugh.SetActive(false);
    }

    public void Activate()//Do this at the start
    {
        BossHealthbar.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        NewPos = new Vector2(6f, 1.34f);
        speed = 0.9f;

        CD3 = 1;
        CD2 = 1;

        Abilities.Add("Property_Business");
        Abilities.Add("Stock_Shortage");
        Abilities.Add("Greedy_Opportunity");

        //Property_Business();  //(REMOVE // TO TEST THIS ABILITY)
        //Stock_Shortage();     //(REMOVE // TO TEST THIS ABILITY)
        //Greedy_Opportunity(); //(REMOVE // TO TEST THIS ABILITY)

        for (int deactivated = 0; deactivated < DeactivateTowerSpots.Count; deactivated++)//make space for the boss by destroying the last row
        {
            currentTowerSpot = DeactivateTowerSpots[deactivated];
            Destroy(currentTowerSpot);
        }
    }

    void Update()
    {
        if (IsActive)//when the boss is meant to be active
        {
            BossHealth.value = Health;
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, NewPos, speed * Time.deltaTime);//move dramatically to position

            timer += Time.deltaTime;

            if(timer >= 15)//The time before boss uses an ability
            {
                for (loop = 0; loop < 1; loop++)
                {
                    RandomAbility = Random.Range(0, 3);//choose one random ability
                    if (RandomAbility == 0)
                        Property_Business();
                    else if (RandomAbility == 1)
                        Stock_Shortage();
                    else if (RandomAbility == 2)
                        Greedy_Opportunity();
                }
                Debug.Log("I used" + Abilities[RandomAbility]);
                CD1--;
                CD2--;
                CD3--;
                timer = 0;
            }
        }

         if (Health == 0)
        {
            isDead = true;
        }

        if (isDead) //A condition to prevent issues when simply unloading the scene, as it would then cause victory condition.
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pepsi;
            WinCondition.GetComponent<Victory>().Win();
        }
    }

    void Property_Business()//Ability 1
    /* The Corporate controls the property market. He will 'buy out' your land right under your nose. 
     * The Corporate will mark an empty unit slot on the map and render it permanently unusuable. 
     * The player cannot place units in the slot with the Corporate mark. */
    {
        if (CD1 <= 0)//Do this ability
        {
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
                GameObject sign = Instantiate(CorporateSign);
                sign.transform.position = activeSpot.transform.position;
                sign.transform.position = new Vector3(activeSpot.transform.position.x, activeSpot.transform.position.y, 0); //An attempt to fix Sign placement.
                sign.transform.SetParent(transform); //Places all sign as children for the Boss.
                Destroy(activeSpot);//This permanently removes the slot from the round.
                TowerSpots.RemoveAt(RandomSpot);//Removes the Towerspot from the list to prevent reruns.

                CD1 = 3;//Set cooldown
            }
            else { CD1 = 1; } //Cooldown before retrying to purchase

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
            StartCoroutine(IndicateStockShortage()); //Runs a coroutine to give time for the indicator.

            CD2 = 2;//set cooldown
        }
        else
            loop--;
    }
    IEnumerator IndicateStockShortage()
    {
        manaBarStockIndicator.SetActive(true);
        Snap1.SetActive(true);
        yield return new WaitForSeconds(stockShortageWarningTime);
        Snap1.SetActive(false);
        Snap2.SetActive(true);
        manaSteal = Random.Range(1, 4);
        ManaSystem.CurrentMana -= manaSteal;
        manaBarStockIndicator.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Snap2.SetActive(false);
    }
    void Greedy_Opportunity()//Ability 3
    /* The Corporate's greed is immeasurable as he will take any opportunity he sees.The Corporate will sometimes try to take one of your units to sell it. 
     * He will reach out his hand on one of the lanes to try to take a unit from that lane. While he reaches out, his hand will be vulrnurable. 
     * If the hand takes a total of 150 damage it will go back. All damage done to the hand is also dealt to The Corporate. 
     * If the hand reaches a unit, it will start taking it. After 4 seconds if the hand still persists, the hand will snatch that unit, destroying it. 
     * It will then become invincible and go back into hiding. */
    {
        if (CD3 <= 0)//Do this ability
        {
            for(int loop = 0; loop < 1; loop++)
            {
                RandomHand = Random.Range(0, 4);
                CurrentHand = CorporateHands[RandomHand];

                if (CurrentHand != null && CurrentHand.GetComponent<GreedyOpportunity>().obstacleInTheWay != false)
                {
                    CurrentHand.GetComponent<GreedyOpportunity>().obstacleInTheWay = false;
                    StartCoroutine(Laughter());
                }
                else
                    loop--;
            }
            CD3 = 2;//set cooldown
        }
        else
            loop--;
    }

    IEnumerator tookDamage()
    {
        if (Health == 3 || Health == 2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Damaged1;
            //Do damage animation 1
        }
        else if (Health == 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Damaged2;
            //Do damage animation 2
        }
        yield return new WaitForSeconds(2);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = Default;
    }

    IEnumerator Laughter()
    {
        Laugh.SetActive(true);
        yield return new WaitForSeconds(2);
        Laugh.SetActive(false);
    }

    public void RecieveDamage()
    {
        Health--;
        if(Health != 0)
            StartCoroutine(tookDamage());
    }
}