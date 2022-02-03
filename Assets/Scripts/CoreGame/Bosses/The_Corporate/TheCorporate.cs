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
    [SerializeField] private Animator hamsterAnimator;
    private bool hasBeenDefeated;

    //Animations & poses
    public Sprite Default;
    public Sprite Damaged1;
    public Sprite Damaged2;
    public Sprite pepsi;

    public GameObject Snap1;
    public GameObject Snap2;

    public GameObject Laugh;

    public GameObject CorpDialogue;

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
    private List<GameObject> placedCorporateSigns = new List<GameObject>();

    //Stock_Shortage
    private int manaSteal;

    //Greedy_Opportunity
    private int RandomHand;
    private GameObject CurrentHand;

    private float timer;
    private int loop;

    //Death confirmation
    private bool isDead;

    public List<AudioSource> VoiceStockShortage = new List<AudioSource>();
    public List<AudioSource> VoiceGreedyOpportunity = new List<AudioSource>();
    public List<AudioSource> VoicePropertyBusiness = new List<AudioSource>();
    public List<AudioSource> VoiceTakeDamage = new List<AudioSource>();

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


        //Ths cheats above have been upgraded to be in cheats. Right click the component to get the contextMenu options.

        for (int deactivated = 0; deactivated < DeactivateTowerSpots.Count; deactivated++)//make space for the boss by destroying the last row
        {
            currentTowerSpot = DeactivateTowerSpots[deactivated];
            Destroy(currentTowerSpot);
        }
    }

    #region Cheats
    [ContextMenu("Run Property Business")]
    private void PropertCheat()
    {
        Property_Business();
    }
    [ContextMenu("Run Stock Shortage")]
    private void StockCheat()
    {
        Stock_Shortage();
    }
    [ContextMenu("Run Greedy Opportunity")]
    private void GreedyCheat()
    {
        Greedy_Opportunity();
    }
    #endregion

    void Update()
    {
        if (IsActive)//when the boss is meant to be active
        {
            BossHealth.value = Health;
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, NewPos, speed * Time.deltaTime);//move dramatically to position

            timer += Time.deltaTime;

            if(timer >= 12)//The time before boss uses an ability
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
            for (int CurretnSign = placedCorporateSigns.Count - 1; CurretnSign >= 0; CurretnSign--)
            {
                Destroy(placedCorporateSigns[CurretnSign]); //Destroys all placed signs.
            }
            this.gameObject.GetComponent<SpriteRenderer>().sprite = pepsi;
            GetComponent<Animator>().enabled = true;
            if (!hasBeenDefeated)
            {
                WinCondition.GetComponent<Victory>().Win();
                AddBossDefeatToBestiary(); //With the defeat, the boss is unlocked in the bestiary.
                hasBeenDefeated = true;
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
                int RandomVoice = Random.Range(0, VoicePropertyBusiness.Count);
                VoicePropertyBusiness[RandomVoice].Play();

                GameObject sign = Instantiate(CorporateSign);
                sign.transform.position = activeSpot.transform.position;
                sign.transform.position = new Vector3(activeSpot.transform.position.x, activeSpot.transform.position.y, 0); //An attempt to fix Sign placement.
                sign.transform.SetParent(transform); //Places all sign as children for the Boss.
                placedCorporateSigns.Add(sign);
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
            int RandomVoice = Random.Range(0, VoiceStockShortage.Count);
            VoiceStockShortage[RandomVoice].Play();

            StartCoroutine(IndicateStockShortage()); //Runs a coroutine to give time for the indicator.

            CD2 = 2;//set cooldown
        }
        else
            loop--;
    }
    IEnumerator IndicateStockShortage()
    {
        manaBarStockIndicator.GetComponent<Animator>().SetTrigger("Stock"); //Starts warning animation
        hamsterAnimator.SetTrigger("Panic");
        Snap1.SetActive(true);
        yield return new WaitForSeconds(stockShortageWarningTime);
        Snap1.SetActive(false);
        Snap2.SetActive(true);
        manaSteal = Random.Range(1, 4);
        ManaSystem.CurrentMana -= manaSteal;
        manaBarStockIndicator.GetComponent<Animator>().SetTrigger("End");
        hamsterAnimator.SetTrigger("Charge");
        yield return new WaitForSeconds(0.5f);
        Snap2.SetActive(false);
    }
    void Greedy_Opportunity()//Ability 3
    /* The Corporate's greed is immeasurable as he will take any opportunity he sees.The Corporate will sometimes try to take one of your units to sell it. 
     * He will reach out his hand on one of the lanes to try to take a unit from that lane. While he reaches out, his hand will be vulrnurable. 
     * The hand has 150HP and when it dies, The Corporate loses 1HP.
     * If the hand reaches a unit, it will start taking it. After 4 seconds if the hand still persists, the hand will snatch that unit, destroying it. 
     * If the hand reaches your farm, it will take a card from your deck, then proceed to back away with it, invincible.
     * It will then become invincible and go back into hiding. */
    {
        if (CD3 <= 0)//Do this ability
        {
            for(int loop = 0; loop < 1; loop++)
            {
                RandomHand = Random.Range(0, 4);
                if (CorporateHands[RandomHand])
                {
                CurrentHand = CorporateHands[RandomHand];

                }

                if (CurrentHand != null && CurrentHand.GetComponent<GreedyOpportunity>().obstacleInTheWay != false)
                {
                    int RandomVoice = Random.Range(0, VoiceGreedyOpportunity.Count);
                    VoiceGreedyOpportunity[RandomVoice].Play();

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

        CorpDialogue.GetComponent<DialogueCode>().PreperationMode();
    }

    IEnumerator Laughter()
    {
        Laugh.SetActive(true);
        yield return new WaitForSeconds(2);
        Laugh.SetActive(false);
    }

    public void RecieveDamage()
    {
        int RandomVoice = Random.Range(0, VoiceTakeDamage.Count);
        VoiceTakeDamage[RandomVoice].Play();

        Health--;
        if(Health != 0)
            StartCoroutine(tookDamage());
    }

    #region Bestiary
    private void AddBossDefeatToBestiary()
    {
        if (FindObjectOfType<SaveSystem>())
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>();

            saving.data.bossList[0] = true; //Boss index 0 has been defeated Corporate.
        }
    }
    #endregion
}