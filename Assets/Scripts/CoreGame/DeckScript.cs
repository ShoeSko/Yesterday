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

    public CardScript CowCard;
    public CardScript Chikin;
    public CardScript Sheep;
    public CardScript TestCard; //Test thingy
    public CardScript TestCard2; //Test thingy

    public GameObject deckViewer;
    public GameObject ViewDeck;

    public int Rodents;//For SquirrelFamily card


    void Start()
    {
        //Deck.Add(TestCard);
        //Deck.Add(TestCard2);
        //Everything above is used for testing, unlock the thing under when finished with testing

        
        Deck.Add(CowCard);//the starting cards 
        Deck.Add(Chikin);//the starting cards 
        Deck.Add(Sheep);//the starting cards 
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
        for (int cards = 0; cards < DeckCards; cards++)
        {
            Debug.Log("Looking at card number " + cards);

            for (int RodentNumber = 0; RodentNumber <= RodentsList.Count; RodentNumber++)
            {
                if (Deck[cards] == RodentsList[RodentNumber])
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