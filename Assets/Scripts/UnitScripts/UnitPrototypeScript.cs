using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrototypeScript : MonoBehaviour
{
    [Header("Base Stats")]
    [Range(0,100)]public float health;

    [Header("Shooting")]
    [Tooltip("Is this unit going to shoot?")][SerializeField]private bool isShooter;
    [Tooltip("Time between each shot")][Range(0,100)][SerializeField] private float shootRechargeTime;
    [Tooltip("The speed of the projectile being fired")][Range(0,100)][SerializeField]private float projectileSpeed;
    [Tooltip("Prefab of the projectile to be shot")][SerializeField] private GameObject projectilePrefab; //Find a way to make this not mandatory? Or make 2 scripts, one range & one melee.
    [Tooltip("The damage the projectile will deal")][Range(0,100)] public int projectileDamage;
    [SerializeField]private LayerMask targetLayer;
    [SerializeField]private LayerMask edgeOfRangeLayer;


    private bool isRecharging;
    private Vector3 originForAim;
    private Vector3 directionForAim;
    private float hitRange;

    [Header("Hitting")]
    private bool isCQC;

    private void Start()
    {
        Aim();
    }

    private void Update()
    {
        ShootProjectile();
        Death();
    }


    private void ShootProjectile()
    {
        if (isShooter)
        {
            RaycastHit2D hit; //Delegate memory

            hit = Physics2D.Raycast(originForAim, directionForAim, hitRange, targetLayer);
            Debug.DrawRay(originForAim, directionForAim * hitRange, Color.red,10);
            if (hit)
            {
                //Debug.Log(hit.collider.name);//What is hit
                //Debug.DrawLine(originForAim, hit.transform.position, Color.blue); //Line to target
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
        Debug.Log(hitRange);
    }

    IEnumerator ShootRecharge()
    {
        yield return new WaitForSeconds(shootRechargeTime);
        isRecharging = false;
        Debug.Log("Done Recharging");
        yield return null;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void Death()
    {
        if(health <= 0) 
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.transform.SetParent(transform.parent, true);
            Destroy(gameObject);
        }
    }
}
