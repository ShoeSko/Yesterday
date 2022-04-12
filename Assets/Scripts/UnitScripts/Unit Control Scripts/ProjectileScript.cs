using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [HideInInspector] public int projectileDamage;
    [HideInInspector] public int numberOfMaxTargets;
    [HideInInspector] public int damageTypeFromParent; //What damage do I do father?

    private void Start()
    {
        UnitPrototypeScript parentScript = gameObject.GetComponentInParent<UnitPrototypeScript>();
        projectileDamage = parentScript.projectileDamage; //What damage does my parent want me to do?
        numberOfMaxTargets = parentScript.targetsToShoot; //How many will the bullet hit before going away?
        damageTypeFromParent = parentScript.damageType; //What kind of damage do I do?
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
