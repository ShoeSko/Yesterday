using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CardReward : MonoBehaviour
{
    public List<CardScript> totalCards = new List<CardScript>();//all of the cards in the game
    public List<GameObject> rewardCards = new List<GameObject>();//The random cards given to the player
    public List<TextMeshProUGUI> rewardCardsTMPro = new List<TextMeshProUGUI>();//The names of random cards given to the player ** //Second note, will always be the same size as rewardCards

    private List<CardScript> Group1 = new List<CardScript>();
    private List<CardScript> Group2 = new List<CardScript>();
    private List<CardScript> Group3 = new List<CardScript>();

    private int sizeOfCards;
    private int sizeOfRewardCards;

    private CardScript activeCard;//This is the "current" card being looked at in the for loop
    private int randomCardInt;//Sum of the cards in the selection pool
    private GameObject cardPrefab;//Current card prefab
    private string CardName;//Name of the current card
    private Vector3 prefabPos;//defines where to spawn the card preview prefab

    private int groupNumber;//used to place cards in groups

    private int groupOfCards;


    private void Start()
    {
        groupNumber = 1;

        sizeOfCards = totalCards.Count;
        sizeOfRewardCards = rewardCards.Count;

        randomize3Cards();//3 star reward
        //future updates: 2 star reward & 1 star reward
    }

    void randomize3Cards()//3 star reward
    {
        for (int cardsToRandomize = 0; cardsToRandomize < sizeOfRewardCards; cardsToRandomize++)
        {
            cardPrefab = rewardCards[cardsToRandomize];//define the active card spot that's being looked at
            Debug.Log(cardPrefab.name);
            prefabPos = cardPrefab.transform.position;

            randomCardInt = Random.Range(0, sizeOfCards);//randomise a card from the total card pool
            activeCard = totalCards[randomCardInt];

            CardName = activeCard.cardName;
            Debug.Log(CardName);

            cardPrefab = activeCard.Prefab;

            rewardCardsTMPro[cardsToRandomize].text = CardName; //Write the name of the current random card. **

            rewardCardsTMPro[cardsToRandomize].transform.position = new Vector2(prefabPos.x, prefabPos.y + 1);//offset for the text to appear above the icon
            cardPrefab.transform.position = prefabPos;
            Instantiate(cardPrefab);

            //assign cards in groups
            if (groupNumber == 1)
                Group1.Add(activeCard);
            else if (groupNumber == 2)
                Group2.Add(activeCard);
            else if (groupNumber == 3)
            {
                Group3.Add(activeCard);
                groupNumber = 0;
            }

            groupNumber++;
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



    public void selectG1()//add cards from group 1 to your deck
    {
        groupOfCards = Group1.Count;

        for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
        {
            activeCard = Group1[CardsInGroup];
            DeckScript.Deck.Add(activeCard);
        }

        NextScene();
    }
    public void selectG2()//add cards from group 2 to your deck
    {
        groupOfCards = Group2.Count;

        for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
        {
            activeCard = Group2[CardsInGroup];
            DeckScript.Deck.Add(activeCard);
        }

        NextScene();
    }
    public void selectG3()//add cards from group 3 to your deck
    {
        groupOfCards = Group3.Count;

        for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
        {
            activeCard = Group3[CardsInGroup];
            DeckScript.Deck.Add(activeCard);
        }

        NextScene();
    }

    private void NextScene()
    {
        if (MinigameSceneScript.activeMinigame == 1)
        {
            MinigameSceneScript.activeMinigame++;
            SceneManager.LoadScene("Minigame#" + MinigameSceneScript.scene2);
        }
        else if (MinigameSceneScript.activeMinigame == 2)
        {
            MinigameSceneScript.activeMinigame++;
            SceneManager.LoadScene("Minigame#" + MinigameSceneScript.scene3);
        }
        else if (MinigameSceneScript.activeMinigame == 3)
        {
            SceneManager.LoadScene("CoreGame");
        }
    }
}