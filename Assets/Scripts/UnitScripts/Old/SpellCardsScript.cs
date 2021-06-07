using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCardsScript : MonoBehaviour
{
    public CardSpellScript spell; // Scripableobject template for all spells

    [Header("Spell basic")]
    [Tooltip("Can this spell affect allies?")] private bool targetFriendly;
    [Tooltip("What is a friendly layer?")] private LayerMask friendlyLayer;
    [Tooltip("Can this spell affect enemies?")] private bool targetEnemy;
    [Tooltip("What is a hostile layer?")] private LayerMask enemyLayer;

    [Header("Negative Effects")]
    [Tooltip("Can this spell slow enemies?")] private bool canSlow; //Slows enemies
    [Tooltip("How powerful is the slow?")] [Range(0, 1)] private float slowDebuffStrength; //Strength in %
    [Tooltip("Can this spell stun enemies?")] private bool canStun; //Stops movement and attack
    [Tooltip("Can this spell pacify an enemy")] private bool canPacify; //Stops attack not movement
    [Tooltip("Can this spell root an enemy")] private bool canRoot; //Stops movement not attack
    [Tooltip("How long will this debuff last?")] private float debuffTime; // Period the debuf takes place
    [Tooltip("Can this spell harm/kill enemies?")] private bool canHarm; // Harm with enough damage will also be a kill effect.
    [Tooltip("How powerfull is the harm effect?")] private int harmStrength; // Strength in numbers


    [Header("Positive Effects")]
    [Tooltip("Can this spell buff ally attack speed or damage?")] private bool canBuffAttack; // Can it buff speed / Damage?
    [Tooltip("How much attack speed is buffed?")] private float attackSpeedBuffStrength; // Strenght in %
    [Tooltip("How much attack damage is buffed?")] private float attackDamageBuffStrength; // Strenght in %
    [Tooltip("How long will this buff last?")] private float buffTime; // Period the buff takes place

    [Tooltip("Can this spell buff ally health?")] private bool canBuffHealth; // Permanent Health Buff
    [Tooltip("How much health is buffed?")] private float healthBuffStrength; //Strenght in %?

    //[Header("Can this spell harm everything?")] public bool canRagnarok; //Wait a bit

    private bool hasCastSpell; //Has the spell been cast?

    private void Start()
    {
        SpellInfoFeed(); //Updates all the stats of the spell.
    }

    private void Update()
    {
        LocateTargetLocation();
        SearchForTarget();
    }

    private void SearchForTarget()
    {
        if (targetEnemy) { ActivateEnemySpellEffect(); }
        else if (targetFriendly) { ActivateFriendlySpellEffect(); }
    }

    private void ActivateEnemySpellEffect()
    {
        if (canSlow && !hasCastSpell)
        {
            GameObject[] EnemiesToSlow = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < EnemiesToSlow.Length; i++)
            {
                BasicEnemyMovement enemy = EnemiesToSlow[i].GetComponent<BasicEnemyMovement>();
                GreedyOpportunity handEnemy = EnemiesToSlow[i].GetComponent<GreedyOpportunity>();
                if (EnemiesToSlow[i] == enemy)
                {
                enemy.Slow(slowDebuffStrength, debuffTime); // This will be changed, tag. find ?
                print("Slowed");
                }
                else if (EnemiesToSlow[i] == handEnemy)
                {
                    handEnemy.Slow(slowDebuffStrength, debuffTime);
                }
                hasCastSpell = true;
            }
            TerminateSpellExistance();
        }

        Collider2D[] enemiesToEnchant = Physics2D.OverlapCircleAll(transform.position, 0.5f, enemyLayer);
        if (enemiesToEnchant.Length > 0 && !hasCastSpell)
        {
            hasCastSpell = true;
            if (enemiesToEnchant[0])
            {
                BasicEnemyMovement enemy = enemiesToEnchant[0].GetComponent<BasicEnemyMovement>();
                GreedyOpportunity handEnemy = enemiesToEnchant[0].GetComponent<GreedyOpportunity>();
                if (canStun)
                {
                    if(enemiesToEnchant[0] == enemy)
                    {
                    enemy.Stun(debuffTime);
                    print("Stunned");
                    }
                    else if (enemiesToEnchant[0] == handEnemy)
                    {
                        handEnemy.Stun(debuffTime);
                    }
                }
                else if (canPacify)
                {
                    if (enemiesToEnchant[0] == enemy)
                    {
                    enemy.Pacify(debuffTime);
                    print("Disarmed");
                    }
                    else if (enemiesToEnchant[0] == handEnemy)
                    {
                        handEnemy.Pacify(debuffTime);
                    }
                }
                else if (canRoot)
                {
                    if (enemiesToEnchant[0] == enemy)
                    {
                    enemy.Root(debuffTime);
                    print("Rooted");
                    }
                    else if (enemiesToEnchant[0] == handEnemy)
                    {
                        handEnemy.Root(debuffTime);
                    }
                }
                else if (canHarm)
                {
                    if (enemiesToEnchant[0] == enemy)
                    {
                    enemy.Harm(harmStrength);
                    print("Harmed");
                    }
                    else if (enemiesToEnchant[0] == handEnemy)
                    {
                        handEnemy.Harm(harmStrength);
                    }
                }
                TerminateSpellExistance();
            }
        }
    }

    private void ActivateFriendlySpellEffect()
    {
        Collider2D[] alliesToEnchant = Physics2D.OverlapCircleAll(transform.position, 0.5f, friendlyLayer);
        if (alliesToEnchant.Length > 0 && !hasCastSpell)
        {
            hasCastSpell = true;
            if (alliesToEnchant[0])
            {
                UnitPrototypeScript Unit = alliesToEnchant[0].GetComponent<UnitPrototypeScript>();
                if (canBuffAttack)
                {
                    Unit.AttackBuff(attackSpeedBuffStrength, attackDamageBuffStrength, buffTime); // Buffs damage /& speed
                    print("Attack Buffed");
                }
                else if (canBuffHealth)
                {
                    Unit.HealthBuff(healthBuffStrength); // Send message to buff the health of first target.
                    print("Health Buffed");
                    hasCastSpell = true;
                }
                TerminateSpellExistance();
            }
        }
    }
    private void TerminateSpellExistance()
    {
        if (hasCastSpell)
        {
            Destroy(gameObject);
        }
    }

    private void LocateTargetLocation()
    {
        Vector3 mousePos;

        Camera mainCamera = Camera.main;
#if UNITY_ANDROID //Everything within this, only works if the build is android.
        if (Input.touchCount > 0)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position); //This needs APK testing.
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
        else { transform.position = new Vector3(-50, -15, 0); } //Prevents any colision whilst you are not targeting anything.

