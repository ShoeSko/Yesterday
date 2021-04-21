using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCardsScript : MonoBehaviour
{
    public CardSpellScript spell; // Scripableobject template for all spells

    [Header("Spell basic")]
    [Tooltip("Can this spell affect allies?")] private bool targetFriendly;
    [Tooltip("Can this spell affect enemies?")] private bool targetEnemy;

    [Header("Negative Effects")]
    [Tooltip("Can this spell slow enemies?")] private bool canSlow; //Slows enemies
    [Tooltip("How powerful is the slow?")][Range(0,1)] private float slowDebuffStrength; //Strength in %
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

    private void Start()
    {
        SpellInfoFeed(); //Updates all the stats of the spell.
    }







    private void SpellInfoFeed()
    {
        // Spell Basic
        targetFriendly = spell.targetFriendly;
        targetEnemy = spell.targetEnemy;
        // Negative Effects
        canSlow = spell.canSlow;
        slowDebuffStrength = spell.slowDebuffStrength;
        canStun = spell.canStun;
        canPacify = spell.canPacify;
        canRoot = spell.canRoot;
        debuffTime = spell.debuffTime;
        canHarm = spell.canHarm;
        // Positive Effects
        canBuffAttack = spell.canBuffAttack;
        attackSpeedBuffStrength = spell.attackSpeedBuffStrength;
        attackDamageBuffStrength = spell.attackDamageBuffStrength;
        buffTime = spell.buffTime;
        canBuffHealth = spell.canBuffHealth;
        healthBuffStrength = spell.healthBuffStrength;

    }
}
