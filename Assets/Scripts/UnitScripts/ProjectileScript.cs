using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [HideInInspector]public int projectileDamage;

    private void Start()
    {
        UnitPrototypeScript parentScript = gameObject.GetComponentInParent<UnitPrototypeScript>();
        projectileDamage = parentScript.Unit.projectileDamage; //What damage does my parent want me to do?
    }
}
