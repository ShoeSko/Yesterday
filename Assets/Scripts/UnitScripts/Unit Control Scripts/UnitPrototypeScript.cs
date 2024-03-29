using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UnitPrototypeScript : MonoBehaviour
{
    #region Variables
    public CardScript Unit; //Scriptableobject template for all Units.
    [Header("Unit Controls")]
    [Range(0, 100)] [HideInInspector] public float health;

    [Header("Unit Attack")]
    [Tooltip("Is this unit going to shoot?")] private bool isShooter;
    [Tooltip("Is this unit going to punch?")] private bool isPunching;
    [Tooltip("Is this Unit special in some way?")] private bool isSpecial;
    [Tooltip("The type of damage done/taken")] [HideInInspector] public int damageType;

    [Header("Shooting")]
    [Tooltip("Time between each shot")] [Range(0, 100)] private float shootRechargeTime;
    [Tooltip("The speed of the projectile being fired")] [Range(0, 100)] private float projectileSpeed;
    [Tooltip("Prefab of the projectile to be shot")] private GameObject projectilePrefab;
    [Tooltip("The damage the projectile will deal")] [Range(0, 100)] [HideInInspector]public int projectileDamage;
    [Tooltip("What layer are enemies on?")] private LayerMask shootingTargetLayer;
    [Tooltip("What layer is the range wall on?")] private LayerMask edgeOfRangeLayer;
    [Tooltip("How many targets will be hit?")] [HideInInspector] public int targetsToShoot;

    private bool isRecharging; //Is the unit recharging
    private Vector3 originForAim; //Where does the unit aim from?
    private Vector3 directionForAim; //In which direction does the unit aim?
    private float hitRange; //How far can the unit shoot?

    [Header("Punching")]
    [Tooltip("Time between each punch")] [Range(0, 100)] private float punchRechargeTime;
    [Tooltip("The damage the punch will deal")] [Range(0, 100)] private int punchDamage;
    [Tooltip("What layer are enemies on?")] private LayerMask punchingTargetLayer;
    [Tooltip("This does not change the actual range(Use scrub for that)")] [Range(0, 2)][SerializeField] private float punchRange;
    [Tooltip("How many targets will be hit?")] private int targetsToPunch;
    [Tooltip("Can it hit all targets?")] private bool canPunchEverything;
    [Tooltip("Does the unit give knockback on hit?")] private bool hasKnockback;
    [Tooltip("How powerful is the knockback?")] private float knockbackPower;

    [Tooltip("Where does it attack from?")] [SerializeField] private Transform punchPosition;
    private bool hasPunched;
    private bool isPunchRecharging;
    private int punchTargetAmount; //Stores the punch value.

    [Header("Special")]
    [Tooltip("Does the unit die and kill the enemy on contact?")]private bool isSacrificialKill;

    [Tooltip("Does the unit buff the ally in front of it?")] private bool isSupportExpert;
    [Tooltip("What layer is allies on?")] private LayerMask allyLayerToTarget;
    [Tooltip("The range that the support can reach")]private float hitAllyRange;
    [Tooltip("How much bigger will the unit be?")] private Vector3 sizeBuff;
    [Tooltip("How much the health should be buffed")] private float healthBuff;
    [Tooltip("How much the damage should be buffed")] private float damageBuff;
    private bool hasInstakilled;
    private bool isMultiPurpose;

    private bool hasBuffedAlly = false; //Has the unit given it's buff?

    private float healthToBuff;
    private float damageProjectileToBuff;
    private float damagePunchToBuff;
    private Vector3 originForAllyAim; //Where does the unit aim from?
    private Vector3 directionForAllyAim; //In which direction does the unit aim?

    private bool isDead;

    [Header("Unit index")]
    [Tooltip("What index is the unit?")] public int unitIndex;

    [Header("Spell effects")]
    private int shootDamageSave; // Saves the shoot Damage
    private float shootSpeedSave; // Saves the shoot speed
    private int punchDamageSave; // Saves the Punch Damage
    private float punchSpeedSave; // Saves the Punch speed
    private float healthSave; //Saves the health;

    [Header("Unit Damage Taken Visuals")]
    private Animator animatorOfUnit;

    //Unit abilities:
    private Collider2D hitEnemy;//Used to access the current punched target outside the punch statement
    private bool OneTimeTrigger;
    private bool CHARRGE;
    private int HitCounter;
    private int UnitsInLane;
    private float Delay;
    private float countdown;
    private int BonusDamageInt;
    private static bool MafiaBuffPig;
    private static bool MafiaBuffCow;
    private static bool MafiaCancelCow;
    private static bool MafiaCancelPig;
    private GameObject[] lane;
    private List<GameObject> BuffedCards = new List<GameObject>();
    private Vector3 StartingPos;

    private float BasicHealth;//Variable to store the starting health of the card
    private int BasicDamage;//Variable to store the starting punching damage of the card
    private int BasicShootingDamage;//Variable to store the starting shooting damage of the card

    private GameObject DeckObject;
    private DeckScript DeckCode;

    [Header("Lane Placement")]
    public int laneNumberUnit;
    public int lanePlacementUnit;

    public bool IsSaint;//Corruption Boss Effect
    private bool WasShooter;//Used to turn back on the unit's ability to shoot
    private bool WasFighter;//Used to turn back on the unit's ability to punch

    #endregion
    #region Standard Voids
    private void Start()
    {
        UnitInfoFeed(); //All info of the Unit is recorded here.
        if(isShooter && isPunching)
        {
            isMultiPurpose = true; //If  it can both attack and punch
        }
        if (isShooter) { Aim(); }
        if (isSupportExpert) { AllyAim(); }
        healthSave = health; // Saves the health value at max.

        if(EnemySpawning.s_isCoreGame == true) //Makes sure the animation does not occur in the card selection
        {
            animatorOfUnit = GetComponent<Animator>();
            animatorOfUnit.enabled = true;
        }

        //Card abilities:

        if(Unit.cardName == "Duckey")
        {
            ManaSystem.CurrentMana += Unit.manaCost;
            Debug.Log("Duckey restored " + Unit.manaCost);
        }

        if(Unit.cardName == "Pigster" || Unit.cardName == "Cowster")
        {
            BasicHealth = health;
            BasicDamage = punchDamage;
            BasicShootingDamage = projectileDamage;
        }

        if (Unit.cardName == "Croc")
        {
            BasicHealth = health;
            BasicDamage = punchDamage;
        }

        if(Unit.cardName == "Squirrels")
        {
            DeckObject = GameObject.Find("Deck");
            DeckCode = DeckObject.GetComponent<DeckScript>();
            DeckCode.CheckForRodents();

            //Debug.Log("There are " + DeckCode.Rodents + " rodents in this deck");

            health += 15 * DeckCode.Rodents;
            punchDamage += 2 * DeckCode.Rodents;

            //Debug.Log("My Health is" + health);
            //Debug.Log("My Damage is" + punchDamage);

            DeckCode.Rodents = 0;
        }

        if(Unit.cardName == "Evil Rabbit")
        {
            GameObject [] RandomEnemy = GameObject.FindGameObjectsWithTag("Enemy");

            //Debug.Log("There are this many enemies" + RandomEnemy.Length);
            
            for(int EnemiesToHit = 0; EnemiesToHit < 3; EnemiesToHit++)
            {
                if(RandomEnemy.Length > EnemiesToHit)
                {
                    int RandomizedNumber = Random.Range(0, RandomEnemy.Length);

                    RandomEnemy[RandomizedNumber].GetComponent<BasicEnemyMovement>().TakeDamage(punchDamage, hasKnockback, knockbackPower, damageType);
                }
            }
        }

        if(Unit.cardName == "Saddler")
        {
            DeckObject = GameObject.Find("Deck");
            DeckCode = DeckObject.GetComponent<DeckScript>();

            DeckCode.OstrichCowardness();
        }

        if(Unit.cardName == "CowBoy")
        {
            GameObject HandReset = GameObject.Find("HANDscript");
            HandReset.GetComponent<NewCardHandScript>().ReRollHand();
        }

        if(Unit.cardName == "Cheer Hamster")
        {
            GameObject Manager = GameObject.Find("MANAger");

            Manager.GetComponent<ManaSystem>().manaGainSpeed -= 0.3f;
        }

        if(Unit.cardName == "Ninjey")
        {
            BasicHealth = health;
        }

        if(Unit.cardName == "Black Rooster")
        {
            int friendly;

            friendly = GameObject.FindGameObjectsWithTag("Obstacle").Length;

            health += friendly * 10;
            punchDamage += friendly * 3;

            //Debug.Log("My new health is" + health);
            //Debug.Log("My new damage is" + punchDamage);

            //Debug.Log("Friendly units:" + friendly);
            //Debug.Log("Enemy units:" + friendly);
        }

        if (Unit.cardName == "Maid")
        {
            Debug.Log("my number is " + lanePlacementUnit);
            Debug.Log("my lane is " + laneNumberUnit);

            if (laneNumberUnit == 1)
                lane = LanePlacedUnits.s_lane1;
            else if (laneNumberUnit == 2)
                lane = LanePlacedUnits.s_lane2;
            else if (laneNumberUnit == 3)
                lane = LanePlacedUnits.s_lane3;
            else if (laneNumberUnit == 4)
                lane = LanePlacedUnits.s_lane4;

        }

        if (Unit.cardName == "Shiburai")
        {
            DeckObject = GameObject.Find("Deck");
            DeckCode = DeckObject.GetComponent<DeckScript>();

            DeckCode.LastShiburai();

            if(DeckCode.Shiburais == 1)
            {
                canPunchEverything = true;
                targetsToPunch = 5;
                health += 35;
                punchDamage += 10;
            }
            else
            {
                canPunchEverything = false;
                targetsToPunch = 1;
            }

            Debug.Log(DeckCode.Shiburais);
            Debug.Log(health);
        }

        if(Unit.cardName == "RatKing")
        {
            DeckObject = GameObject.Find("Deck");
            DeckCode = DeckObject.GetComponent<DeckScript>();

            DeckCode.RatKingEffect();
        }

        if (Unit.cardName == "Frogger")
        {
            StartingPos = transform.position;
            BasicDamage = punchDamage;
        }

        if (Unit.cardName == "Best boy")
        {
            GameObject.Find("HANDscript").GetComponent<NewCardHandScript>().CreateFindMaster();
        }
    }

    private void Update()
    {
        if (isShooter) { ShootProjectile(); }
        else if (isPunching) { Punch(); }
        else if (isSpecial) { Special(); }
        if(EnemySpawning.s_isCoreGame == true)
        {
        CurrentDamageTaken();
        Death();
        }
        if (IsSaint)
            SaintEffects();


        //Card abilities
        if (Unit.cardName == "Pigster")
        {
            MafiaBuffPig = true;

            if (MafiaCancelCow == true)
            {
                health = health / 1.5f;
                punchDamage = BasicDamage;
                OneTimeTrigger = false;
                Debug.Log("Am smol, health is" + health);
                Debug.Log("Am smol, damage is" + punchDamage);
                MafiaCancelCow = false;
            }

            if (MafiaBuffCow == true)
                mafiaAbility();
        }

        if (Unit.cardName == "Cowster")
        {
            MafiaBuffCow = true;

            if (MafiaCancelPig == true)
            {
                health = health / 1.5f;
                projectileDamage = BasicShootingDamage;
                OneTimeTrigger = false;
                Debug.Log("Am smol, health is" + health);
                Debug.Log("Am smol, damage is" + projectileDamage);
                MafiaCancelCow = false;
            }

            if (MafiaBuffPig == true)
                mafiaAbility();
        }

        if (Unit.cardName == "Croc")
        {
            ability_Croc();
        }

        if (Unit.cardName == "Karl")
        {
            if(OneTimeTrigger == false)//Is not attacking
            {
                BasicDamage = punchDamage;
            }
        }

        if (Unit.cardName == "Ninjey")
        {          
            if (health < BasicHealth)//This means the unit got damaged
            {
                ability_Ninjey();
            }

            if(OneTimeTrigger == true)
            {
                countdown += Time.deltaTime;

                if(countdown >= 5)
                {
                    GetComponent<Collider2D>().enabled = true;
                    OneTimeTrigger = false;
                }
            }
        }

        if (Unit.cardName == "Maid")
        {
            Debug.Log(health);

            foreach (GameObject unit in lane)
            {
                if(unit != null)
                {
                    if (BuffedCards.Contains(unit))
                    {
                        //do nothing
                    }
                    else
                    {
                        unit.GetComponent<UnitPrototypeScript>().health += 50;
                        unit.GetComponent<UnitPrototypeScript>().punchDamage += 7;
                        unit.GetComponent<UnitPrototypeScript>().projectileDamage += 1;

                        if(unit.GetComponent<UnitPrototypeScript>().Unit.cardName == "Buffcat")
                        {
                            unit.GetComponent<UnitPrototypeScript>().health += 50;
                            unit.GetComponent<UnitPrototypeScript>().punchDamage += 7;
                            unit.GetComponent<UnitPrototypeScript>().projectileDamage += 1;
                        }

                        BuffedCards.Add(unit);
                    }
                }
            }
        }

        if (Unit.cardName == "Super Bull")
        {
            countdown += Time.deltaTime;

            Debug.Log(countdown);

            if(countdown >= 20)
            {
                punchDamage += 15;
                punchRechargeTime = punchRechargeTime / 1.2f;
                health += 50;
                transform.localScale = transform.localScale * 1.2f;

                countdown = 0;
            }
        }

        if (Unit.cardName == "Black Knight")
        {
            if (laneNumberUnit == 1)
                lane = LanePlacedUnits.s_lane1;
            else if (laneNumberUnit == 2)
                lane = LanePlacedUnits.s_lane2;
            else if (laneNumberUnit == 3)
                lane = LanePlacedUnits.s_lane3;
            else if (laneNumberUnit == 4)
                lane = LanePlacedUnits.s_lane4;

            UnitsInLane = 0;

            foreach (GameObject unit in lane)
            {
                if (unit != null)
                    UnitsInLane++;
            }

            if (UnitsInLane == 1 && OneTimeTrigger == false)
            {
                punchDamage += punchDamage;
                OneTimeTrigger = true;
            }
            else if (UnitsInLane > 1 && OneTimeTrigger == true)
            {
                punchDamage -= punchDamage / 2;
                OneTimeTrigger = false;
            }

            Debug.Log(punchDamage);
        }

        if (Unit.cardName == "The Duelist")
        {
            if (laneNumberUnit == 1)
                lane = LanePlacedUnits.s_lane1;
            else if (laneNumberUnit == 2)
                lane = LanePlacedUnits.s_lane2;
            else if (laneNumberUnit == 3)
                lane = LanePlacedUnits.s_lane3;
            else if (laneNumberUnit == 4)
                lane = LanePlacedUnits.s_lane4;

            foreach (GameObject unit in lane)
            {
                if (unit != null)
                    UnitsInLane++;
            }

            if (UnitsInLane == 1)
            {
                canPunchEverything = true;
                targetsToPunch = 5;
            }
            else
            {
                canPunchEverything = false;
                targetsToPunch = 1;
            }
        }

        if (Unit.cardName == "Wooly Sheep")
        {
            countdown += Time.deltaTime;

            if (health <= 750 && countdown >= 15)
            {
                health += 25;
                countdown = 0;
            }
        }

        if (Unit.cardName == "Laser Horse")
        {
            countdown += Time.deltaTime;

            if(countdown >= 15)
            {
                if(targetsToShoot < 10)
                    targetsToShoot++;

                countdown = 0;
            }
        }

        if (Unit.cardName == "King of the Hill")
        {
            countdown += Time.deltaTime;

            if(countdown >= 10)
            {
                knockbackPower = knockbackPower * 1.1f;

                countdown = 0;
            }
        }

        if(Unit.cardName == "Frogger")
        {
            originForAim = transform.position;
            directionForAim = transform.right;
            RaycastHit2D hitRangeLimit;
            hitRangeLimit = Physics2D.Raycast(originForAim, directionForAim, 999999, edgeOfRangeLayer);
            hitRange = hitRangeLimit.distance;

            RaycastHit2D hit;

            hit = Physics2D.Raycast(originForAim, directionForAim, hitRange, shootingTargetLayer);
            Debug.DrawRay(originForAim, directionForAim * hitRange, Color.red, 10);

            if (hit)
            {
                if (!OneTimeTrigger)
                {
                    CHARRGE = true;
                    BonusDamageInt = 0;
                    Delay = 0;

                    OneTimeTrigger = true;
                }
            }
            else
            {
                CHARRGE = false;
            }

            if (CHARRGE)
            {
                transform.position += Vector3.right * Time.deltaTime * 3;
                countdown += Time.deltaTime;
                float bonusDamage = countdown * 10;
                BonusDamageInt = (int)bonusDamage;
                punchDamage = BasicDamage + BonusDamageInt;
            }
            else
            {
                if(transform.position.x > StartingPos.x)
                {
                    transform.position += Vector3.left * Time.deltaTime * 4;
                }
            }

            if (transform.position.x <= StartingPos.x)
            {
                Delay += Time.deltaTime;

                if(Delay >= 5)
                {
                    OneTimeTrigger = false;
                }
            }
        }
    }
    #endregion
    #region Shooting
    private void ShootProjectile()
    {
        if (isShooter)
        {
            RaycastHit2D hit; //Delegate memory

            hit = Physics2D.Raycast(originForAim, directionForAim, hitRange, shootingTargetLayer);
            Debug.DrawRay(originForAim, directionForAim * hitRange, Color.red,10);
            if (hit)
            {
                if (!isRecharging)
                {
                    GameObject projectileObject = Instantiate(projectilePrefab, transform);
                    Rigidbody2D projectileObjectRg2D = projectileObject.GetComponent<Rigidbody2D>();
                    projectileObject.transform.position = transform.position;
                    projectileObjectRg2D.velocity = new Vector2(projectileSpeed/* * Time.deltaTime*/,0);

                    isRecharging = true;
                    StartCoroutine(ShootRecharge());
                }
            }
        }
    }
    private void Aim()
    {
        originForAim = transform.position;
        directionForAim = transform.right;
        RaycastHit2D hitRangeLimit;
        hitRangeLimit = Physics2D.Raycast(originForAim, directionForAim, 999999, edgeOfRangeLayer);
        hitRange = hitRangeLimit.distance;
    }

    IEnumerator ShootRecharge()
    {
        yield return new WaitForSeconds(shootRechargeTime);
        isRecharging = false;
        yield return null;
    }
    #endregion
    #region Punching
    private void Punch() //Attack time
    {
        if (!canPunchEverything) //Limits the amount of enemies that can be punched at the same time
        {
            if (!hasPunched) //If you have yet to attack
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(punchPosition.position, punchRange, punchingTargetLayer); //Overlap of all Units withing the attack range
                if (enemiesToDamage.Length >= targetsToPunch)
                {
                    punchTargetAmount = targetsToPunch; //Makes it possible to hitt multiple(Can be removed if wanted)
                }
                else { punchTargetAmount = enemiesToDamage.Length; }
                for (int i = 0; i < punchTargetAmount; i++)
                {
                    if (enemiesToDamage[i])
                    {
                        hasPunched = true;
                        if (enemiesToDamage[i].GetComponent<BasicEnemyMovement>())
                        {
                            enemiesToDamage[i].GetComponent<BasicEnemyMovement>().TakeDamage(punchDamage, hasKnockback, knockbackPower, damageType); //Sent attackDamage to Unit
                            hitEnemy = enemiesToDamage[i];
                            OnHitAbility();//Check if the unit has any abilities that trigger when hitting an enemy
                            //print("Punch");
                        }
                        else if (enemiesToDamage[i].GetComponent<GreedyOpportunity>())
                        {
                            enemiesToDamage[i].GetComponent<GreedyOpportunity>().TakeDamage(punchDamage, hasKnockback, knockbackPower); //Sent attackDamage to Unit
                            OnHitAbility();//Check if the unit has any abilities that trigger when hitting an enemy
                            //print("Punch");
                        }
                    }
                }
            }
            else if (hasPunched && !isPunchRecharging) { StartCoroutine(PunchRecharge()); } //Start Coroutine to recharge
        }
        else if (canPunchEverything)
        {
            if (!hasPunched) //If you have yet to attack
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(punchPosition.position, punchRange, punchingTargetLayer); //Overlap of all Units withing the attack range
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    if (enemiesToDamage[i])
                    {
                        hasPunched = true;
                        if (enemiesToDamage[i].GetComponent<BasicEnemyMovement>())
                        {
                        enemiesToDamage[i].GetComponent<BasicEnemyMovement>().TakeDamage(punchDamage, hasKnockback,knockbackPower, damageType); //Sent attackDamage to Unit
                        OnHitAbility();//Check if the unit has any abilities that trigger when hitting an enemy
                        //print("Punch");
                        }
                        else if (enemiesToDamage[i].GetComponent<GreedyOpportunity>())
                        {
                            enemiesToDamage[i].GetComponent<GreedyOpportunity>().TakeDamage(punchDamage, hasKnockback, knockbackPower); //Sent attackDamage to Unit

                        }
                    }
                }
            }
            else if (hasPunched && !isPunchRecharging) { StartCoroutine(PunchRecharge()); } //Start Coroutine to recharge
        }
    }

    private void OnDrawGizmosSelected() //Gizmo to represent attack range.
    {
        if (isPunching)
        {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchPosition.position, punchRange);
        }
    }
    IEnumerator PunchRecharge() //Recharges the attack speed
    {
        isPunchRecharging = true;
        yield return new WaitForSeconds(punchRechargeTime);
        hasPunched = false;
        isPunchRecharging = false;
        yield return null;
    }


    #endregion
    #region Special
    private void Special()
    {
        if (isSacrificialKill) { PiranhaPond(); }
        if(isSupportExpert) { SupportBuffing(); }
    }
    private void PiranhaPond() //Piranha ability
    {
        transform.gameObject.tag= "InstaKill"; //If the unit is Piranha Pond, then it will be an instaKill tag, the enemy will die with it on impact.
        //print("Is now instakill");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isMultiPurpose) //Switches between range and melee on contact
        {
            isShooter = false;
        }
        //print("Has had a collision");
        if (this.gameObject.tag == "InstaKill")
        {
            //print("Was insta kill");
            if (collision.gameObject.GetComponent<BasicEnemyMovement>() || collision.gameObject.GetComponent<GreedyOpportunity>())
            {
                //print("Got the kill inn");
                if (!hasInstakilled)
                {
                    HandDeath();
                    hasInstakilled = true;
                    Destroy(collision.gameObject);
                    Destroy(this.gameObject);

                    if (collision.gameObject.GetComponent<GreedyOpportunity>())
                    {
                        collision.gameObject.GetComponent<GreedyOpportunity>().isDead = true;
                    }
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isMultiPurpose) //Switches between range and melee on lack of contact
        {
            isShooter = true;
        }
    }
    private void AllyAim() //Special Aim for targeting allies specifically.
    {
        originForAllyAim = transform.position;
        directionForAllyAim = transform.right;
        originForAllyAim = originForAllyAim + directionForAllyAim; //Make sure there is an offset to prevent the support from supporting themselves.
    }
    private void SupportBuffing() //Supportive Cat ability
    {
        RaycastHit2D hitAlly; //Delegate memory

        hitAlly = Physics2D.Raycast(originForAllyAim, directionForAllyAim, hitAllyRange, allyLayerToTarget); //Ray to hit your ally
        Debug.DrawRay(originForAllyAim, directionForAllyAim * hitAllyRange, Color.yellow, 20); //Visual guide to ray

        if (hitAlly)
        {
            if (!hasBuffedAlly)
            {
                Debug.Log("Unit that is targeted buffed" + hitAlly.transform.name); //Confirm that the correct unit is hit.
                UnitPrototypeScript allyUnit = hitAlly.transform.gameObject.GetComponent<UnitPrototypeScript>(); //Get the script to affect it.
                allyUnit.transform.localScale += sizeBuff; //How much the unit increases in size
                CapsuleCollider2D HitboxReduction = allyUnit.GetComponent<CapsuleCollider2D>();
                HitboxReduction.size = new Vector2(HitboxReduction.size.x - 0.3f, HitboxReduction.size.y - 0.3f);
                //Fixes the issue where the hitbox became larger than the unit's attack range


                healthToBuff = allyUnit.health; //Get a refrence for the health
                damageProjectileToBuff = allyUnit.projectileDamage;//Get a refrence for the damage
                damagePunchToBuff = allyUnit.punchDamage; //Get a refrence for the damage

                DamageHealingBuff(); //Start the calculation program
                allyUnit.health = healthBuff;
                allyUnit.punchDamage =(int)damageBuff; //How much ounch damage increases
                allyUnit.projectileDamage = (int)damageBuff; //How much shoot damage increases

                if(allyUnit.name == "Buffcat")
                {
                    healthToBuff = allyUnit.health; //Get a refrence for the health
                    damageProjectileToBuff = allyUnit.projectileDamage;//Get a refrence for the damage
                    damagePunchToBuff = allyUnit.punchDamage; //Get a refrence for the damage

                    DamageHealingBuff(); //Start the calculation program
                    allyUnit.health = healthBuff;
                    allyUnit.punchDamage = (int)damageBuff; //How much ounch damage increases
                    allyUnit.projectileDamage = (int)damageBuff; //How much shoot damage increases
                }

                hasBuffedAlly = true; //Currently only 1 buff, more changes needs to be discussed.
            }
        }
    }
    private void DamageHealingBuff()
    {
        healthBuff = healthToBuff + (healthToBuff * healthBuff); //Increase the health by this much.

        damageProjectileToBuff = damageProjectileToBuff + (damageProjectileToBuff * damageBuff); //Increase the damage by this much.
        damagePunchToBuff *= damagePunchToBuff + (damagePunchToBuff * damageBuff); //Increase the damage by this much.
    }

    #endregion
    #region Take Damage
    public void TakeDamage(int damage, int damageTypeTaken)
    {
        if (DamageTypeCore.s_isUsingWeaknessStrenght) //Do this if it is being used.
        {

            if (damageTypeTaken == DamageTypeCore.s_HighestDamageTyping)
            {
                //print("A unit took damage of type " + damageTypeTaken);
                if (damageType == 0)
                {
                    health -= (damage + (damage / DamageTypeCore.s_DamageDivisionModule));
                    animatorOfUnit.SetTrigger("Vulnerable");
                }
                else if (damageType == (DamageTypeCore.s_HighestDamageTyping - 1))
                {
                    health -= (damage - (damage / DamageTypeCore.s_DamageDivisionModule));
                    animatorOfUnit.SetTrigger("Resistant");
                }
                else
                {
                    health -= damage; //If none of the above apply 
                }
            }
            else if (damageTypeTaken == 0)
            {
                if (damageType == 1)
                {
                    health -= (damage + (damage / DamageTypeCore.s_DamageDivisionModule));
                    animatorOfUnit.SetTrigger("Vulnerable");
                }
                else if (damageType == DamageTypeCore.s_HighestDamageTyping)
                {
                    health -= (damage - (damage / DamageTypeCore.s_DamageDivisionModule));
                    animatorOfUnit.SetTrigger("Resistant");
                }
                else //If none of the above apply
                {
                    health -= damage;
                }
            }
            if (damageType == (damageTypeTaken + 1))
            {
                health -= (damage + (damage / DamageTypeCore.s_DamageDivisionModule));
                animatorOfUnit.SetTrigger("Vulnerable");
            }
            else if (damageType == (damageTypeTaken - 1))
            {
                health -= (damage - (damage / DamageTypeCore.s_DamageDivisionModule));
                animatorOfUnit.SetTrigger("Resistant");
            }
            else
            {
                health -= damage;
            } 
        }
        else //If not using the Weakness strenght system
        {
            health -= damage;
        }
        //print("Unit health is " + health);
        if (health > 0)
        {
            StartCoroutine(PeriodOfBeingDamaged());
        }
    }

    IEnumerator PeriodOfBeingDamaged() //This entire thing can do whatever is put in here(Rotation is just a short representation.
    {
        Quaternion orgRot;
        orgRot = new Quaternion(0,0,0,0); //Retain the original rotational value

        transform.Rotate(0, 0, 10);
        yield return new WaitForSeconds(0.2f);
        transform.rotation = orgRot;
        yield return null;
    }

    private void Death()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            isDead = true;
            //print("Im die, thank you forever!");
        }
    }

    public void HandDeath()
    {
        isDead = true;
    }
    private void OnDestroy()
    {
        if (IsSaint)
        {
            GameObject.Find("PurificationZone").GetComponent<SanctuaryCode>().PurifierDied();
        }

        //Unit abilities:
        if (Unit.cardName == "Pigster")
        {
            MafiaBuffPig = false;
            MafiaCancelPig = true;
        }

        if (Unit.cardName == "Cowster")
        {
            MafiaBuffCow = false;
            MafiaCancelCow = true;
        }

        if (Unit.cardName == "Cheer Hamster")
        {
            GameObject Manager = GameObject.Find("MANAger");

            Manager.GetComponent<ManaSystem>().manaGainSpeed += 0.3f;
        }

        if(laneNumberUnit == 1) //In theory, this should remove the unit from the list upon death, preventing post mortem bonus.
        {
            LanePlacedUnits.s_lane1[lanePlacementUnit] = null;
        }
        if (laneNumberUnit == 2)
        {
            LanePlacedUnits.s_lane2[lanePlacementUnit] = null;
        }
        if (laneNumberUnit == 3)
        {
            LanePlacedUnits.s_lane3[lanePlacementUnit] = null;
        }
        if (laneNumberUnit == 4)
        {
            LanePlacedUnits.s_lane4[lanePlacementUnit] = null;
        }


        if (EnemySpawning.s_isCoreGame)
        {
            if (transform.parent && isDead)
            {
                transform.GetChild(1).gameObject.SetActive(true);

                transform.GetChild(1).gameObject.GetComponent<TowerSpotsScript>().enabled = true;
                transform.GetChild(1).gameObject.GetComponent<Image>().enabled = true;
                transform.GetChild(1).gameObject.GetComponent<Button>().enabled = true;

                transform.GetChild(1).gameObject.transform.SetParent(transform.parent, true);

            }
        }
    }

    private void CurrentDamageTaken() // This calculates the amount of damage into a percentage, and feeds this info into an animator.
    {
        float currentDamage;

        currentDamage = (health / healthSave)*100;
        animatorOfUnit.SetFloat("DamageTaken",currentDamage);
    }

    #endregion
    #region Spell Effects

    public void AttackBuff(float speedBuff, float damageBuff, float buffTime) // Get this to buff allies with spells
    {
        StartCoroutine(AttackBuffTime(speedBuff, damageBuff, buffTime));
    }
    IEnumerator AttackBuffTime(float speedStrength, float damageStrength,float waitTime)
    {
        shootSpeedSave = shootRechargeTime;
        punchSpeedSave = punchRechargeTime;

        shootDamageSave = projectileDamage;
        punchDamageSave = punchDamage;

        shootRechargeTime = shootRechargeTime - (shootRechargeTime * speedStrength);
        punchRechargeTime = punchRechargeTime - (punchRechargeTime * speedStrength);

        DamageMath(damageStrength);

        yield return new WaitForSeconds(waitTime);

        shootRechargeTime = shootSpeedSave;
        punchRechargeTime = punchSpeedSave;

        projectileDamage = shootDamageSave;
        punchDamage = punchDamageSave;
        yield return null;
    }
    private void DamageMath(float damageBonus)
    {
        float damageChange = projectileDamage;
        projectileDamage = (int)(damageChange + (damageChange*damageBonus));

        damageChange = punchDamage;
        punchDamage = (int)(damageChange + (damageChange * damageBonus));
    }

    public void HealthBuff(float healthBuff) // Get this to buff allies health with spells
    {
        health = health + (healthSave * healthBuff); //Currently buffs health in comparison to current health %
        healthSave = healthSave + (healthSave * healthBuff); //Updates what the max health is, potentially remove this. Makes sure that damage visuals are up to date with the new health max in %.
    }
    #endregion
    #region Lane Placement
    public void DefineUnitPlacement(int laneNumber, int LanePlacement) //This does nothing else than letting the unit know which lane and spot it is in for when it dies.
    {
        laneNumberUnit = laneNumber;
        lanePlacementUnit = LanePlacement;
    }
    #endregion
    private void UnitInfoFeed()
    {
        //Base
        health = Unit.health;
        //Attack Options
        isShooter = Unit.isShooter;
        isPunching = Unit.isPunching;
        isSpecial = Unit.isSpecial;
        damageType = Unit.damageType;
        //Shooting
        shootRechargeTime = Unit.shootRechargeTime;
        projectileSpeed = Unit.projectileSpeed;
        projectilePrefab = Unit.projectilePrefab;
        projectileDamage = Unit.projectileDamage;
        shootingTargetLayer = Unit.shootingTargetLayer;
        edgeOfRangeLayer = Unit.edgeOfRangeLayer;
        targetsToShoot = Unit.targetsToShoot;
        //Punching
        punchRechargeTime = Unit.punchRechargeTime;
        punchDamage = Unit.punchDamage;
        punchingTargetLayer = Unit.punchingTargetLayer;
        targetsToPunch = Unit.targetsToPunch;
        canPunchEverything = Unit.canPunchEverything;
        hasKnockback = Unit.hasKnockback;
        knockbackPower = Unit.knockbackPower;
        //Special
        isSacrificialKill = Unit.isSacrificialKill;
        isSupportExpert = Unit.isSupportExpert;

        allyLayerToTarget = Unit.allyLayerToTarget;
        hitAllyRange = Unit.hitAllyRange;
        sizeBuff = Unit.sizeBuff;
        healthBuff = Unit.healthBuff;
        damageBuff = Unit.damageBuff;

        unitIndex = Unit.unitIndex;
    }

    private void SaintEffects()
    {
        //Cant attack while purifying
        if (isPunching)
        {
            WasFighter = true;
            isPunching = false;
        }
        if (isShooter)
        {
            WasShooter = true;
            isShooter = false;
        }

        //Add other visual effects like a slight glow maybe

        GameObject.Find("PurificationZone(Clone)").GetComponent<SanctuaryCode>().SaintLives = true;
        GameObject.Find("PurificationZone(Clone)").GetComponent<SanctuaryCode>().CurrentSaint = this;
    }

    public void UnSanctifyUnit()
    {
        if (WasShooter)
            isShooter = true;
        if (WasFighter)
            isPunching = true;

        IsSaint = false;
    }

    private void OnHitAbility()
    {
        if (Unit.cardName == "The Chef")//The chef rat ability
        {
            hitEnemy.GetComponent<BasicEnemyMovement>().RatDebuff();
        }
        
        if (Unit.cardName == "Goosey")
        {
            HitCounter++;

            if(HitCounter == 4)
            {
                punchDamage = punchDamage * 3;
            }
            if(HitCounter == 5)
            {
                punchDamage = punchDamage / 3;
                HitCounter = 0;
            }

            Debug.Log("The hitcounter is" + HitCounter);
            Debug.Log("My damage is " + punchDamage);
        }

        if (Unit.cardName == "Karl")
        {
            if (hitEnemy.GetComponent<BasicEnemyMovement>().enemyHealth >= 0)
            {
                OneTimeTrigger = true;//Dont update my basic damage

                punchDamage += 8;

                Debug.Log("Enemy got hit and lived, my damage is now " + punchDamage);
            }
            else
            {
                punchDamage = BasicDamage;

                OneTimeTrigger = false;//No more targets in range

                Debug.Log("Enemy got hit and i died, my damage is now " + punchDamage);
            }
        }

        if (Unit.cardName == "Commando Owl")
        {
            punchRechargeTime = punchRechargeTime * 0.9f;//This needs a nerf

            Debug.Log("My attack speed is now " + punchRechargeTime);
        }

        if (Unit.cardName == "King Kobra")
        {
            hitEnemy.GetComponent<BasicEnemyMovement>().KobraPoison();
        }

        if (Unit.cardName == "Sniper Monkey")
        {
            hitEnemy.GetComponent<BasicEnemyMovement>().MonkePoison();
        }

        if (Unit.cardName == "Snek")
        {
            hitEnemy.GetComponent<BasicEnemyMovement>().SnekPoison();
        }

        if(Unit.cardName == "Frogger")
        {
            punchDamage -= BonusDamageInt;
            BasicDamage = punchDamage;//Makes sure i dont lose buffs
            countdown = 0;
            CHARRGE = false;
        }
    }

    private void mafiaAbility()//"Pigster" + "Cowster" ability
    {
        if (OneTimeTrigger == false)
        {
            health = health * 1.5f;
            Debug.Log("My health changed from " + health / 1.5f + "to " + health);

            float damageFloat;
            damageFloat = punchDamage;
            damageFloat = Mathf.Round(damageFloat * 1.5f);
            punchDamage = (int)damageFloat;
            Debug.Log("My damage changed from " + punchDamage / 1.5f + "to " + punchDamage);

            float RangeddamageFloat;
            RangeddamageFloat = projectileDamage;
            RangeddamageFloat = Mathf.Round(RangeddamageFloat * 1.5f);
            projectileDamage = (int)RangeddamageFloat;
            Debug.Log("My damage changed from " + projectileDamage / 1.5f + "to " + projectileDamage);

            OneTimeTrigger = true;//dont trigger this again
        }
    }
    private void ability_Croc()//"Black Rooster" ability
    {
        if (health > BasicHealth)//This prevents his damage to be decrease when recieving a health buff
        {
            BasicHealth = health;
        }

        float damageFloat;
        int damageBuff = BasicDamage;
        damageFloat = damageBuff;
        damageFloat = Mathf.Round(damageFloat / 1.5f);
        damageBuff = (int)damageFloat;


        float HealthPercentage;//Calculate current health %
        HealthPercentage = (health / BasicHealth);

        float floatdamage;//float -> int workaround
        floatdamage = BasicDamage - (damageBuff * HealthPercentage);//Increase damage when health is lowered

        punchDamage = (int)floatdamage;

        Debug.Log("Im at " + HealthPercentage * 100 + "% of my health");
        Debug.Log("My damage is " + punchDamage);
    }

    private void ability_Ninjey()
    {
        GetComponent<Collider2D>().enabled = false;
        OneTimeTrigger = true;

        BasicHealth = health;
    }
}
