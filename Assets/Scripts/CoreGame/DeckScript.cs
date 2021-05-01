using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public static List<CardScript> Deck = new List<CardScript>();

    private int DeckCards;
    public CardScript activecard;
    private int randomcard;

    public CardScript CowCard;

    public GameObject deckViewer;
    public GameObject ViewDeck;


    void Start()
    {
        Deck.Add(CowCard);//the starting cards 

        DeckCards = Deck.Count;

        deckViewer.GetComponent<DeckView>().CreateList();

        ViewDeck.SetActive(false);
    }

    public void Randomise()
    {
        for (int cards = 0; cards < 1; cards++)
        {
            randomcard = Random.Range(0, DeckCards);

            activecard = Deck[randomcard];

            if (activecard == null)
            {
                cards--;
            }

        }
    }

    public void DeckViewModeOn()
    {
        Time.timeScale = 0;
    }
    public void DeckViewModeOff()
    {
        Time.timeScale = 1;
    }
}