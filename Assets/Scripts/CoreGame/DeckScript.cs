using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public static List<CardScript> Deck = new List<CardScript>();
    public List<CardScript> RodentsList = new List<CardScript>();

    private int DeckCards;
    public CardScript activecard;
    private int randomcard;

    public bool CardTesting;

    public CardScript CowCard;
    public CardScript Chikin;
    public CardScript Sheep;
    public CardScript TestCard; //Test thingy
    public CardScript TestCard2; //Test thingy
    public CardScript TestCard3; //Test thingy
    public CardScript TestCard4; //Test thingy
    public CardScript TestCard5; //Test thingy
    public CardScript TestCard6; //Test thingy
    public CardScript TestCard7; //Test thingy
    public CardScript TestCard8; //Test thingy
    public CardScript TestCard9; //Test thingy
    public CardScript TestCard10; //Test thingy
    public CardScript TestCard11; //Test thingy
    public CardScript TestCard12; //Test thingy
    public CardScript TestCard13; //Test thingy
    public CardScript TestCard14; //Test thingy
    public CardScript TestCard15; //Test thingy
    public CardScript TestCard16; //Test thingy

    public GameObject deckViewer;
    public GameObject ViewDeck;

    public int Rodents;//For SquirrelFamily card


    void Start()
    {
        if (CardTesting)
        {
            Deck.Add(TestCard);
            Deck.Add(TestCard2);
            Deck.Add(TestCard3);
            Deck.Add(TestCard4);
            Deck.Add(TestCard5);
            Deck.Add(TestCard6);
            Deck.Add(TestCard7);
            Deck.Add(TestCard8);
            Deck.Add(TestCard9);
            Deck.Add(TestCard10);
            Deck.Add(TestCard11);
            Deck.Add(TestCard12);
            Deck.Add(TestCard13);
            Deck.Add(TestCard14);
            Deck.Add(TestCard15);
            Deck.Add(TestCard16);
            //Everything above is used for testing, unlock the thing under when finished with testing
        }
        else
        {
            Deck.Add(CowCard);//the starting cards 
            Deck.Add(Chikin);//the starting cards 
            Deck.Add(Sheep);//the starting cards 
        }

        DeckUpdater();
        
    }

    public void DeckUpdater()
    {
        if(Deck.Count == 0) //Prevents the deck from ever running out.
        {
        Deck.Add(CowCard);//the starting cards 
        }

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

            if (activecard == null)//just in case something crashes
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

    public void CheckForRodents()
    {
        for (int spot = 0; spot < DeckCards; spot++)
        {
            Debug.Log("Looking at card number " + spot);

            for (int RodentNumber = 0; RodentNumber < RodentsList.Count; RodentNumber++)
            {
                if (Deck[spot] == RodentsList[RodentNumber])
                {
                    Rodents++;
                    RodentNumber = RodentsList.Count+1;
                }
            }
        }
        Debug.Log("Found this many rodents: " + Rodents);
    }

    public void OstrichCowardness()
    {
        for (int card = 0; card < DeckCards; card++)
        {
            CardScript CurrentCard = Deck[card];


            Debug.Log("I am looking at card number " + card + ", its " + CurrentCard);

            if(CurrentCard.cardName == "Saddler")
            {
                Deck.Remove(CurrentCard);
                Debug.Log("This is me, im saddler and should be removed");
                card--;
                DeckCards--;
            }
        }
    }
}