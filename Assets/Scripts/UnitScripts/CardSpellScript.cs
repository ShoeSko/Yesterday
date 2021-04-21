using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New SpellCard", menuName = "Spell Card")]
public class CardSpellScript : ScriptableObject
{

    public string cardName;

    public Sprite image;
    public int manaCost;
    public string effectDescription;
    public string flavorTxt;
    public GameObject Prefab; // Need a representative for the spell cards in pick options.

    [Header("Spell basic")]
    [Tooltip("Can this spell affect allies?")] public bool targetFriendly;
    [Tooltip("Can this spell affect enemies?")] public bool targetEnemy;

    [Header("Negative Effects")]
    [Tooltip("Can this spell slow enemies?")] public bool canSlow; //Slows enemies
    [Tooltip("How powerful is the slow?")][Range(0,1)] public float slowDebuffStrength; //Strength in %
    [Tooltip("Can this spell stun enemies?")] public bool canStun; //Stops movement and attack
    [Tooltip("Can this spell pacify an enemy")] public bool canPacify; //Stops attack not movement
    [Tooltip("Can this spell root an enemy")] public bool canRoot; //Stops movement not attack
    [Tooltip("How long will this debuff last?")] public float debuffTime; // Period the debuf takes place
    [Tooltip("Can this spell harm/kill enemies?")] public bool canHarm; // Harm with enough damage will also be a kill effect.


    [Header("Positive Effects")]
    [Tooltip("Can this spell buff ally attack speed or damage?")] public bool canBuffAttack; // Can it buff speed / Damage?
    [Tooltip("How much attack speed is buffed?")] public float attackSpeedBuffStrength; // Strenght in %
    [Tooltip("How much attack damage is buffed?")] public float attackDamageBuffStrength; // Strenght in %
    [Tooltip("How long will this buff last?")] public float buffTime; // Period the buff takes place

    [Tooltip("Can this spell buff ally health?")] public bool canBuffHealth; // Permanent Health Buff
    [Tooltip("How much health is buffed?")] public float healthBuffStrength; //Strenght in %?

    //[Header("Can this spell harm everything?")] public bool canRagnarok; //Wait a bit

}
