using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyOpportunityDeckStealing : MonoBehaviour
{
    public DeckScript deck;

    public CardScript anyCard;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "OverlordHand")
        {
            StealFromDeck(collision.gameObject); //Runns the program, gives it accsess to the OverlordHand
        }
    }

    [ContextMenu("StealFromDeck")]
    private void StealFromDeck(GameObject OverlordHand)
    {
        //Get the info
        int deckSize;
        int cardToRemove;

        deckSize = DeckScript.Deck.Count;
        cardToRemove = Random.Range(0, deckSize);

        if (OverlordHand.GetComponent<GreedyOpportunity>())
        {
            print(OverlordHand.name);
            OverlordHand.GetComponent<GreedyOpportunity>().isRetreating = true;
        }

        //Initiate the steal
        DeckScript.Deck.RemoveAt(cardToRemove);

        print("Steals from deck, it now contains = " + DeckScript.Deck.Count);
        deck.DeckUpdater(); //Updates the Deck
    }
    [ContextMenu("Add a card")]
    private void AddCard() //This exists only to test Card adding.
    {
        DeckScript.Deck.Add(anyCard);
        deck.DeckUpdater(); //Updates the Deck
        print("Should be added");
    }
}
