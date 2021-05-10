using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckView : MonoBehaviour
{
    public List<GameObject> Cards = new List<GameObject>();

    private int CardList;
    private int DeckList;

    private GameObject activeCard;
    private CardDisplayer CardStats;


    public void CreateList()
    {
        CardList = Cards.Count;
        DeckList = DeckScript.Deck.Count;

        for (int card = 0; card < CardList; card++)
        {
            activeCard = Cards[card];

            if (card < DeckList)
            {
                activeCard.SetActive(true); //
                CardStats = activeCard.GetComponent<CardDisplayer>();
                CardStats.card = DeckScript.Deck[card];
                CardStats.Read();
            }
            else
            {
                //Destroy(activeCard);
                activeCard.SetActive(false);
            }

        }
    }
}
