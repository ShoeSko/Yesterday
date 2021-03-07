using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpotsScript : MonoBehaviour
{
    public GameObject cowwithgunPrefab;
    private Vector2 buttonPos;
    public GameObject towerSpots;

    public void Start()
    {
        buttonPos = transform.position;
    }
    public void PlaceCard()
    {
        ManaSystem.CurrentMana -= 2;
        Instantiate(cowwithgunPrefab, buttonPos, transform.rotation);
        towerSpots.SetActive(false);
        gameObject.SetActive(false);
    }
}