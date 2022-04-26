using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy", menuName = "EnemyTemplate")]
public class EnemyScript : ScriptableObject
{
    [Header("Enemy Controls")]
    [Range(0, 100)] public float moveSpeed = 5;
    [Range(0, 1000)] public int enemyHealth = 100;
    [Tooltip("The tag corresponding to what the enemy will care about")] public string obstacleTags;
    [Tooltip("The tag coresponding to projectiles")] public string projectileTags;

    [Header("Enemy Attack")]
    [Tooltip("The damage they deal")] [Range(0, 100)] public int attackDamage;
    [Tooltip("The rate of attacks")] [Range(0, 100)] public float attackSpeed;
    [Tooltip("What layer is to be attacked?")] public LayerMask whatIsUnitLayer = 10;
    [Tooltip("The type of damage done/taken")] public int damageType;

    [Header("Special Abilities")]
    [Tooltip("Will natures wrath spawn upon enemy death?")] public bool canUseNaturesWrath;
    [Tooltip("The Nature's Wrath prefab is needed containg script with similair name")] public GameObject naturesWrathObject;

    [Header("Enemy confirmation for Animation")]
    [Tooltip("Is the Enemy Merry, so that her animation will play")] public bool[] specialAnimationCheckList;

    [Header("Enemy Index")]
    //[Tooltip("What type of enemy is this?")]public bestiaryOptions bestiaryType; 
    //public enum bestiaryOptions { Beast, Humanoid, Monstrosity }
    [Tooltip("Is it a beast?")] public bool isBeast; 
    [Tooltip("Is it a humanoid?")] public bool isHumanoid;
    [Tooltip("Is it a monstrosity?")] public bool isMonstrosity;
    [Tooltip("What index number does it have? Please start with 0.")] public int enemyIndex;
}
