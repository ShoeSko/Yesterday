using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheBossClass_Parent : MonoBehaviour     //Ability reference to "BossAbilities" scripts
{
    //All bosses must use class "BossAbilites : TheBossClass_Parent"
    //All bosses must reffer to all voids in this class

    delegate void AbilityList();//Stores ability voids
    List<AbilityList> Ability = new List<AbilityList>();

    private void Start()
    {
        Ability.Add(Ability1);
        Ability.Add(Ability2);
        Ability.Add(Ability3);
    }

    public void UseAbility(int whatAbility)
    {
        Ability[whatAbility]();
    }

    public virtual void Ability1() { }
    public virtual void Ability2() { }
    public virtual void Ability3() { }
}

public class CoreBossManager : MonoBehaviour
{
    #region Parameters
    public List<BossScriptableObject> Bosses = new List<BossScriptableObject>();
    private BossScriptableObject currentBoss;

    private string bossname;
    [HideInInspector] public int health;

    private List<Sprite> bossSprites = new List<Sprite>();
    private Color healthbarColor;

    private int useAbilityDelay;
    private List<int> abilityCD;
    private List<int> startabilityCD;

    private List<AudioClip> abilitySFX = new List<AudioClip>();
    [HideInInspector]public AudioClip soundtrack;

    private float xpos;
    private float ypos;
    private float speed; //Standard = 0.9

    #endregion

    #region Setup
    private float timer;//Used as countdown for ability CD
    private Sprite currentSprite;
    private Rigidbody2D rb;
    [HideInInspector] public bool IsActive;
    private Vector2 NewPos; //= new Vector2(6f, 1.59707f);

    public List<GameObject> DeactivateTowerSpots = new List<GameObject>();
    private GameObject CurrentTowerSpot;

    public GameObject Dialoguecode;
    public GameObject GameManager;
    private int randomAbility;
    private int loop;

    public GameObject BossHealthbar;
    public Text BossHealthbarName;
    public Slider BossHealthslider;
    public Image BossHealthbarColor;
    public GameObject WinCondition;
    private bool hasBeenDefeated;

    [HideInInspector]public int defineBoss;
    [HideInInspector] public TheBossClass_Parent bossClass;
    #endregion


    void Start()
    {

    }
    //
    public void Activate()
    {
        PullInformation();

        GameObject spawnedBoss = new GameObject();
        defineBoss = GameManager.GetComponent<NewCardHandScript>().RandomBoss;
        Debug.Log("The defineboss is: " + defineBoss);
        switch (defineBoss)//Add more scripts when implementing more bosses
        {
            case 0:
                bossClass = spawnedBoss.AddComponent<CorporateAbilities>();
                break;
            case 1:
                bossClass = spawnedBoss.AddComponent<GuardianAbilities>();
                break;
            case 2:
                bossClass = spawnedBoss.AddComponent<CorruptionAbilities>();
                break;
            default:
                break;
        }


        //Saving system
        if (FindObjectOfType<SaveSystem>())
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>();
            saving.data.bossMeetList[GameManager.GetComponent<NewCardHandScript>().RandomBoss] = true;//Save information of current boss to the beastiary
        }




        //Deactivate Slots
        for (int deactivated = 0; deactivated < DeactivateTowerSpots.Count; deactivated++)//make space for the boss by destroying the last row
        {
            CurrentTowerSpot = DeactivateTowerSpots[deactivated];
            Destroy(CurrentTowerSpot);
        }

        //Abilities
        for (int i = 0; i < abilityCD.Count; i++)
        {
            abilityCD[i] = startabilityCD[i];

        }

        //Visual
        currentSprite = bossSprites[0];
        this.GetComponent<SpriteRenderer>().sprite = currentSprite;

        BossHealthbar.SetActive(true);
        BossHealthbarName.text = bossname;
        BossHealthbarColor.color = healthbarColor;
        BossHealthslider.maxValue = health;
        BossHealthslider.value = health;

        //Physics
        NewPos = new Vector2(xpos, ypos);
        rb = GetComponent<Rigidbody2D>();

        Debug.Log("new pos is: " + NewPos);
        Debug.Log("my sprite should be: " + bossSprites[0]);
        Debug.Log("my sprite is: " + GetComponent<SpriteRenderer>().sprite);

        IsActive = true;
    }

    private void Update()
    {
        if (IsActive == true)
        {
            BossHealthslider.value = health;
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, NewPos, speed * Time.deltaTime);//move dramatically to new position

            timer += Time.deltaTime;


            if (timer >= useAbilityDelay)//The time before boss uses an ability
            {
                if(abilityCD[0] > 0 && abilityCD[1] > 0 && abilityCD[2] > 0)//Makes sure the loop below doesn't get stuck
                {
                    for (int ability = 0; ability < abilityCD.Count; ability++)
                    {
                        abilityCD[ability]--;
                    }
                }

                for (loop = 0; loop < 1; loop++)
                {
                    randomAbility = Random.Range(0, abilityCD.Count);//choose one random ability

                    if (abilityCD[randomAbility] > 0)
                        loop--;
                }

                bossClass.GetComponent<TheBossClass_Parent>().UseAbility(randomAbility);

                timer = 0;
            }
        }
    }


    private void PullInformation()
    {
        currentBoss = Bosses[GameManager.GetComponent<NewCardHandScript>().RandomBoss];//Define which boss I should read
        Debug.Log("Current boss is: " + currentBoss);

        bossname = currentBoss.Name;
        health = currentBoss.BossHealth;

        bossSprites = currentBoss.BossSprites;
        healthbarColor = currentBoss.BossSliderColor;

        useAbilityDelay = currentBoss.UseAbilityDelay;
        abilityCD = currentBoss.AbilityCooldown;
        startabilityCD = currentBoss.StartAbilityCooldown;

        abilitySFX = currentBoss.AbilitySFX;
        soundtrack = currentBoss.Soundtrack;

        xpos = currentBoss.Xpos;
        ypos = currentBoss.Ypos;
        speed = currentBoss.IntroSpeed;
    }

    private void AddBossDefeatToBestiary()
    {
        if (FindObjectOfType<SaveSystem>())
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>();

            saving.data.bossList[GameManager.GetComponent<NewCardHandScript>().whichStage] = true;//Put in the boss to beastiary after defeating it
        }
    }
}