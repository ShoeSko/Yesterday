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

    public List<GameObject> ViewCardGroup = new List<GameObject>();

    private int sizeOfCards;
    private int sizeOfRewardCards;

    private CardScript activeCard;//This is the "current" card being looked at in the for loop
    private int randomCardInt;//Sum of the cards in the selection pool
    private GameObject cardPrefab;//Current card prefab
    private string CardName;//Name of the current card
    private Vector3 prefabPos;//defines where to spawn the card preview prefab

    private int groupNumber;//used to place cards in groups

    private int groupOfCards;
    private int StarLimit;
    public static int Stars;

    public GameObject Overview;
    public GameObject ViewView;


    private GameObject[] unitList;
    private int unitListLenght;


    private void Start()
    {
        Overview.SetActive(true);
        ViewView.SetActive(false);

        StarLimit = 0;
        groupNumber = 1;

        sizeOfCards = totalCards.Count;
        sizeOfRewardCards = rewardCards.Count;

        if(Stars == 3)
            randomize3Cards();
        if (Stars == 2)
            randomize2Cards();
        if (Stars == 1)
            randomize1Cards();
    }

    public void randomize3Cards()//3 star reward
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


    public void randomize2Cards()//2 star reward
    {
        for (int cardsToRandomize = 0; cardsToRandomize < sizeOfRewardCards; cardsToRandomize++)
        {
            StarLimit++;

            cardPrefab = rewardCards[cardsToRandomize];//define the active card spot that's being looked at
            Debug.Log(cardPrefab.name);
            prefabPos = cardPrefab.transform.position;

            if(StarLimit <= 6)//limit cards to randomise to 6
            {
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
            else
                cardPrefab.SetActive(false);
        }
    }


    public void randomize1Cards()//1 star reward
    {
        for (int cardsToRandomize = 0; cardsToRandomize < sizeOfRewardCards; cardsToRandomize++)
        {
            StarLimit++;

            cardPrefab = rewardCards[cardsToRandomize];//define the active card spot that's being looked at
            Debug.Log(cardPrefab.name);
            prefabPos = cardPrefab.transform.position;

            if (StarLimit <= 3)//limit cards to randomise to 3
            {
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
            else
                cardPrefab.SetActive(false);
        }
    }



    public void viewG1()//view all cards from group 1
    {
        Overview.SetActive(false);
        ViewView.SetActive(true);

        groupOfCards = Group1.Count;

        for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
        {
            activeCard = Group1[CardsInGroup];
            cardPrefab = ViewCardGroup[CardsInGroup];

            cardPrefab.SetActive(true);
            cardPrefab.GetComponent<CardDisplayer>().card = activeCard;

            cardPrefab.GetComponent<CardDisplayer>().Read();

            ViewCleaner(); //Will clean the scene of objects during view.
        }
    }
    public void viewG2()//view all cards from group 2
    {
        Overview.SetActive(false);
        ViewView.SetActive(true);

        groupOfCards = Group2.Count;

        for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
        {
            activeCard = Group2[CardsInGroup];
            cardPrefab = ViewCardGroup[CardsInGroup];

            cardPrefab.SetActive(true);
            cardPrefab.GetComponent<CardDisplayer>().card = activeCard;

            cardPrefab.GetComponent<CardDisplayer>().Read();

            ViewCleaner(); //Will clean the scene of objects during view.
        }
    }
    public void viewG3()//view all cards from group 3
    {
        Overview.SetActive(false);
        ViewView.SetActive(true);

        groupOfCards = Group3.Count;

        for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
        {
            activeCard = Group3[CardsInGroup];
            cardPrefab = ViewCardGroup[CardsInGroup];

            cardPrefab.SetActive(true);
            cardPrefab.GetComponent<CardDisplayer>().card = activeCard;

            cardPrefab.GetComponent<CardDisplayer>().Read();

            ViewCleaner(); //Will clean the scene of objects during view.
        }
    }

    private void ViewCleaner()
    {
        SpriteRenderer unitRenderer;
        unitList = GameObject.FindGameObjectsWithTag("Obstacle"); //Grabs all Units spawned
        unitListLenght = unitList.Length;
        for (int i = 0; i < unitListLenght; i++)
        {
            unitList[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void selectG1()//add cards from group 1 to your deck
    {
        groupOfCards = Group1.Count;

        for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
        {
            activeCard = Group1[CardsInGroup];
            DeckScript.Deck.Add(activeCard);
        }

        //NextScene();
    }
    public void selectG2()//add cards from group 2 to your deck
    {
        groupOfCards = Group2.Count;

        for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
        {
            activeCard = Group2[CardsInGroup];
            DeckScript.Deck.Add(activeCard);
        }

        //NextScene();
    }
    public void selectG3()//add cards from group 3 to your deck
    {
        groupOfCards = Group3.Count;

        for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
        {
            activeCard = Group3[CardsInGroup];
            DeckScript.Deck.Add(activeCard);
        }

        //NextScene();
    }

    //private void NextScene()//The code to change scene after choosing your cards
    //{
    //    if (MinigameSceneScript.activeMinigame == 1)
    //    {
    //        MinigameSceneScript.activeMinigame++;
    //        SceneManager.LoadScene("Minigame#" + MinigameSceneScript.scene2);
    //    }
    //    else if (MinigameSceneScript.activeMinigame == 2)
    //    {
    //        MinigameSceneScript.activeMinigame++;
    //        SceneManager.LoadScene("Minigame#" + MinigameSceneScript.scene3);
    //    }
    //    else if (MinigameSceneScript.activeMinigame == 3)
    //    {
    //        SceneManager.LoadScene("CoreGame");
    //    }
    //}

    public void Back()
    {
        ViewBacker(); //Will revert the Objects upon return

        Overview.SetActive(true);
        ViewView.SetActive(false);
    }

    private void ViewBacker()
    {
        print("I was triggered!!!!");
        for (int i = 0; i < unitListLenght; i++)
        {
            unitList[i].GetComponent<SpriteRenderer>().enabled = true;
            print(unitList[i].name + (" should be turned on now"));
        }
    }
}