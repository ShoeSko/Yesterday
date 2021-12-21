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


    //Ability_Purification



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
        Health = 4;
        BossHealthbar.SetActive(false);//Sets his healthbar to inactive
    }

    public void Activate()
    {
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


    void Desperate_Rage()//Ability 0 (Soecial Passive)
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
        if (CD3 <= 0)
        {
            //Do stuff

            CD3 = 4;
        }
        else
            loop--;
    }
}
