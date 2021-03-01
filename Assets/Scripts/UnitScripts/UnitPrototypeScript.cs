using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrototypeScript : MonoBehaviour
{
    /*
     HP for destruction
    
    A shoot function
        Bool if it is a shooter or not
        public prefab placement for customization
        Fire rate
        Fire speed
        Fire direction
        Damage.. on bullet perhaps
        
    Physical harm
        Bool if physical harmer or not
        attack speed
        Attack damage, if applicable
        
      
     
     */

    [Header("Shooting")]
    [Tooltip("Is this unit going to shoot?")]public bool isShooter;
    private bool isRecharging;
    [Tooltip("Time between each shot")]public float shootRechargeTime;
    public float projectileSpeed;
    [Tooltip("Prefab of the projectile to be shot")]public GameObject projectilePrefab; //Find a way to make this not mandatory...


    [Header("Hitting")]
    private bool isCQC;




    private void ShootProjectile()
    {
        bool temptForIf = true;//To be removed when shoot condition is stable.
        //Raycast to trigger shooting?
        if (isShooter)
        {
            if (!isRecharging)
            {

                isRecharging = true;
                StartCoroutine(ShootRecharge());
            }
        }
    }



    IEnumerator ShootRecharge()
    {
        yield return new WaitForSeconds(shootRechargeTime);
        isRecharging = false;
        Debug.Log("Done Recharging");
        yield return null;
    }
}
