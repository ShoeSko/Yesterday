using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReward : MonoBehaviour
{
    public List<GameObject> totalCards = new List<GameObject>();//all of the cards in the game
    public List<GameObject> rewardCards = new List<GameObject>();//The random cards given to the player

    private int sizeOfCards;
    private int sizeOfRewardCards;

    private GameObject activeCard;//This is the "current" card being looked at in the for loop
    private int randomCardInt;
    private GameObject randomCard;//random card from the total card group

    private Vector3 prefabPos;//defines where to spawn the card preview prefab


    private void Start()
    {
        sizeOfCards = totalCards.Count;
        sizeOfRewardCards = rewardCards.Count;

        randomize3Cards();//3 star reward
        //future updates: 2 star reward & 1 star reward
    }

    void randomize3Cards()//3 star reward
    {
        for (int cardsToRandomize = 0; cardsToRandomize < sizeOfRewardCards; cardsToRandomize++)
        {
            activeCard = rewardCards[cardsToRandomize];//define the active card that's being looked at
            Debug.Log(activeCard.name);

            prefabPos = activeCard.transform.position;

            randomCardInt = Random.Range(0, sizeOfCards);//randomise a card from the total card pool
            randomCard = totalCards[randomCardInt];

            activeCard = randomCard;//set the active card to the random card
            Debug.Log(activeCard.name);

            activeCard.transform.position = prefabPos;
            Instantiate(activeCard);//spawn the card (except its bugged for some reason)
        }
    }



    void viewG1()//view all cards from group 1
    {

    }
    void viewG2()//view all cards from group 2
    {

    }
    void viewG3()//view all cards from group 3
    {

    }



    void selectG1()//add cards from group 1 to your deck
    {

    }
    void selectG2()//add cards from group 2 to your deck
    {

    }
    void selectG3()//add cards from group 3 to your deck
    {

    }
}