using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public List<CardScript> Deck = new List<CardScript>();

    private int DeckCards;
    public CardScript activecard;
    private int randomcard;

    //public CardDisplayer chosenCard;


    void Start()
    {
        DeckCards = Deck.Count;
    }

    public void Randomise()
    {
        for (int cards = 0; cards < 1; cards++)
        {
            randomcard = Random.Range(0, DeckCards);

            activecard = Deck[randomcard];

            if (activecard != null)
            {
                Debug.Log(activecard.name);

                //chosenCard.card = activecard.GetComponent<CardDisplayer>().card;
            }
            else
                cards--;
        }
    }
}