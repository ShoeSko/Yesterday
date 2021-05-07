using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpotsScript : MonoBehaviour
{
    private Vector2 buttonPos;
    public GameObject towerSpots;
    public Transform positionResetTransform;
    private GameObject card;
    private GameObject cardplayed;
    private CardDisplayer CardValues;
    private GameObject Unit;
    private int manacost;
    public GameObject Deck;
    private DeckScript DeckCode;


    public void Start()
    {
        buttonPos = transform.position;
        card = GameObject.Find("HANDscript");
        DeckCode = Deck.GetComponent<DeckScript>();
    }
    public void PlaceCard()
    {
        cardplayed = card.GetComponent<NewCardHandScript>().PlayedCard;//"What card is being played?"
        CardValues = cardplayed.GetComponent<CardDisplayer>();//Access played card information

        manacost = CardValues.manaValue; //Define manacost
        Unit = CardValues.UnitPrefab;//Define unit 


        ManaSystem.CurrentMana -= manacost;
        GameObject unit = Instantiate(Unit, buttonPos, transform.rotation);//spawn the correct unit
        unit.transform.SetParent(positionResetTransform, true);
        this.transform.SetParent(unit.transform,true);
        towerSpots.SetActive(false);
        gameObject.SetActive(false);

        DeckCode.Randomise();
        CardValues.card = DeckCode.activecard;
        cardplayed.GetComponent<CardDisplayer>().Read();
        NewCardHandScript.s_cardWasPlayer = true;
        card.GetComponent<NewCardHandScript>().ReSetCard();
    }

    private void Update()
    {
        if (transform.parent == positionResetTransform && towerSpots)
        {
            //print("Found " + towerSpots + " it was not this");
            transform.SetParent(towerSpots.transform, true);
            //print("I run");
        }
    }
}