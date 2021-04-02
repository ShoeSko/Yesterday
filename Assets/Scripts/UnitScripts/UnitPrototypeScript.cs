using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrototypeScript : MonoBehaviour
{
    public CardScript Unit; //Scriptableobject template for all Units.
    [Header("Unit Controls")]
    [Range(0, 100)] private float health;

    [Header("Unit Attack")]
    [Tooltip("Is this unit going to shoot?")] private bool isShooter;
    [Tooltip("Is this unit going to punch?")] private bool isCQC;
    [Tooltip("Is this Unit special in some way?")] private bool isSpecial;

    [Header("Shooting")]
    [Tooltip("Time between each shot")] [Range(0, 100)] private float shootRechargeTime;
    [Tooltip("The speed of the projectile being fired")] [Range(0, 100)] private float projectileSpeed;
    [Tooltip("Prefab of the projectile to be shot")] private GameObject projectilePrefab;
    [Tooltip("The damage the projectile will deal")] [Range(0, 100)] private int projectileDamage;
    [Tooltip("What layer are enemies on?")] private LayerMask shootingTargetLayer;
    [Tooltip("What layer is the range wall on?")] private LayerMask edgeOfRangeLayer;

    private bool isRecharging; //Is the unit recharging
    private Vector3 originForAim; //Where does the unit aim from?
    private Vector3 directionForAim; //In which direction does the unit aim?
    private float hitRange; //How far can the unit shoot?

    [Header("Punching")]
    [Tooltip("Time between each punch")] [Range(0, 100)] private float punchRechargeTime;
    [Tooltip("The damage the punch will deal")] [Range(0, 100)] private int punchDamage;
    [Tooltip("What layer are enemies on?")] private LayerMask punchingTargetLayer;




    private void Start()
    {
        UnitInfoFeed();
        Aim();
    }

    private void Update()
    {
        if (isShooter) { ShootProjectile(); }
        else if (isCQC) { print("Punch"); }

        Death();
    }

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
    private void punch()
    {

    }


    #endregion




    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void Death()
    {
        if(health <= 0 && !Unit.isNotInCorrectSceneTest) 
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.transform.SetParent(transform.parent, true);
            Destroy(gameObject);
        }
        else if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void UnitInfoFeed()
    {
        //Base
        health = Unit.health;
        //Attack Options
        isShooter = Unit.isShooter;
        isCQC = Unit.isCQC;
        isSpecial = Unit.isSpecial;
        //Shooting
        shootRechargeTime = Unit.shootRechargeTime;
        projectileSpeed = Unit.projectileSpeed;
        projectilePrefab = Unit.projectilePrefab;
        projectileDamage = Unit.projectileDamage;
        shootingTargetLayer = Unit.shootingTargetLayer;
        edgeOfRangeLayer = Unit.edgeOfRangeLayer;
        //Punching
        punchRechargeTime = Unit.punchRechargeTime;
        punchDamage = Unit.punchDamage;
        punchingTargetLayer = Unit.punchingTargetLayer;

    }
}
