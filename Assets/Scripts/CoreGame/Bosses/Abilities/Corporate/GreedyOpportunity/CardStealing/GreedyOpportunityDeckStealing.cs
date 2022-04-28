using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreedyOpportunityDeckStealing : MonoBehaviour
{
    public DeckScript deck;

    public CardScript anyCard;

    [HideInInspector] public bool isCardStolen;
    [SerializeField] private Animator animationOfHand;

    private GameObject theHand;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "OverlordHand")
        {
            theHand = collision.gameObject;
            theHand.SetActive(false);
            animationOfHand.SetTrigger("Steal");
            //StealFromDeck(collision.gameObject); //Runns the program, gives it accsess to the OverlordHand
        }
    }

    private void Update()
    {
        if (isCardStolen)
        {
            isCardStolen = false;
            theHand.SetActive(true);
            StealFromDeck(theHand);
        }
    }

    [ContextMenu("StealFromDeck")]
    public void StealFromDeck(GameObject OverlordHand)
    {
        //Get the info
        int deckSize;
        int cardToRemove;

        deckSize = DeckScript.Deck.Count;
        cardToRemove = Random.Range(0, deckSize);

        if (OverlordHand.GetComponent<GreedyOpportunity>())
        {
            int childCount = OverlordHand.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                if (OverlordHand.transform.GetChild(i).gameObject.GetComponent<CardDisplayer>()) //Safety so that if the child is moved, it can still be found.
                {
                    OverlordHand.transform.GetChild(i).gameObject.SetActive(true); //Turns on the card in the hand.
                }
            }


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
