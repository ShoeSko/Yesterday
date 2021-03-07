using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpotsScript : MonoBehaviour
{
    public GameObject cowwithgunPrefab;//temporary solution: the unit that's supposed to be summoned
    private Vector2 buttonPos;
    public GameObject towerSpots;

    public void Start()
    {
        buttonPos = transform.position;
    }
    public void PlaceCard()
    {
        ManaSystem.CurrentMana -= 2;//temporary solution: how much mana the unit costs
        Instantiate(cowwithgunPrefab, buttonPos, transform.rotation);//spawn the correct unit
        towerSpots.SetActive(false);
        gameObject.SetActive(false);
    }
}