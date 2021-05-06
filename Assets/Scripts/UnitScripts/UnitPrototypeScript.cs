using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private bool hasBuffedAlly = false; //Has the unit given it's buff?

    private float healthToBuff;
    private float damageProjectileToBuff;
    private float damagePunchToBuff;
    private Vector3 originForAllyAim; //Where does the unit aim from?
    private Vector3 directionForAllyAim; //In which direction does the unit aim?

    [Header("Spell effects")]
    private int shootDamageSave; // Saves the shoot Damage
    private float shootSpeedSave; // Saves the shoot speed
    private int punchDamageSave; // Saves the Punch Damage
    private float punchSpeedSave; // Saves the Punch speed
    private float healthSave; //Saves the health;

    [Header("Unit Damage Taken Visuals")]
    private Animator animatorOfUnit;

    #endregion
    #region Standard Voids
    private void Start()
    {
        UnitInfoFeed(); //All info of the Unit is recorded here.
        if (isShooter) { Aim(); }
        if (isSupportExpert) { AllyAim(); }
        healthSave = health; // Saves the health value at max.

        if(EnemySpawning.s_isCoreGame == true) //Makes sure the animation does not occur in the card selection
        {
            animatorOfUnit = GetComponent<Animator>();
            animatorOfUnit.enabled = true;
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
                        enemiesToDamage[i].GetComponent<BasicEnemyMovement>().TakeDamage(punchDamage, hasKnockback, knockbackPower); //Sent attackDamage to Unit
                        //print("Punch");
                        }
                        else if (enemiesToDamage[i].GetComponent<GreedyOpportunity>())
                        {
                            enemiesToDamage[i].GetComponent<GreedyOpportunity>().TakeDamage(punchDamage, hasKnockback, knockbackPower); //Sent attackDamage to Unit
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
                        enemiesToDamage[i].GetComponent<BasicEnemyMovement>().TakeDamage(punchDamage, hasKnockback,knockbackPower); //Sent attackDamage to Unit
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

                healthToBuff = allyUnit.health; //Get a refrence for the health
                damageProjectileToBuff = allyUnit.projectileDamage;//Get a refrence for the damage
                damagePunchToBuff = allyUnit.punchDamage; //Get a refrence for the damage

                DamageHealingBuff(); //Start the calculation program
                allyUnit.health = healthBuff;
                allyUnit.punchDamage =(int)damageBuff; //How much ounch damage increases
                allyUnit.projectileDamage = (int)damageBuff; //How much shoot damage increases

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
    public void TakeDamage(int damage)
    {
        health -= damage;
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
            print("Im die, thank you forever!");
        }
    }
    private void OnDestroy()
    {
        if (EnemySpawning.s_isCoreGame)
        {
            transform.GetChild(0).gameObject.SetActive(true);

            transform.GetChild(0).gameObject.GetComponent<TowerSpotsScript>().enabled = true;
            transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
            transform.GetChild(0).gameObject.GetComponent<Button>().enabled = true;

            transform.GetChild(0).gameObject.transform.SetParent(transform.parent, true);
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

    private void UnitInfoFeed()
    {
        //Base
        health = Unit.health;
        //Attack Options
        isShooter = Unit.isShooter;
        isPunching = Unit.isPunching;
        isSpecial = Unit.isSpecial;
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
    }
}