#else
        if (Input.GetMouseButton(0))
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(mousePos.x,mousePos.y,0);
        }
        else { transform.position = new Vector3(-50, -15, 0); } //Prevents any colision whilst you are not targeting anything.
#endif
    }


    private void OnDrawGizmosSelected() //Gizmo to represent spell effect range
    {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
    private void SpellInfoFeed()
    {
        // Spell Basic
        targetFriendly = spell.targetFriendly;
        friendlyLayer = spell.friendlyLayer;
        targetEnemy = spell.targetEnemy;
        enemyLayer = spell.enemyLayer;
        // Negative Effects
        canSlow = spell.canSlow;
        slowDebuffStrength = spell.slowDebuffStrength;
        canStun = spell.canStun;
        canPacify = spell.canPacify;
        canRoot = spell.canRoot;
        debuffTime = spell.debuffTime;
        canHarm = spell.canHarm;
        harmStrength = spell.harmStrength;
        // Positive Effects
        canBuffAttack = spell.canBuffAttack;
        attackSpeedBuffStrength = spell.attackSpeedBuffStrength;
        attackDamageBuffStrength = spell.attackDamageBuffStrength;
        buffTime = spell.buffTime;
        canBuffHealth = spell.canBuffHealth;
        healthBuffStrength = spell.healthBuffStrength;

    }
}
