using UnityEngine;
[CreateAssetMenu (fileName = "New Enemy", menuName ="EnemyTemplate")]
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

    [Header("Enemy confirmation for Animation")]
    [Tooltip("Is the Enemy Merry, so that her animation will play")] public bool isMerry;
}
