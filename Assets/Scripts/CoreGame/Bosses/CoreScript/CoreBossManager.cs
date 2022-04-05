using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreBossManager : MonoBehaviour
{
    #region Parameters
    public List<BossScriptableObject> Bosses = new List<BossScriptableObject>();
    private BossScriptableObject currentBoss;

    private string bossname;
    public int health;

    private List<Sprite> bossSprites = new List<Sprite>();
    private Color healthbarColor;

    private int useAbilityDelay;
    private List<int> abilityCD;
    private List<int> startabilityCD;

    private List<AudioClip> abilitySFX = new List<AudioClip>();

    private float xpos;
    private float ypos;
    private float speed;//= 0.9f;

    #endregion

    #region Setup
    private float timer;//Used as countdown for ability CD
    private Sprite currentSprite;
    private Rigidbody2D rb;
    public bool IsActive;
    private Vector2 NewPos; //= new Vector2(6f, 1.59707f);

    public List<GameObject> DeactivateTowerSpots = new List<GameObject>();
    private GameObject CurrentTowerSpot;

    public GameObject Dialoguecode;
    public GameObject GameManager;
    private int randomAbility;
    public int loop;
    private int failsafe;//Makes sure ability cd won't get stuck

    public GameObject BossHealthbar;
    public Text BossHealthbarName;
    public Slider BossHealthslider;
    public Image BossHealthbarColor;
    public GameObject WinCondition;
    private bool hasBeenDefeated;

    #endregion

    public void Activate()
    {
        PullInformation();

        //Saving system
        if (FindObjectOfType<SaveSystem>())
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>();
            saving.data.bossMeetList[GameManager.GetComponent<NewCardHandScript>().whichStage] = true;//Save information of current boss to the beastiary
        }

        //Deactivate Slots
        for (int deactivated = 0; deactivated < DeactivateTowerSpots.Count; deactivated++)//make space for the boss by destroying the last row
        {
            CurrentTowerSpot = DeactivateTowerSpots[deactivated];
            Destroy(CurrentTowerSpot);
        }

        //Abilities
        for(int i = 0; i < abilityCD.Count; i++)
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

        //Physics
        NewPos = new Vector2(xpos, ypos);
        rb = GetComponent<Rigidbody2D>();
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
                for (loop = 0; loop < 1; loop++)
                {
                    failsafe++;

                    if(failsafe < 20)
                    {
                        randomAbility = Random.Range(0, abilityCD.Count);//choose one random ability

                        if (abilityCD[randomAbility] > 0)
                            loop--;
                    }
                    else//This is an emergency break in case no ability is off cooldown for any reason
                    {
                        for (int ability = 0; ability < abilityCD.Count; ability++)
                        {
                            abilityCD[ability]--;
                        }

                        loop--;
                    }
                }

                failsafe = 0;
                timer = 0;
            }
        }
    }


    private void PullInformation()
    {
        currentBoss = Bosses[GameManager.GetComponent<NewCardHandScript>().whichStage];//Define which boss I should read

        bossname = currentBoss.Name;
        health = currentBoss.BossHealth;
        bossSprites = currentBoss.BossSprites;
        healthbarColor = currentBoss.BossSliderColor;
        useAbilityDelay = currentBoss.UseAbilityDelay;
        abilityCD = currentBoss.AbilityCooldown;
        startabilityCD = currentBoss.StartAbilityCooldown;
        abilitySFX = currentBoss.AbilitySFX;
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