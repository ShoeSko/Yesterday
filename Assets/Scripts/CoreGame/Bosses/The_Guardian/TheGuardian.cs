using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheGuardian : MonoBehaviour
{
    private List<string> Abilities = new List<string>();

    public List<GameObject> DeactivateTowerSpots = new List<GameObject>();
    private GameObject CurrentTowerSpot;

    public GameObject Dialoguecode;

    public GameObject BossHealthbar;
    public Slider BossHealth;
    public GameObject WinCondition;
    private bool hasBeenDefeated;

    //setup
    private Rigidbody2D rb;
    public bool IsActive;
    private Vector2 NewPos;
    private float speed;
    public int Health = 5;//boss' health

    //This will keep track of boss' cooldowns
    private int CD1;
    private int CD2;
    private int CD3;

    private int RandomAbility;

    //Ability_Wrath_of_the_Forest
    public GameObject NaturesWrath;
    public List<GameObject> SpawnPoints = new List<GameObject>();
    private int spawnLocation;

    //Ability_Eclipse
    public GameObject Darkness;
    private bool UsedEclipse;
    private Rigidbody2D DarknessRB;
    private float DarknessSpeed = 2;
    private Vector3 DarknessStartingPos;
    private Vector3 DarknessEndingPos;
    private float EclipseDuration;//Duration of the darkness effect

    //Ability_Motherly_Embrace
    private GameObject VisibleEnemy;
    private GameObject MothersChosen;
    private bool ChosenOne;

    //public List<AudioSource> VoiceWrathOfTheForest = new List<AudioSource>();
    //public List<AudioSource> VoiceEclipse = new List<AudioSource>();
    //public List<AudioSource> VoiceMotherlyEmbrace = new List<AudioSource>();
    //public List<AudioSource> VoiceTakeDamage = new List<AudioSource>();


    private float timer;
    private int loop;

    private bool IsDead;


    void Start()
    {
        Health = 5;
        BossHealthbar.SetActive(false);

        Darkness.SetActive(true);
        DarknessRB = Darkness.GetComponent<Rigidbody2D>();
        DarknessStartingPos = Darkness.transform.position;
        DarknessEndingPos = new Vector3(5, DarknessStartingPos.y, DarknessStartingPos.z);
    }

    public void Activate()
    {
        if (FindObjectOfType<SaveSystem>())
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>();
            saving.data.bossMeetList[1] = true;
        }

        BossHealthbar.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        NewPos = new Vector2(6f, 1.59707f);
        speed = 0.9f;

        CD3 = 1;

        Abilities.Add("Wrath_of_the_Forest");//Damage spikes
        Abilities.Add("Eclipse");//Mist screen coverup
        Abilities.Add("Motherly_Embrace");//Buff

        for (int deactivated = 0; deactivated < DeactivateTowerSpots.Count; deactivated++)//make space for the boss by destroying the last row
        {
            CurrentTowerSpot = DeactivateTowerSpots[deactivated];
            Destroy(CurrentTowerSpot);
        }
    }

    void Update()
    {
        if(IsActive == true)
        {
            BossHealth.value = Health;
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, NewPos, speed * Time.deltaTime);//move dramatically to position

            timer += Time.deltaTime;

            if (timer >= 12)//The time before boss uses an ability
            {
                for (loop = 0; loop < 1; loop++)
                {
                    RandomAbility = Random.Range(0, 3);//choose one random ability
                    if (RandomAbility == 0)
                        Wrath_of_the_Forest();
                    else if (RandomAbility == 1)
                        Eclipse();
                    else if (RandomAbility == 2)
                        Motherly_Embrace();
                }
                Debug.Log("I used" + Abilities[RandomAbility]);
                CD1--;
                CD2--;
                CD3--;
                timer = 0;
            }

            /*
            if (Input.GetKey(KeyCode.Alpha5) && Input.GetKeyDown(KeyCode.Alpha7))//cheat to damage boss
            {
                Health--;
            }
            */
        }

        if (UsedEclipse == true)//The "Eclipse" ability
        {
            DarknessRB.transform.position = Vector3.MoveTowards(DarknessRB.transform.position, DarknessEndingPos, DarknessSpeed * Time.deltaTime);
            EclipseDuration += Time.deltaTime;

            if (EclipseDuration >= 15)
                UsedEclipse = false;
        }
        else
        {
            DarknessRB.transform.position = Vector3.MoveTowards(DarknessRB.transform.position, DarknessStartingPos, DarknessSpeed * Time.deltaTime);
            EclipseDuration = 0;
        }

        if(ChosenOne == true)
        {
            if(MothersChosen == null)
            {
                //int RandomVoice = Random.Range(0, VoiceTakeDamage.Count);
                //VoiceTakeDamage[RandomVoice].Play();

                Health--;
                ChosenOne = false;

                if(Health != 0)
                    Dialoguecode.GetComponent<DialogueCode>().PreperationMode();
            }
        }

        if (Health == 0)
            IsDead = true;

        if(IsDead == true)
        {
            BossDialogue.Boss = 1;
            if (!hasBeenDefeated)
            {
                if (FindObjectOfType<SaveSystem>())
                {
                    SaveSystem saving = FindObjectOfType<SaveSystem>();
                    saving.data.bossList[1] = true;
                    Debug.Log("I saved");
                }

                WinCondition.GetComponent<Victory>().Win();
                AddBossDefeatToBestiary(); //With the defeat, the boss is unlocked in the bestiary.
                hasBeenDefeated = true;
            }
        }
    }


    void Wrath_of_the_Forest()//Ability 1
    /*The Guardian accumulates all of her wrath into a singular strike. 
     * She sends out a trail of throny vines, dealing 50 damage to all of your units in that lane. 
     * Guardian's allies are unaffected by this ability.*/
    {
        if (CD1 <= 0)
        {
            //Do stuff
            //int RandomVoice = Random.Range(0, VoiceWrathOfTheForest.Count);
            //VoiceWrathOfTheForest[RandomVoice].Play();

            spawnLocation = Random.Range(0, 4);
            Instantiate(NaturesWrath);
            NaturesWrath.transform.position = SpawnPoints[spawnLocation].transform.position;

            CD1 = 1;
        }
        else
            loop--;
    }

    void Eclipse()//Ability 2
    /*The Guardian causes the moon itself to shadow her units. 
     * For the duration of the Eclipse (10 sec), the right side of the screen is dark,
     * blocking vision of all incoming enemies, as well as your existing units on that side.*/
    {
        if (CD2 <= 0)
        {
            //Do stuff
            //int RandomVoice = Random.Range(0, VoiceEclipse.Count);
            //VoiceEclipse[RandomVoice].Play();

            UsedEclipse = true;

            CD2 = 3;
        }
        else
            loop--;
    }

    void Motherly_Embrace()//Ability 3
    /*The Guardian's connection to nature allows her to strenghten her allies. 
     * The Guardian chooses a random enemy unit on the board. That unit's health, damage and attack speed is increased by 100% 
     * a visible link is created between The Guardian and that unit. 
     * When the buffed unit dies, it deals 50 damage to the boss and the link in severed.*/
    {
        if (CD3 <= 0)
        {
            //Do stuff
            //int RandomVoice = Random.Range(0, VoiceMotherlyEmbrace.Count);
            //VoiceMotherlyEmbrace[RandomVoice].Play();

            VisibleEnemy = GameObject.FindGameObjectWithTag("Enemy");
            if (VisibleEnemy != null)
            {
                if(VisibleEnemy.GetComponent<BasicEnemyMovement>().chosenByMom == false)//Can't buff the same guy twice
                {
                    VisibleEnemy.GetComponent<BasicEnemyMovement>().MotherlyEmbraceBuff();
                    VisibleEnemy.transform.localScale = VisibleEnemy.transform.localScale * 1.5f;

                    MothersChosen = VisibleEnemy;
                    ChosenOne = true;

                    CD3 = 2;
                }
                else if (CD1 > 0 && CD2 > 0)
                {
                    CD1--;
                    CD2--;
                    loop--;
                }
                else
                    loop--;
            }
            else if (CD1 > 0 && CD2 > 0)
            {
                CD1--;
                CD2--;
                loop--;
            }
            else
                loop--;
        }
        else
            loop--;
    }

    #region Bestiary
    private void AddBossDefeatToBestiary()
    {
        if (FindObjectOfType<SaveSystem>())
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>();

            saving.data.bossList[1] = true; //Boss index 1 has been defeated Moth.
        }
    }
    #endregion
}