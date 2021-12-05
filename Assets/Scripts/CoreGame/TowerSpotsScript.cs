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
    [SerializeField ]private int laneNumber; //Used to indicate which lane the spot is from.
    private int lanePlacement; //Used to indicate the placement in the lane.
    public static bool NuggetSpawnOnce;

    public void Start()
    {
        buttonPos = transform.position;
        card = GameObject.Find("HANDscript");
        DeckCode = Deck.GetComponent<DeckScript>();
    }

    public void PlaceCard(int LanePlacement)
    {
        lanePlacement = LanePlacement;

        cardplayed = card.GetComponent<NewCardHandScript>().PlayedCard;//"What card is being played?"
        CardValues = cardplayed.GetComponent<CardDisplayer>();//Access played card information

        manacost = CardValues.manaValue; //Define manacost
        Unit = CardValues.UnitPrefab;//Define unit 


        ManaSystem.CurrentMana -= manacost;
        GameObject unit = Instantiate(Unit, buttonPos, transform.rotation);//spawn the correct unit
        unit.transform.SetParent(positionResetTransform, true);
        this.transform.SetParent(unit.transform,true);

        LanePlacedUnits.PlaceNewUnitInList(unit,laneNumber,lanePlacement); //Places the newly spawned unit in a list to be refered from.

        unit.GetComponent<UnitPrototypeScript>().DefineUnitPlacement(laneNumber, lanePlacement); // Gives the Unit Prototype Script an actual reference to their placement.

        towerSpots.SetActive(false);
        gameObject.SetActive(false);


        //Nugget's ability WIP
        if (CardValues.card.cardName == "Nugget" && NuggetSpawnOnce == false)
        {
            towerSpots.SetActive(true);
            NuggetSpawnOnce = true;
        }
        else
        {
            DeckCode.Randomise();
            CardValues.card = DeckCode.activecard;
            cardplayed.GetComponent<CardDisplayer>().Read();
            NewCardHandScript.s_cardWasPlayer = true;
            card.GetComponent<NewCardHandScript>().ReSetCard();
            NuggetSpawnOnce = false;
        }
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

    public void TutorialPlaceUnit()
    {
        if(MinigameSceneScript.Tutorial == true)
        {
            card = GameObject.Find("HANDscript");
            card.GetComponent<NewCardHandScript>().PlayedFirstCard();
        }
    }
}