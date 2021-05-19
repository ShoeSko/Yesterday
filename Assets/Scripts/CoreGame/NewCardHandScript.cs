using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardHandScript : MonoBehaviour
{
    public bool Smallhand = true;

    public GameObject PlayedCard;
    public GameObject quackenButton;
    public GameObject Deck;
    private GameObject CurrentCard;
    public GameObject TheCorporatePrefab;

    private int card;

    public static int Stage;//Which stage is the player on? (Will define which enemies spawn)
    public GameObject SpawnerHuman;//Spawner for Businessmen
    public GameObject SpawnerBeast;//Spawner for Beasts
    public GameObject SpawnerBoss;//Spawner for Boss

    public AudioSource Music1;
    public AudioSource Music2;
    public AudioSource Boss1;

    public GameObject BG_Day;
    public GameObject BG_Evening;
    public GameObject BG_Night;


    public GameObject enlargeButton;//arrow up above your hand to enlarge your hand
    public GameObject minimizeButton;//arrow down visible after enlarging your hand
    public GameObject handSmall;//your hand at the bottom of the screen
    public GameObject handEnlarged;//enlarged hand after pressing the scroll up button
    public GameObject TowerSpots;

    public GameObject CardButton1S;//all of this is temporary
    public GameObject CardButton2S;
    public GameObject CardButton3S;
    public GameObject CardButton4S;
    public GameObject CardButton5S;

    public GameObject CardButton1L;//all of this is temporary
    public GameObject CardButton2L;
    public GameObject CardButton3L;
    public GameObject CardButton4L;
    public GameObject CardButton5L;

    public int ManaCost1;
    public GameObject card1;
    private CardDisplayer CardValues1;

    public int ManaCost2;
    public GameObject card2;
    private CardDisplayer CardValues2;

    public int ManaCost3;
    public GameObject card3;
    private CardDisplayer CardValues3;

    public int ManaCost4;
    public GameObject card4;
    private CardDisplayer CardValues4;

    public int ManaCost5;
    public GameObject card5;
    private CardDisplayer CardValues5;

    public GameObject Lcard1;
    public GameObject Lcard2;
    public GameObject Lcard3;
    public GameObject Lcard4;
    public GameObject Lcard5;

    #region Add On Variables
    private int cardNr; //This exists to communicate with TowerSpotsScript to tell it what card is being used for animations.
    public static bool s_cardWasPlayer; //Too make sure a card was player;
    [HideInInspector]public bool isManaCardGlow1;
    [HideInInspector]public bool isManaCardGlow2;
    [HideInInspector]public bool isManaCardGlow3;
    [HideInInspector]public bool isManaCardGlow4;
    [HideInInspector]public bool isManaCardGlow5;

    public List<GameObject> cardActiveIndicaterList = new List<GameObject>();
    #endregion



    //Additioanl objects & variables used for tutorial
    public GameObject manabox;
    public GameObject manabar;
    public GameObject ManaNumber;
    public GameObject StartButton;
    public GameObject DeckCanvas;
    public GameObject Lane1;
    public GameObject Lane2;
    public GameObject Lane3;
    public GameObject Lane4;
    public GameObject TS1;
    public GameObject TS3;
    public GameObject TS4;
    public GameObject DeckHideButton;

    public GameObject TutorialSpawner1;
    public GameObject TutorialSpawner2;
    public GameObject elitespawner1;
    public GameObject elitespawner2;
    public GameObject elitespawner3;
    public GameObject elitespawner4;


    public GameObject BotWave;
    public GameObject SpeechBubble;
    public GameObject ContinueText;
    public List<GameObject> Texts = new List<GameObject>();
    private int Text;
    private bool SpawnOnce;
    private float SpawnDelay;
    private bool CantClick;
    private bool PlayableCards;
    public bool LookForEnemies;
    public GameObject MenuReturn;



    void Start()
    {
        handEnlarged.SetActive(true);
        minimizeButton.SetActive(true);
        TowerSpots.SetActive(false);

        if(Stage == 1)
        {
            BG_Day.SetActive(true);
            BG_Evening.SetActive(false);
            BG_Night.SetActive(false);
        }
        else if(Stage == 2)
        {
            BG_Day.SetActive(false);
            BG_Evening.SetActive(true);
            BG_Night.SetActive(false);
        }
        else if(Stage == 3)
        {
            BG_Day.SetActive(false);
            BG_Evening.SetActive(false);
            BG_Night.SetActive(true);
        }

        ManaSystem.CurrentMana = 0;

        for (card = 1; card <= 5; card++)
        {
            Deck.GetComponent<DeckScript>().Randomise();

            CurrentCard = GameObject.Find("SCard" + card);
            CurrentCard.GetComponent<CardDisplayer>().card = Deck.GetComponent<DeckScript>().activecard;
            CurrentCard.GetComponent<CardDisplayer>().Read();

            StartCoroutine(CardAnimationOn(CurrentCard, card));//Made to do the animation on start.
        }

        SetCard();
        handEnlarged.SetActive(false);
        minimizeButton.SetActive(false);


        //Tutorial settings
        if(MinigameSceneScript.Tutorial == true)
        {
            BG_Day.SetActive(true);
            Text = 0;
            ManaSystem.CurrentMana = 10;

            //Deactivate everything
            handSmall.SetActive(false);
            quackenButton.SetActive(false);
            manabar.SetActive(false);
            manabox.SetActive(false);
            Deck.SetActive(false);
            DeckCanvas.SetActive(false);
            ManaNumber.SetActive(false);
            enlargeButton.SetActive(false);
            StartButton.SetActive(false);
            Lane1.SetActive(false);
            Lane3.SetActive(false);
            Lane4.SetActive(false);

            BotWave.SetActive(true);
            SpeechBubble.SetActive(true);
            ContinueText.SetActive(true);
            Texts[Text].SetActive(true);
        }
    }

    IEnumerator CardAnimationOn(GameObject animCard, int nr) //Initiates the card draw animation
    {
        animCard.GetComponent<Animator>().SetBool("CardUsed " + nr, true); //Turns on the bool that releases the animation
        yield return new WaitForSeconds(0.5f);
        animCard.GetComponent<Animator>().SetBool("CardUsed " + nr, false); //Turns of the bool after 1 second to prevent a loop of animation
    }

    void Update()
    {
        ManaCostDisplayer();

        ReadMana(); //Quick fix to read the mana value

        if(s_cardWasPlayer == true) { AnimationReset(); }
        CardSelectedActivation(); //Marks the current selected card in green.


        //Tutorial stuff
        if(MinigameSceneScript.Tutorial == true)
        {
            enlargeButton.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (Text == 18)
                    MenuReturn.GetComponent<LevelTransitionSystem>().GameOverButtonPress();

                if (Text != 2 && Text != 5 && Text != 8 && Text != 10 && Text != 14)
                {
                    if (CantClick == false)
                    {
                        if (Text == 11 || Text == 12 || Text == 13 || Text == 14 || Text == 15)
                            Time.timeScale = 1;

                        Texts[Text].SetActive(false);
                        Text++;
                        Texts[Text].SetActive(true);

                        if (Text == 2)
                        {
                            handSmall.SetActive(true);
                            ContinueText.SetActive(false);
                            PlayableCards = true;
                        }
                        if (Text == 3)
                        {
                            PlayableCards = false;
                        }
                        if (Text == 5)
                        {
                            ContinueText.SetActive(false);
                            TowerSpots.SetActive(true);
                            TS1.SetActive(false);
                            TS3.SetActive(false);
                            TS4.SetActive(false);
                        }
                        if (Text == 8)
                        {
                            ContinueText.SetActive(false);
                            DeckCanvas.SetActive(true);
                        }
                        if (Text == 10)
                        {
                            DeckHideButton.SetActive(true);
                            ContinueText.SetActive(false);
                        }
                        if (Text == 13)
                        {
                            Texts[Text].SetActive(false);
                            ContinueText.SetActive(false);
                            BotWave.SetActive(false);
                            SpeechBubble.SetActive(false);
                            CantClick = true;
                            ManaNumber.GetComponent<ManaSystem>().GameStarted();
                            PlayableCards = true;
                        }
                        if (Text == 14)
                        {
                            Texts[Text].SetActive(false);
                            ContinueText.SetActive(false);
                            BotWave.SetActive(false);
                            SpeechBubble.SetActive(false);
                            CantClick = true;
                            SpawnDelay = 0;
                        }
                        if (Text == 16)
                        {
                            CantClick = true;
                            BotWave.SetActive(false);
                            SpeechBubble.SetActive(false);
                            ContinueText.SetActive(false);
                            Texts[Text].SetActive(false);
                        }
                        if (Text == 18)
                            CantClick = true;
                    }
                }
            }

            if (Text == 11)
            {
                if(SpawnOnce == false)
                {
                    TutorialSpawner1.GetComponent<EnemySpawning>().gameStarted = true;
                    Time.timeScale = 0;
                    SpawnOnce = true;
                }
            }
            if(Text == 12)
            {
                Lane1.SetActive(true);
                Lane3.SetActive(true);
                Lane4.SetActive(true);
                Time.timeScale = 0;
            }

            if (Text != 14)
                quackenButton.SetActive(false);

            if (Text == 16)
                LookForEnemies = true;
            else
                LookForEnemies = false;
        }
    }

    private void FixedUpdate()
    {
        if(MinigameSceneScript.Tutorial == true)
        {
            if(Text == 13)
            {
                SpawnDelay += Time.deltaTime;

                if(SpawnDelay >= 3 && SpawnOnce == true)
                {
                    TutorialSpawner2.GetComponent<EnemySpawning>().gameStarted = true;
                    SpawnDelay = 0;
                    SpawnOnce = false;
                }

                if(SpawnDelay >= 24.15f)
                {
                    Time.timeScale = 0;
                    CantClick = false;
                    Texts[Text].SetActive(true);
                    ContinueText.SetActive(true);
                    BotWave.SetActive(true);
                    SpeechBubble.SetActive(true);
                    SpawnDelay = 0;
                }
            }

            if(Text == 14)
            {
                SpawnDelay += Time.deltaTime;

                if(SpawnDelay >= 20 && SpawnOnce == false)
                {
                    elitespawner1.GetComponent<EnemySpawning>().gameStarted = true;
                    elitespawner2.GetComponent<EnemySpawning>().gameStarted = true;
                    elitespawner3.GetComponent<EnemySpawning>().gameStarted = true;
                    elitespawner4.GetComponent<EnemySpawning>().gameStarted = true;
                    SpawnOnce = true;
                }
                if(SpawnDelay >= 27)
                {
                    Time.timeScale = 0;
                    CantClick = false;
                    Texts[Text].SetActive(true);
                    BotWave.SetActive(true);
                    SpeechBubble.SetActive(true);
                    quackenButton.SetActive(true);
                    PlayableCards = false;
                    SpawnDelay = 0;
                }
            }
        }
    }


    private void ManaCostDisplayer()
    {

        if (ManaSystem.CurrentMana >= ManaCost1 && isManaCardGlow1)
        {
            if (Smallhand)
                CardButton1S.SetActive(true);
            else if (!Smallhand)
                CardButton1L.SetActive(true);
        }
        else
        {
            CardButton1S.SetActive(false);
            CardButton1L.SetActive(false);
        }

        if (ManaSystem.CurrentMana >= ManaCost2 && isManaCardGlow2)
        {
            if (Smallhand)
                CardButton2S.SetActive(true);
            else if (!Smallhand)
                CardButton2L.SetActive(true);
        }
        else
        {
            CardButton2S.SetActive(false);
            CardButton2L.SetActive(false);
        }

        if (ManaSystem.CurrentMana >= ManaCost3 && isManaCardGlow3)
        {
            if (Smallhand)
                CardButton3S.SetActive(true);
            else if (!Smallhand)
                CardButton3L.SetActive(true);
        }
        else
        {
            CardButton3S.SetActive(false);
            CardButton3L.SetActive(false);
        }

        if (ManaSystem.CurrentMana >= ManaCost4 && isManaCardGlow4)
        {
            if (Smallhand)
                CardButton4S.SetActive(true);
            else if (!Smallhand)
                CardButton4L.SetActive(true);
        }
        else
        {
            CardButton4S.SetActive(false);
            CardButton4L.SetActive(false);
        }

        if (ManaSystem.CurrentMana >= ManaCost5 && isManaCardGlow5)
        {
            if (Smallhand)
                CardButton5S.SetActive(true);
            else if (!Smallhand)
                CardButton5L.SetActive(true);
        }
        else
        {
            CardButton5S.SetActive(false);
            CardButton5L.SetActive(false);
        }

    }

    public void EnlargeButtonPressed()//enlarge your hand
    {
        Smallhand = false;
        enlargeButton.SetActive(false);
        handSmall.SetActive(false);
        minimizeButton.SetActive(true);
        handEnlarged.SetActive(true);
        quackenButton.SetActive(false);
    }

    public void MinimizeButtonPressed()//minimize your hand
    {
        Smallhand = true;
        enlargeButton.SetActive(true);
        handSmall.SetActive(true);
        minimizeButton.SetActive(false);
        handEnlarged.SetActive(false);
        quackenButton.SetActive(true);
    }


    public void PlayCard1()//Play the first card
    {
        MinimizeButtonPressed();

        if(MinigameSceneScript.Tutorial == false)
        {
            if (PlayedCard == card1)
            {
                TowerSpots.SetActive(false);
                PlayedCard = null;
                cardNr = 0;
            }
            else
            {
                TowerSpots.SetActive(true);
                PlayedCard = card1;
                cardNr = 1;
            }
        }
        else//Tutorial settings
        {
            if(PlayableCards == true)
            {
                if (Text == 2)
                {
                    Texts[Text].SetActive(false);
                    Text++;
                    Texts[Text].SetActive(true);
                    manabox.SetActive(true);
                    manabar.SetActive(true);
                    ManaNumber.SetActive(true);
                    ContinueText.SetActive(true);
                }

                PlayedCard = card1;
                cardNr = 1;

                if (Text == 13 || Text == 14)
                    TowerSpots.SetActive(true);

                quackenButton.SetActive(false);
            }
        }
    }
    public void PlayCard2()//Play the second card
    {
        MinimizeButtonPressed();

        if (MinigameSceneScript.Tutorial == false)
        {
            if (PlayedCard == card2)
            {
                TowerSpots.SetActive(false);
                PlayedCard = null;
                cardNr = 0;
            }
            else
            {
                TowerSpots.SetActive(true);
                PlayedCard = card2;
                cardNr = 2;
            }
        }
        else//Tutorial settings
        {
            if (PlayableCards == true)
            {
                if(Text == 2)
                {
                    Texts[Text].SetActive(false);
                    Text++;
                    Texts[Text].SetActive(true);
                    manabox.SetActive(true);
                    manabar.SetActive(true);
                    ManaNumber.SetActive(true);
                    ContinueText.SetActive(true);
                }

                PlayedCard = card2;
                cardNr = 2;

                if (Text == 13 || Text == 14)
                    TowerSpots.SetActive(true);

                quackenButton.SetActive(false);
            }
        }
    }
    public void PlayCard3()//Play the third card
    {
        MinimizeButtonPressed();

        if (MinigameSceneScript.Tutorial == false)
        {
            if (PlayedCard == card3)
            {
                TowerSpots.SetActive(false);
                PlayedCard = null;
                cardNr = 0;
            }
            else
            {
                TowerSpots.SetActive(true);
                PlayedCard = card3;
                cardNr = 3;
            }
        }
        else//Tutorial settings
        {
            if (PlayableCards == true)
            {
                if (Text == 2)
                {
                    Texts[Text].SetActive(false);
                    Text++;
                    Texts[Text].SetActive(true);
                    manabox.SetActive(true);
                    manabar.SetActive(true);
                    ManaNumber.SetActive(true);
                    ContinueText.SetActive(true);
                }

                PlayedCard = card3;
                cardNr = 3;

                if (Text == 13 || Text == 14)
                    TowerSpots.SetActive(true);

                quackenButton.SetActive(false);
            }
        }
    }
    public void PlayCard4()//Play the fourth card
    {
        MinimizeButtonPressed();

        if (MinigameSceneScript.Tutorial == false)
        {
            if (PlayedCard == card4)
            {
                TowerSpots.SetActive(false);
                PlayedCard = null;
                cardNr = 0;
            }
            else
            {
                TowerSpots.SetActive(true);
                PlayedCard = card4;
                cardNr = 4;
            }
        }
        else//Tutorial settings
        {
            if (PlayableCards == true)
            {
                if (Text == 2)
                {
                    Texts[Text].SetActive(false);
                    Text++;
                    Texts[Text].SetActive(true);
                    manabox.SetActive(true);
                    manabar.SetActive(true);
                    ManaNumber.SetActive(true);
                    ContinueText.SetActive(true);
                }

                PlayedCard = card4;
                cardNr = 4;

                if (Text == 13 || Text == 14)
                    TowerSpots.SetActive(true);

                quackenButton.SetActive(false);
            }
        }
    }
    public void PlayCard5()//Play the fifth card
    {
        MinimizeButtonPressed();

        if (MinigameSceneScript.Tutorial == false)
        {
            if (PlayedCard == card5)
            {
                TowerSpots.SetActive(false);
                PlayedCard = null;
                cardNr = 0;
            }
            else
            {
                TowerSpots.SetActive(true);
                PlayedCard = card5;
                cardNr = 5;
            }
        }
        else//Tutorial settings
        {
            if (PlayableCards == true)
            {
                if (Text == 2)
                {
                    Texts[Text].SetActive(false);
                    Text++;
                    Texts[Text].SetActive(true);
                    manabox.SetActive(true);
                    manabar.SetActive(true);
                    ManaNumber.SetActive(true);
                    ContinueText.SetActive(true);
                }
                
                PlayedCard = card5;
                cardNr = 5;

                if (Text == 13 || Text == 14)
                    TowerSpots.SetActive(true);

                quackenButton.SetActive(false);
            }
        }
    }

    private void CardSelectedActivation()
    {
        for (int i = 0; i < 5; i++)
        {
            if(i == cardNr - 1)
            {
                cardActiveIndicaterList[i].SetActive(true);
            }
            else
            {
                cardActiveIndicaterList[i].SetActive(false);
            }
        }
    }

    private void ReadMana()
    {
        CardValues1 = card1.GetComponent<CardDisplayer>();
        ManaCost1 = CardValues1.manaValue;

        CardValues2 = card2.GetComponent<CardDisplayer>();
        ManaCost2 = CardValues2.manaValue;

        CardValues3 = card3.GetComponent<CardDisplayer>();
        ManaCost3 = CardValues3.manaValue;

        CardValues4 = card4.GetComponent<CardDisplayer>();
        ManaCost4 = CardValues4.manaValue;

        CardValues5 = card5.GetComponent<CardDisplayer>();
        ManaCost5 = CardValues5.manaValue;
    }

    public void SetCard()
    {
        for (card = 1; card <= 5; card++)
        {
            CurrentCard = GameObject.Find("LCard" + card);
            CurrentCard.GetComponent<CardDisplayer>().card = GameObject.Find("SCard" + card).GetComponent<CardDisplayer>().card;
            CurrentCard.GetComponent<CardDisplayer>().Read();
        }
    }

    public void ReSetCard()
    {
        Lcard1.GetComponent<CardDisplayer>().card = CardValues1.card;
        Lcard1.GetComponent<CardDisplayer>().Read();

        Lcard2.GetComponent<CardDisplayer>().card = CardValues2.card;
        Lcard2.GetComponent<CardDisplayer>().Read();

        Lcard3.GetComponent<CardDisplayer>().card = CardValues3.card;
        Lcard3.GetComponent<CardDisplayer>().Read();

        Lcard4.GetComponent<CardDisplayer>().card = CardValues4.card;
        Lcard4.GetComponent<CardDisplayer>().Read();

        Lcard5.GetComponent<CardDisplayer>().card = CardValues5.card;
        Lcard5.GetComponent<CardDisplayer>().Read();
    }

    public void Spawn()
    {
        if (Stage == 1)
        {
            SpawnerBeast.GetComponent<EnemySpawning>().gameStarted = true;
            Music1.Play();
        }
        else if (Stage == 2)
        {
            SpawnerHuman.GetComponent<EnemySpawning>().gameStarted = true;
            Music2.Play();
        }
        else if (Stage == 3)
        {
            SpawnerBoss.GetComponent<EnemySpawning>().gameStarted = true;
            Boss1.Play();
            TheCorporatePrefab.GetComponent<TheCorporate>().Activate();
            TheCorporatePrefab.GetComponent<TheCorporate>().IsActive = true;
        }
        else//temporary solution to test boss mechanics
        {
            SpawnerBoss.GetComponent<EnemySpawning>().gameStarted = true;
            Boss1.Play();
            TheCorporatePrefab.GetComponent<TheCorporate>().Activate();
            TheCorporatePrefab.GetComponent<TheCorporate>().IsActive = true;
            BG_Day.SetActive(false);
            BG_Evening.SetActive(false);
            BG_Night.SetActive(true);
        }
    }

    private void AnimationReset()
    {
        print("The bool got turned on");
        if(cardNr == 1)
        {
            StartCoroutine(CardAnimationOn(card1, 1));
            cardNr = 0;
            s_cardWasPlayer = false;
        }
        else if(cardNr == 2)
        {
            StartCoroutine(CardAnimationOn(card2, 2));
            cardNr = 0;
            s_cardWasPlayer = false;
        }
        else if(cardNr == 3)
        {
            StartCoroutine(CardAnimationOn(card3, 3));
            cardNr = 0;
            s_cardWasPlayer = false;
        }
        else if(cardNr == 4)
        {
            StartCoroutine(CardAnimationOn(card4, 4));
            cardNr = 0;
            s_cardWasPlayer = false;
        }
        else if(cardNr == 5)
        {
            StartCoroutine(CardAnimationOn(card5, 5));
            cardNr = 0;
            s_cardWasPlayer = false;
        }
    }

    //Tutorial stuff
    public void PlayedFirstCard()
    {
        if(Text == 5)
        {
            Texts[5].SetActive(false);
            Texts[6].SetActive(true);
            ContinueText.SetActive(true);
            Deck.SetActive(true);

            TS1.SetActive(true);
            TS3.SetActive(true);
            TS4.SetActive(true);
            TowerSpots.SetActive(false);
            Text++;
        }
    }

    public void DeckTutorial()
    {
        if(MinigameSceneScript.Tutorial == true)
        {
            Texts[Text].SetActive(false);
            Text++;
            Texts[Text].SetActive(true);
            ContinueText.SetActive(true);
            DeckHideButton.SetActive(false);
        }
    }

    public void DeckTutorial2()
    {
        if (MinigameSceneScript.Tutorial == true)
        {
            Texts[Text].SetActive(false);
            Text++;
            Texts[Text].SetActive(true);
            ContinueText.SetActive(true);
            DeckCanvas.SetActive(false);
        }
    }

    public void TutorialQuacken()
    {
        Texts[Text].SetActive(false);
        Text++;
        Texts[Text].SetActive(true);
        ContinueText.SetActive(true);
    }

    public void TutorialWin()
    {
        Texts[Text].SetActive(true);
        SpeechBubble.SetActive(true);
        BotWave.SetActive(true);
        ContinueText.SetActive(true);
        CantClick = false;
        LookForEnemies = false;
    }
}