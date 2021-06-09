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

    private bool ChosenUnits;


    private GameObject[] unitList;
    private UnitPrototypeScript[] unitListPrototype;
    private int unitListLenght;


    //Tutorial stuff
    [Header("Tutorial")]
    public static int TutorialMission;
    public GameObject view1Button;
    public GameObject choose1Button;
    public GameObject view2Button;
    public GameObject choose2Button;
    public GameObject view3Button;
    public GameObject choose3Button;
    public GameObject cardgroup1;
    public GameObject cardgroup2;
    public GameObject cardgroup3;
    public GameObject RobotWave;
    public GameObject SpeechBubble;
    public GameObject continueText;
    public GameObject viewBackButton;

    private bool AllreadyActivated; 

    public List<GameObject> Texts1 = new List<GameObject>();
    public List<GameObject> Texts2 = new List<GameObject>();
    public List<GameObject> Texts3 = new List<GameObject>();
    private int Text;
    [SerializeField] private float timeBeforeBubbleIsHidden;

    [Header("Options")]
    [SerializeField] private GameObject optionsButton;

    private void Start()
    {
        ChosenUnits = false;
        sizeOfCards = totalCards.Count;
        sizeOfRewardCards = rewardCards.Count;

        if (MinigameSceneScript.Tutorial == false)
        {
            TutorialMission = 0;//hopefully resets the tutorial
            Overview.SetActive(true);
            ViewView.SetActive(false);

            StarLimit = 0;
            groupNumber = 1;

            if (Stars == 3)
                randomize3Cards();
            if (Stars == 2)
                randomize2Cards();
            if (Stars == 1)
                randomize1Cards();
        }
        else//Tutorial settings
        {
            Text = 0;
            view1Button.SetActive(false);
            choose1Button.SetActive(false);

            RobotWave.SetActive(true);
            SpeechBubble.SetActive(true);
            continueText.SetActive(true);

            //First reward screen of tutorial
            if (TutorialMission == 1)
            {
                cardgroup3.SetActive(false);
                cardgroup2.SetActive(false);
                Texts1[Text].SetActive(true);
                optionsButton.SetActive(false);
            }//Second reward screen of tutorial
            else if(TutorialMission == 2)
            {
                cardgroup3.SetActive(false);
                cardgroup2.SetActive(false);
                Texts2[Text].SetActive(true);
                optionsButton.SetActive(false);

                TutorialRandomG2();
            }//Third reward screen of tutorial
            else if(TutorialMission == 3)
            {
                groupNumber = 1;

                Texts3[Text].SetActive(true);
                randomize3Cards();
                view2Button.SetActive(false);
                choose2Button.SetActive(false);
                view3Button.SetActive(false);
                choose3Button.SetActive(false);
                optionsButton.SetActive(false);
            }
        }
    }

    private void Update()
    {
        //Tutorial stuff
        if(TutorialMission == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                if(Text != 3)
                {
                    if (Text == 0)
                        TutorialRandomG1();

                    Texts1[Text].SetActive(false);
                    Text++;
                    Texts1[Text].SetActive(true);

                    if (Text == 3)
                    {
                        choose1Button.SetActive(true);
                        continueText.SetActive(false);
                    }
                }
            }
        }
        else if(TutorialMission == 2)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                if(Text == 0)
                {
                    Texts2[Text].SetActive(false);
                    Text++;
                    Texts2[Text].SetActive(true);
                    view1Button.SetActive(true);
                    continueText.SetActive(false);
                }

                if(Text == 2)
                {
                    viewBackButton.SetActive(true);
                    Texts2[Text].SetActive(false);
                    SpeechBubble.SetActive(false);
                    RobotWave.SetActive(false);
                    continueText.SetActive(false);
                    Text++;
                }
            }
        }
        else if(TutorialMission == 3)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                if(Text != 2)
                {
                    Texts3[Text].SetActive(false);
                    Text++;

                    if (Text != 2)
                        Texts3[Text].SetActive(true);
                    else
                    {
                        RobotWave.SetActive(false);
                        SpeechBubble.SetActive(false);
                        continueText.SetActive(false);

                        view1Button.SetActive(true);
                        choose1Button.SetActive(true);
                        view2Button.SetActive(true);
                        choose2Button.SetActive(true);
                        view3Button.SetActive(true);
                        choose3Button.SetActive(true);
                    }
                }
            }
        }
    }
    
    public void randomize3Cards()//3 star reward
    {
        for (int cardsToRandomize = 0; cardsToRandomize < sizeOfRewardCards; cardsToRandomize++)
        {
            cardPrefab = rewardCards[cardsToRandomize];//define the active card spot that's being looked at
            cardPrefab.SetActive(true);
            prefabPos = cardPrefab.transform.position;

            randomCardInt = Random.Range(0, sizeOfCards);//randomise a card from the total card pool
            activeCard = totalCards[randomCardInt];

            cardPrefab.GetComponent<CardRewardDisplayer>().card = activeCard;
            cardPrefab.GetComponent<CardRewardDisplayer>().Read();


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
            cardPrefab.SetActive(true);
            prefabPos = cardPrefab.transform.position;

            if (StarLimit <= 6)//limit cards to randomise to 6
            {
                randomCardInt = Random.Range(0, sizeOfCards);//randomise a card from the total card pool
                activeCard = totalCards[randomCardInt];

                cardPrefab.GetComponent<CardRewardDisplayer>().card = activeCard;
                cardPrefab.GetComponent<CardRewardDisplayer>().Read();

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
            cardPrefab.SetActive(true);
            prefabPos = cardPrefab.transform.position;

            if (StarLimit <= 3)//limit cards to randomise to 3
            {
                randomCardInt = Random.Range(0, sizeOfCards);//randomise a card from the total card pool
                activeCard = totalCards[randomCardInt];

                cardPrefab.GetComponent<CardRewardDisplayer>().card = activeCard;
                cardPrefab.GetComponent<CardRewardDisplayer>().Read();

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

    private void ViewCleaner() //This entire thing needs rework with art.
    {
        SpriteRenderer unitRenderer;
        unitListPrototype = UnitPrototypeScript.FindObjectsOfType<UnitPrototypeScript>(); //Grabs all Units spawned
        unitList = new GameObject[unitListPrototype.Length];
        for (int i = 0; i < unitListPrototype.Length; i++)
        {
            unitList[i] = unitListPrototype[i].gameObject;
        }
        unitListLenght = unitList.Length;
        for (int i = 0; i < unitListLenght; i++)
        {
            unitList[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void selectG1()//add cards from group 1 to your deck
    {
        if(ChosenUnits == false)
        {
            groupOfCards = Group1.Count;

            for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
            {
                activeCard = Group1[CardsInGroup];
                DeckScript.Deck.Add(activeCard);

                if (MinigameSceneScript.Tutorial == false) //Stores the info only they are playing normal.
                {
                    UnitForBeastiary(activeCard.unitIndex); //Stores the data in the save file.
                }
            }
            ChosenUnits = true;
        }
    }
    public void selectG2()//add cards from group 2 to your deck
    {
        if(ChosenUnits == false)
        {
            groupOfCards = Group2.Count;

            for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
            {
                activeCard = Group2[CardsInGroup];
                DeckScript.Deck.Add(activeCard);

                if (MinigameSceneScript.Tutorial == false) //Stores the info only they are playing normal.
                {
                    UnitForBeastiary(activeCard.unitIndex); //Stores the data in the save file.
                }
            }
            ChosenUnits = true;
        }
    }
    public void selectG3()//add cards from group 3 to your deck
    {
        if(ChosenUnits == false)
        {
            groupOfCards = Group3.Count;

            for (int CardsInGroup = 0; CardsInGroup < groupOfCards; CardsInGroup++)
            {
                activeCard = Group3[CardsInGroup];
                DeckScript.Deck.Add(activeCard);

                if (MinigameSceneScript.Tutorial == false) //Stores the info only they are playing normal.
                {
                    UnitForBeastiary(activeCard.unitIndex); //Stores the data in the save file.
                }
            }
            ChosenUnits = true;
        }
    }

    #region Beastiary storing
    private void UnitForBeastiary(int indexOfUnit)
    {
        if (FindObjectOfType<SaveSystem>()) //Confirms that the save system is in the scene
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>(); //Finds the save system in the scene
            int indexLenght = saving.data.unitList.Length; //Aquires the lenght of the Array to store in.

            for (int index = 0; index < indexLenght; index++) //Runs a loop through the entire array until it reaches the same index as the unit.
            {
                if(index == indexOfUnit) //If current unit is equal to the in the loop. (Make sure he goes from 0.39)
                {
                    if(saving.data.unitList[index] == false)
                    {
                        //Everything here will we done when a unit is for the first time picked in the reward screen.
                        saving.data.unitList[index] = true;

                        print(indexOfUnit + " is the index of the unit that was just chosen.");
                    }
                }
            }
        }
    }
    #endregion

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

    private void TutorialRandomG1()
    {
        for (int cardsToRandomize = 0; cardsToRandomize < sizeOfRewardCards; cardsToRandomize += 3)
        {
            cardPrefab = rewardCards[cardsToRandomize];//define the active card spot that's being looked at
            cardPrefab.SetActive(true);
            prefabPos = cardPrefab.transform.position;

            randomCardInt = Random.Range(0, sizeOfCards);//randomise a card from the total card pool
            activeCard = totalCards[randomCardInt];

            cardPrefab.GetComponent<CardRewardDisplayer>().card = activeCard;
            cardPrefab.GetComponent<CardRewardDisplayer>().Read();

            Group1.Add(activeCard);
        }
    }

    private void TutorialRandomG2()
    {
        for (int cardsToRandomize = 0; cardsToRandomize < sizeOfRewardCards; cardsToRandomize += 3)
        {
            StarLimit++;

            cardPrefab = rewardCards[cardsToRandomize];//define the active card spot that's being looked at
            prefabPos = cardPrefab.transform.position;

            if (StarLimit <= 2)//limit cards to randomise to 6
            {
                cardPrefab = rewardCards[cardsToRandomize];//define the active card spot that's being looked at
                cardPrefab.SetActive(true);
                prefabPos = cardPrefab.transform.position;

                randomCardInt = Random.Range(0, sizeOfCards);//randomise a card from the total card pool
                activeCard = totalCards[randomCardInt];

                cardPrefab.GetComponent<CardRewardDisplayer>().card = activeCard;
                cardPrefab.GetComponent<CardRewardDisplayer>().Read();

                Group1.Add(activeCard);
            }
        }
    }

    public void TutorialView2()
    {
        if(MinigameSceneScript.Tutorial == true && TutorialMission == 2 && AllreadyActivated == false)
        {
            choose1Button.SetActive(true);
            viewBackButton.SetActive(false);
            Texts2[Text].SetActive(false);
            Text = 2;
            Texts2[Text].SetActive(true);
            continueText.SetActive(true);
            AllreadyActivated = true;
        }
    }
}