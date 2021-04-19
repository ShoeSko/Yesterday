using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CardReward : MonoBehaviour
{
    public List<GameObject> totalCards = new List<GameObject>();//all of the cards in the game
    public List<GameObject> rewardCards = new List<GameObject>();//The random cards given to the player
    public List<TextMeshProUGUI> rewardCardsTMPro = new List<TextMeshProUGUI>();//The names of random cards given to the player ** //Second note, will always be the same size as rewardCards

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

            rewardCardsTMPro[cardsToRandomize].text = activeCard.name; //Write the name of the current random card. **

            rewardCardsTMPro[cardsToRandomize].transform.position = new Vector2(prefabPos.x, prefabPos.y + 1);//offset for the text to appear above the icon
            activeCard.transform.position = prefabPos;
            Instantiate(activeCard);
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
        //select these cards
        NextScene();
    }
    public void selectG2()//add cards from group 2 to your deck
    {
        //select these cards
        NextScene();
    }
    public void selectG3()//add cards from group 3 to your deck
    {
        //select these cards
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