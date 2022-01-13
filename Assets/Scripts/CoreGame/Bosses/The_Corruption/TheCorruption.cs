using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheCorruption : MonoBehaviour
{
    private List<string> Abilities = new List<string>();

    public List<GameObject> DeactivateTowerSpots = new List<GameObject>();
    private GameObject CurrentTowerSpot;

    public GameObject BossHealthbar;
    public Slider BossHealth;
    public GameObject WinCondition;
    private bool hasBeenDefeated;

    //Setup
    private Rigidbody2D rb;
    public bool IsActive;
    private Vector2 NewPos;
    private float speed;
    public int Health = 4;//boss' health

    //This will keep track of boss' cooldowns
    private int CD1;
    private int CD2;
    private int CD3;

    private int LoopErrorFix;

    private int RandomAbility;

    //Ability_Desperate_Rage


    //Ability_Corrupted_Grounds
    private int RandomLane;
    private bool DeactivateDelay;
    private float AbilityDelay;
    private GameObject CurrentTemporarySpotDisable;
    public GameObject TemporarySpotDisableL1;
    public GameObject TemporarySpotDisableL2;
    public GameObject TemporarySpotDisableL3;
    public GameObject TemporarySpotDisableL4;
    private GameObject CurrentLaneChange;
    public GameObject LaneChangeL1;
    public GameObject LaneChangeL2;
    public GameObject LaneChangeL3;
    public GameObject LaneChangeL4;

    //Ability_Corrupting_Gaze
    public List<GameObject> SpawnPlace = new List<GameObject>();
    private int SpawnLocation;

    public GameObject CorruptedUnit;
    private CardScript UnitToCorrupt;
    private Sprite UnitToCorruptSprite;

    private int SuperSecretBalanceMechanic;//Makes it so a 10-cost unit can't spawn within the first 20 seconds of the round

    //Ability_Purification
    private bool isNotOccupied = false;
    public List<GameObject> TowerSpots = new List<GameObject>();
    private int RandomSpot;
    private GameObject activeSpot;
    public GameObject Sanctuary;
    private bool SanctuaryPlaced;
    private float SanctuaryTimer;
    private GameObject sanctuary;
    public bool SaintLives;
    public bool SaintDied;
    public GameObject Spawner;
    public List<GameObject> PlayedSaints = new List<GameObject>();//This will store all saints played in order to "un-sanctify" them 



    //Sound
    //public List<AudioSource> VoiceCorruptedGrounds = new List<AudioSource>();
    //public List<AudioSource> VoiceCorruptingGaze = new List<AudioSource>();
    //public List<AudioSource> VoicePurification = new List<AudioSource>();
    //public List<AudioSource> VoiceTakeDamage = new List<AudioSource>();



    private float timer;
    private int loop;

    private bool IsDead;

    private void Start()
    {
        SuperSecretBalanceMechanic = 3;
        Health = 4;
        BossHealthbar.SetActive(false);//Sets his healthbar to inactive
    }

    public void Activate()
    {
        //Testing
        //Purification();

        BossHealthbar.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        NewPos = new Vector2(6.6f, 1.59707f);
        speed = 0.9f;

        CD2 = 1;
        CD3 = 3;

        Abilities.Add("Corrupted_Grounds");//Zone Debuff - CD1 (1 Turn CD)
        Abilities.Add("Corrupting_Gaze");//Special Unit - CD2 (2 Turn CD)
        Abilities.Add("Purification");//Purify Zone - CD3 (3 Turn CD)

        Abilities.Add("Desperate_Rage");//Card Removal (Passive)

        for (int deactivated = 0; deactivated < DeactivateTowerSpots.Count; deactivated++)//make space for the boss by destroying the last row
        {
            CurrentTowerSpot = DeactivateTowerSpots[deactivated];
            Destroy(CurrentTowerSpot);
        }
    }

    private void Update()
    {
        if (IsActive)
        {
            BossHealth.value = Health;
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, NewPos, speed * Time.deltaTime);//move dramatically to position

            timer += Time.deltaTime;

            if(timer >= 18)//time ebfore boss uses ability;
            {
                LoopErrorFix = 15;//This will prevent the loop from going infinitely if all abilities are on cooldown (which is possible specifically for this boss, but im not willing to re-design the ability cooldown to fix this)

                for (loop = 0; loop < 1; loop++)
                {
                    LoopErrorFix--;

                    if (LoopErrorFix == 0)
                    {
                        CD1--;
                        CD2--;
                        CD3--;
                    }

                    RandomAbility = Random.Range(0, 3);//choose one random ability
                    if (RandomAbility == 0)
                        Corrupted_Grounds();
                    else if (RandomAbility == 1)
                        Corrupting_Gaze();
                    else if (RandomAbility == 2)
                        Purification();
                }
                Debug.Log("I used" + Abilities[RandomAbility]);
                CD1--;
                CD2--;
                CD3--;
                timer = 0;
            }
        }

        if (DeactivateDelay)//This is the Corrupted_Grounds Ability
        {
            AbilityDelay += Time.deltaTime;

            if(AbilityDelay <= 16)
            {
                if(CurrentTemporarySpotDisable != null)
                    CurrentTemporarySpotDisable.SetActive(false);

                CurrentLaneChange.SetActive(true);
            }
            else
            {
                CurrentLaneChange.SetActive(false);
                CurrentTemporarySpotDisable.SetActive(true);
                DeactivateDelay = false;
            }
        }

        if (SanctuaryPlaced)//This is Purification Ability
        {
            if((timer >= 17 && !SaintLives) || SaintDied)//Time window before the sanctuary disappears
            {
                Spawner.GetComponent<EnemySpawning>().delayBetweenSpawnsMin = 8;
                Spawner.GetComponent<EnemySpawning>().delayBetweenSpawnsMax = 12;
                activeSpot.GetComponent<TowerSpotsScript>().IsSanctuary = false;
                activeSpot.GetComponent<Image>().color = Color.black;
                Destroy(sanctuary);
                SanctuaryPlaced = false;
            }

            if (SaintLives)//If spot becomes occupied, boss becomes angy
            {
                SanctuaryTimer += Time.deltaTime;

                //Halve the spawning time
                Spawner.GetComponent<EnemySpawning>().delayBetweenSpawnsMin = 4;
                Spawner.GetComponent<EnemySpawning>().delayBetweenSpawnsMax = 6;
            }

            if(SanctuaryTimer >= 15)//Complete Purification
            {
                Health--;
                Desperate_Rage();

                //Un-Sanctify all units to make sure the current saint won't mess with the next use of this ability
                for (int Animal = 0; Animal < PlayedSaints.Count; Animal++)
                {
                    if(PlayedSaints[Animal] != null)
                    {
                        PlayedSaints[Animal].GetComponent<UnitPrototypeScript>().IsSaint = false;

                        PlayedSaints[Animal].GetComponent<UnitPrototypeScript>().UnSanctifyUnit();
                    }
                } 
                Spawner.GetComponent<EnemySpawning>().delayBetweenSpawnsMin = 8;
                Spawner.GetComponent<EnemySpawning>().delayBetweenSpawnsMax = 12;
                activeSpot.GetComponent<TowerSpotsScript>().IsSanctuary = false;
                activeSpot.GetComponent<Image>().color = Color.black;
                Destroy(sanctuary);
                SanctuaryPlaced = false;
            }

        }


        if (Health == 0)
            IsDead = true;

        if (IsDead == true)
        {
            if (!hasBeenDefeated)
            {
                WinCondition.GetComponent<Victory>().Win();
                //AddBossDefeatToBestiary(); //Sebastian please fix this, i dont know what to do
                hasBeenDefeated = true;
            }
        }
    }


    void Desperate_Rage()//Ability 0 (Special Passive)
    /*The Corruption gets even angrier as it weakens in its power, 
     * driving it to utter madness. When The Corruption takes damage, 
     * it uses this ability and corrupts one of your cards from your hand. 
     * A corrupted card cannot be played.*/
    {
        //Do stuff
    }

    void Corrupted_Grounds()//Ability 1
    /*The Corruption starts taking over your land, corrupting it to its will. 
     * One of your lanes starts oozing purple liquid and becomes corrupted for 16 seconds. 
     * While corrupted in this way, you cannot place any units in that lane until The Corruptions releases its grasp on it.*/
    {
        if (CD1 <= 0)
        {
            //Do stuff
            AbilityDelay = 0;
            RandomLane = Random.Range(1, 5);

            if (RandomLane == 1)
            {
                CurrentTemporarySpotDisable = TemporarySpotDisableL1;
                CurrentLaneChange = LaneChangeL1;
            }
            else if (RandomLane == 2)
            {
                CurrentTemporarySpotDisable = TemporarySpotDisableL2;
                CurrentLaneChange = LaneChangeL2;
            }
            else if (RandomLane == 3)
            {
                CurrentTemporarySpotDisable = TemporarySpotDisableL3;
                CurrentLaneChange = LaneChangeL3;
            }
            else if (RandomLane == 4)
            {
                CurrentTemporarySpotDisable = TemporarySpotDisableL4;
                CurrentLaneChange = LaneChangeL4;
            }

            DeactivateDelay = true;

            Debug.Log("Im using Lane" + CurrentTemporarySpotDisable);

            CD1 = 2;
        }
        else
            loop--;
    }

    void Corrupting_Gaze()//Ability 2
    /*The Corruption's gaze is so powerful it can corrupt any living creature, 
     * turning it against its own will, under the command of the entity. 
     * Apart from sending businessmen and beasts, 
     * The Corruption will corrupt the units from your deck and send them against you.*/
    {
        if (CD2 <= 0)
        {
            //Do stuff

            //Balancing the game with a SuperSecret mechanic (don't tell the players)
            for (int loop = 0; loop < 1; loop++)
            {
                UnitToCorrupt = DeckScript.Deck[Random.Range(0, DeckScript.Deck.Count)];//Random unit from your deck

                if (UnitToCorrupt.manaCost > SuperSecretBalanceMechanic)
                    loop--;
                else
                    SuperSecretBalanceMechanic += 3;
            }

            //Define what to spawn and where
            SpawnLocation = Random.Range(0, SpawnPlace.Count);

            UnitToCorruptSprite = UnitToCorrupt.image;

            CorruptedUnit.GetComponent<SpriteRenderer>().sprite = UnitToCorruptSprite;

            GameObject Enemy = Instantiate(CorruptedUnit, SpawnPlace[SpawnLocation].transform.position, transform.rotation);

            //Enemy.GetComponent<BasicEnemyMovement>().enemy.enemyHealth = 30 + (20 * UnitToCorrupt.manaCost);//Alternative way of balancing this ability
            //Enemy.GetComponent<BasicEnemyMovement>().enemy.attackDamage = 6 + (4 * UnitToCorrupt.manaCost);//Alternative way of balancing this ability
            Enemy.GetComponent<BasicEnemyMovement>().enemy.attackDamage = UnitToCorrupt.punchDamage;
            Enemy.GetComponent<BasicEnemyMovement>().enemy.attackSpeed = UnitToCorrupt.punchRechargeTime;
            int CardHealth = (int)UnitToCorrupt.health;
            Enemy.GetComponent<BasicEnemyMovement>().enemy.enemyHealth = CardHealth;

            Debug.Log("I cost " + UnitToCorrupt.manaCost + "mana");
            Debug.Log("My damage is:  " + Enemy.GetComponent<BasicEnemyMovement>().enemy.attackDamage);
            Debug.Log("My health is:  " + Enemy.GetComponent<BasicEnemyMovement>().enemy.enemyHealth);



            CD2 = 3;
        }
        else
            loop--;
    }


    void Purification()//Ability 3
    /*When fighting The Corruption, its roots start coming out of the ground. 
     * By sending one of your units to that spot, it can be purified to push back the entity. 
     * The spot disappears after 5 seconds if no unit is placed within that time. 
     * When a unit is placed in the "purification zone", it starts the purification process, which takes 15 seconds. 
     * During that time the unit becomes vulnurable and can't attack. 
     * In additon, The Corruption will become aggressive and send more units during that period. 
     * Once the zone is cleansed, The Corruption will take a point of damage and trigger its "Desperate Rage" ability.*/
    {
        if (CD3 <= 0)//Do this ability
        {
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
                //Input for voice later
                //int RandomVoice = Random.Range(0, VoicePropertyBusiness.Count);
                //VoicePropertyBusiness[RandomVoice].Play();

                //Visual hints for the player
                activeSpot.GetComponent<Image>().color = Color.yellow;
      
                sanctuary = Instantiate(Sanctuary);
                sanctuary.transform.position = activeSpot.transform.position;
                sanctuary.transform.position = new Vector3(activeSpot.transform.position.x, activeSpot.transform.position.y, 0); //An attempt to fix Sign placement.
                //sanctuary.transform.SetParent(transform); //Places all sanctuaries as children for the Boss.              

                //Reset values
                SaintLives = false;
                SaintDied = false;
                SanctuaryTimer = 0;

                activeSpot.GetComponent<TowerSpotsScript>().IsSanctuary = true;
                SanctuaryPlaced = true;



                CD3 = 4;//Set cooldown
            }
        }
        else
            loop--;
    }
}
