using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpotsScript : MonoBehaviour
{
    public GameObject cowwithgunPrefab;//temporary solution: the unit that's supposed to be summoned
    private Vector2 buttonPos;
    public GameObject towerSpots;
    public Transform positionResetTransform;
    private GameObject card;
    private GameObject cardplayed;
    private CardDisplayer CardValues;
    private int manacost;


    public void Start()
    {
        buttonPos = transform.position;
        card = GameObject.Find("HANDscript");
    }
    public void PlaceCard()
    {
        cardplayed = card.GetComponent<NewCardHandScript>().PlayedCard;//"What card is being played?"

        CardValues = cardplayed.GetComponent<CardDisplayer>();//access played card information
        manacost = CardValues.manaValue; //define manacost
        //define more stuff later like 'prefabs', different cards, spells, etc.


        ManaSystem.CurrentMana -= manacost;//temporary solution: how much mana the unit costs
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