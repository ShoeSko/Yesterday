using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName = "Card")]
public class CardScript : ScriptableObject
{
    public string cardName;

    public Sprite image;
    public int manaCost;
    public string effectDescription;
    public string flavorTxt;
    public GameObject Prefab;

    [Header("Unit Controls")]
    [Range(0, 500)] public float health;

    [Header("Unit Attack")]
    [Tooltip("Is this unit going to shoot?")] public bool isShooter;
    [Tooltip("Is this unit going to punch?")] public bool isPunching;
    [Tooltip("Is this Unit special in some way?")] public bool isSpecial;

    [Header("Shooting")]
    [Tooltip("Time between each shot")] [Range(0, 100)] public float shootRechargeTime;
    [Tooltip("The speed of the projectile being fired")] [Range(0, 100)] public float projectileSpeed;
    [Tooltip("Prefab of the projectile to be shot")] public GameObject projectilePrefab;
    [Tooltip("The damage the projectile will deal")] [Range(0, 100)] public int projectileDamage;
    [Tooltip("What layer are enemies on?")] public LayerMask shootingTargetLayer;
    [Tooltip("What layer is the range wall on?")] public LayerMask edgeOfRangeLayer;
    [Tooltip("How many targets will be hit?")] [Range(0, 10)] public int targetsToShoot;

    [Header("Punching")]
    [Tooltip("Time between each punch")] [Range(0, 100)] public float punchRechargeTime;
    [Tooltip("The damage the punch will deal")] [Range(0, 100)] public int punchDamage;
    [Tooltip("What layer are enemies on?")] public LayerMask punchingTargetLayer;
    [Tooltip("How many targets will be hit?")] [Range(0, 10)] public int targetsToPunch;
    [Tooltip("Can it hit all targets?")] public bool canPunchEverything;

    [Header("Special")]
    [Tooltip("Does the unit die and kill the enemy on contact?")] public bool isSacrificialKill;

    [Header("Test options, must be off to work normaly")]
    public bool isNotInCorrectSceneTest; //Will be removed
}