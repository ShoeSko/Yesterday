using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTypeCore : MonoBehaviour
{
    [SerializeField] private int highestDamageTyping = 2; //the highest value a damage type can be given.
    static public int s_HighestDamageTyping = 2; //the highest value a damage type can be given.

    [SerializeField] private int damageDivisionModule = 4; //What the damage increase/Decrease is divided by.
    static public int s_DamageDivisionModule = 4; //What the damage increase/Decrease is divided by.

    [SerializeField] private bool isUsingWeaknessStrenght;
    static public bool s_isUsingWeaknessStrenght;
    
    private void Awake() //This just makes the static variables coincide with changes in inspector.
    {
        s_HighestDamageTyping = highestDamageTyping;
        s_DamageDivisionModule = damageDivisionModule;
        s_isUsingWeaknessStrenght = isUsingWeaknessStrenght; //Makes sure that all other scripts knows if it is being used.
    }
}
