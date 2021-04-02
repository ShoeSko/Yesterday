using UnityEngine;
[CreateAssetMenu(fileName = "New Unit", menuName = "UnitTemplate")]
public class UnitScript : ScriptableObject
{
    [Header("Unit Controls")]
    [Range(0, 100)] public float health;

    [Header("Unit Attack")]
    [Tooltip("Is this unit going to shoot?")] public bool isShooter;
    [Tooltip("Is this unit going to punch?")] public bool isCQC;
    [Tooltip("Is this Unit special in some way?")] public bool isSpecial;
    
    [Header("Shooting")]
    [Tooltip("Time between each shot")] [Range(0, 100)] public float shootRechargeTime;
    [Tooltip("The speed of the projectile being fired")] [Range(0, 100)] public float projectileSpeed;
    [Tooltip("Prefab of the projectile to be shot")] public GameObject projectilePrefab;
    [Tooltip("The damage the projectile will deal")] [Range(0, 100)] public int projectileDamage;
    [Tooltip("What layer are enemies on?")]public LayerMask shootingTargetLayer;
    [Tooltip("What layer is the range wall on?")]public LayerMask edgeOfRangeLayer;

    [Header("Punching")]
    [Tooltip("Time between each punch")] [Range(0, 100)] public float punchRechargeTime;
    [Tooltip("The damage the punch will deal")] [Range(0, 100)] public int punchDamage;
    [Tooltip("What layer are enemies on?")] public LayerMask punchingTargetLayer;

}
