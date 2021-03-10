using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpotsScript : MonoBehaviour
{
    public GameObject cowwithgunPrefab;//temporary solution: the unit that's supposed to be summoned
    private Vector2 buttonPos;
    public GameObject towerSpots;
    public Transform positionResetTransform;
    public void Start()
    {
        buttonPos = transform.position;
    }
    public void PlaceCard()
    {
        ManaSystem.CurrentMana -= 2;//temporary solution: how much mana the unit costs
        GameObject unit =Instantiate(cowwithgunPrefab, buttonPos, transform.rotation);//spawn the correct unit
        unit.transform.SetParent(positionResetTransform, true);
        this.transform.SetParent(unit.transform,true);
        towerSpots.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (transform.parent == positionResetTransform)
        {
            transform.SetParent(towerSpots.transform, true);
        }
    }
}