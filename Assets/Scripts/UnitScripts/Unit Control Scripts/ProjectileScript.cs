using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [HideInInspector]public int projectileDamage;
    [HideInInspector] public int numberOfMaxTargets;

    private void Start()
    {
        UnitPrototypeScript parentScript = gameObject.GetComponentInParent<UnitPrototypeScript>();
        projectileDamage = parentScript.projectileDamage; //What damage does my parent want me to do?
        numberOfMaxTargets = parentScript.targetsToShoot; //How many will the bullet hit before going away?
    }

    private void Update()
    {
        if(numberOfMaxTargets <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);//Destroys bullet on leaving the camera.
    }
}
