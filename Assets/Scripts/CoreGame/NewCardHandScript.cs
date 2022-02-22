using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardHandScript : MonoBehaviour
{
    #region Variables
    [Header("Boss Testing")]
    public bool BossTesting;
    [Range(1,3)] public int whichBoss;

    public bool DevStageTest;
    [Range(1,3)] public int whichStage;

    [Header("Base Variables")]
    public bool Smallhand = true;

    public GameObject PlayedCard;
    public GameObject quackenButton;
    public GameObject Deck;
    private GameObject CurrentCard;
    public GameObject TheCorporatePrefab;
    public GameObject TheGuardianPrefab;
    public GameObject TheCorruptionPrefab;
    public GameObject BossIntro;
    public GameObject DeckButton;

    public bool IWon;

    private int card;
    public int RandomBoss;
    public static bool isCampaign;

    [Header("Spawners")]
    public static int Stage;//Which stage is the player on? (Will define which enemies spawn)
    public GameObject SpawnerHuman;//Spawner for Businessmen
    public GameObject SpawnerBeast;//Spawner for Beasts
    public GameObject SpawnerBossCorporate;//Spawner for Boss
    public GameObject SpawnerBossGuardian;//Spawner for Boss
    public GameObject SpawnerBossCorruption;//Spawner for Boss

    [Header("AUDIO")]
    public AudioSource Music1;
    public AudioSource Music2;
    public AudioSource Boss1;
    public AudioSource Boss2;
    public AudioSource Boss3;
    public AudioSource CurrentBGM;

    [Header("Backgrounds")]
    public GameObject BG_Day;
    public GameObject BG_Evening;
    public GameObject BG_Night;

    [Header("Hands & TowerSpots")]
    public GameObject enlargeButton;//arrow up above your hand to enlarge your hand
    public GameObject minimizeButton;//arrow down visible after enlarging your hand
    public GameObject handSmall;//your hand at the bottom of the screen
    public GameObject handEnlarged;//enlarged hand after pressing the scroll up button
    public GameObject TowerSpots;

    [Header("Small Card Buttons")]
    public GameObject CardButton1S;
    public GameObject CardButton2S;
    public GameObject CardButton3S;
    public GameObject CardButton4S;
    public GameObject CardButton5S;

    [Header("Large Card Buttons")]
    public GameObject CardButton1L;
    public GameObject CardButton2L;
    public GameObject CardButton3L;
    public GameObject CardButton4L;
    public GameObject CardButton5L;

    [Header("Mana Values")]
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

    [Header("Large Card Object")]
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

    [HideInInspector] public bool Card1Corrupted;
    [HideInInspector] public bool Card2Corrupted;
    [HideInInspector] public bool Card3Corrupted;
    [HideInInspector] public bool Card4Corrupted;
    [HideInInspector] public bool Card5Corrupted;

    public List<GameObject> cardActiveIndicaterList = new List<GameObject>();
    #endregion

    //Additioanl objects & variables used for tutorial
    [Header("Tutorial Variables")]
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
    private bool LostTutorial;
    private bool QuackenWorks;
    private int DialogueChange = 0;
    private bool ONCE = true;

    public bool CantPlayCards;

    public List<AudioSource> VoiceDialogue = new List<AudioSource>();
    public List<AudioSource> VoiceWin = new List<AudioSource>();
    public List<AudioSource> VoiceLose = new List<AudioSource>();
    public List<AudioSource> VoiceSurprised = new List<AudioSource>();
    #endregion
    #region Setup
    void Start()
    {
        IWon = false;

        if (DevStageTest == true)
            Stage = whichStage;

        CoreGameSetup(); //All the setup for the core game

        //Tutorial settings
        TutorialSetup();
    }
    private void CoreGameSetup()
    {
        handEnlarged.SetActive(true);
        minimizeButton.SetActive(true);
        TowerSpots.SetActive(false);

        if (Stage == 1)
        {
            BG_Day.SetActive(true);
            BG_Evening.SetActive(false);
            BG_Night.SetActive(false);
        }
        else if (Stage == 2)
        {
            BG_Day.SetActive(false);
            BG_Evening.SetActive(true);
            BG_Night.SetActive(false);
        }
        else if (Stage == 3)
        {
            BG_Day.SetActive(false);
            BG_Evening.SetActive(false);
            BG_Night.SetActive(true);

            if (BossTesting)
            {
                RandomBoss = whichBoss;//Simple boss randomiser
                print("boss testing is on????");
            }
            else if (isCampaign)
            {
                print("the campaign is on");
                if (FindObjectOfType<SaveSystem>())
                {
                    print("And the save system exists");
                    SaveSystem saving = FindObjectOfType<SaveSystem>();
                    for (int currentBoss = 2; currentBoss >= 0; currentBoss--)//This for loop crashes the game for some reason
                    {
                        if (saving.data.bossList[currentBoss] == false)
                        {
                            RandomBoss = currentBoss++;
                            Debug.Log("Random boss is" + RandomBoss);
                        }
                    }
                }
            }
            else
            {
                RandomBoss = Random.Range(1, 3);
            }

            BossIntro.GetComponent<Corporate_Intro>().PlayIntro();
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
    }

    private void TutorialSetup()
    {
        if (MinigameSceneScript.Tutorial == true)
        {
            TutorialSurprisedVoice();

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
    #endregion


    void Update()
    {
        if (CantPlayCards)
            enlargeButton.SetActive(false);
        else
            enlargeButton.SetActive(true);

        if (!IWon)
        {
            ManaCostDisplayer();
        }
        else
        {
            quackenButton.SetActive(false);
            DeckButton.SetActive(false);
        }

        ReadMana(); //Quick fix to read the mana value

        if(s_cardWasPlayer == true) { AnimationReset(); }
        CardSelectedActivation(); //Marks the current selected card in green.


        //Tutorial stuff
        RunningTutorial();
    }
    #region Tutorial Parts
    private void RunningTutorial()
    {
        if (MinigameSceneScript.Tutorial == true)
        {
            ActivateDialogue();

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
                if (SpawnOnce == false)
                {
                    TutorialSurprisedVoice();
                    TutorialSpawner1.GetComponent<EnemySpawning>().gameStarted = true;
                    Time.timeScale = 0;
                    SpawnOnce = true;
                }
            }
            if (Text == 12)
            {
                Lane1.SetActive(true);
                Lane3.SetActive(true);
                Lane4.SetActive(true);
                Time.timeScale = 0;
            }

            if (Text == 16)
                LookForEnemies = true;
            else
                LookForEnemies = false;
        }

        if (LostTutorial == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
            {
                Texts[19].SetActive(false);
                SpeechBubble.SetActive(false);
                BotWave.SetActive(false);
                ContinueText.SetActive(false);
                Time.timeScale = 1;
                LostTutorial = false;
            }
        }
    }

    private void FixedUpdate()
    {
        TutorialSpawning();
    }
    private void TutorialSpawning()
    {
        if (MinigameSceneScript.Tutorial == true)
        {
            if (Text == 13)
            {
                SpawnDelay += Time.deltaTime;

                if (SpawnDelay >= 3 && SpawnOnce == true)
                {
                    TutorialSpawner2.GetComponent<EnemySpawning>().gameStarted = true;
                    SpawnDelay = 0;
                    SpawnOnce = false;
                }

                if (SpawnDelay >= 24.15f)
                {
                    TutorialSurprisedVoice();
                    Time.timeScale = 0;
                    CantClick = false;
                    Texts[Text].SetActive(true);
                    ContinueText.SetActive(true);
                    BotWave.SetActive(true);
                    SpeechBubble.SetActive(true);
                    SpawnDelay = 0;
                }
            }

            if (Text == 14)
            {
                SpawnDelay += Time.deltaTime;

                if (SpawnDelay >= 20 && SpawnOnce == false)
                {
                    MinimizeButtonPressed(); //Makes sure the player can see that a lot of enemies spawn.

                    elitespawner1.GetComponent<EnemySpawning>().gameStarted = true;
                    elitespawner2.GetComponent<EnemySpawning>().gameStarted = true;
                    elitespawner3.GetComponent<EnemySpawning>().gameStarted = true;
                    elitespawner4.GetComponent<EnemySpawning>().gameStarted = true;
                    SpawnOnce = true;
                }
                if (SpawnDelay >= 27)
                {
                    TutorialSurprisedVoice();
                    Time.timeScale = 0;
                    CantClick = false;
                    Texts[Text].SetActive(true);
                    BotWave.SetActive(true);
                    SpeechBubble.SetActive(true);
                    QuackenWorks = true;
                    //quackenButton.SetActive(true);
                    PlayableCards = false;
                    SpawnDelay = 0;
                }
            }

            //maybe fix??
            if (QuackenWorks == false)
                quackenButton.SetActive(false);
            else
                quackenButton.SetActive(true);
        }
    }
    #endregion
    #region ManaDisplay & Enlarge/Reduce buttons
    private void ManaCostDisplayer()
    {
        if (ManaSystem.CurrentMana >= ManaCost1 && isManaCardGlow1 && !Card1Corrupted && !CantPlayCards)
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

        if (ManaSystem.CurrentMana >= ManaCost2 && isManaCardGlow2 && !Card2Corrupted && !CantPlayCards)
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

        if (ManaSystem.CurrentMana >= ManaCost3 && isManaCardGlow3 && !Card3Corrupted && !CantPlayCards)
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

        if (ManaSystem.CurrentMana >= ManaCost4 && isManaCardGlow4 && !Card4Corrupted && !CantPlayCards)
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

        if (ManaSystem.CurrentMana >= ManaCost5 && isManaCardGlow5 && !Card5Corrupted && !CantPlayCards)
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
        TowerSpots.SetActive(false);
        PlayedCard = null; //Resets the cards when pulling up the big one
        cardNr = 0;
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
    #endregion
    #region Play Card
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
    #endregion
    #region Card Selection functions
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
        if (CardValues1.CantBeRead == false)
            Lcard1.GetComponent<CardDisplayer>().CantBeRead = false;
        Lcard1.GetComponent<CardDisplayer>().card = CardValues1.card;
        Lcard1.GetComponent<CardDisplayer>().Read();

        if (CardValues2.CantBeRead == false)
            Lcard2.GetComponent<CardDisplayer>().CantBeRead = false;
        Lcard2.GetComponent<CardDisplayer>().card = CardValues2.card;
        Lcard2.GetComponent<CardDisplayer>().Read();

        if (CardValues3.CantBeRead == false)
            Lcard3.GetComponent<CardDisplayer>().CantBeRead = false;
        Lcard3.GetComponent<CardDisplayer>().card = CardValues3.card;
        Lcard3.GetComponent<CardDisplayer>().Read();

        if (CardValues4.CantBeRead == false)
            Lcard4.GetComponent<CardDisplayer>().CantBeRead = false;
        Lcard4.GetComponent<CardDisplayer>().card = CardValues4.card;
        Lcard4.GetComponent<CardDisplayer>().Read();

        if (CardValues5.CantBeRead == false)
            Lcard5.GetComponent<CardDisplayer>().CantBeRead = false;
        Lcard5.GetComponent<CardDisplayer>().card = CardValues5.card;
        Lcard5.GetComponent<CardDisplayer>().Read();
    }
    #endregion
    public void Spawn()
    {
        if (Stage == 1)
        {
            SpawnerBeast.GetComponent<EnemySpawning>().gameStarted = true;
            CurrentBGM = Music1;
            CurrentBGM.Play();
        }
        else if (Stage == 2)
        {
            SpawnerHuman.GetComponent<EnemySpawning>().gameStarted = true;
            CurrentBGM = Music2;
            CurrentBGM.Play();
        }
        else if (Stage == 3)
        {
            if (BossTesting == true)
                RandomBoss = whichBoss;//Set this number to the boss you want to test // Added a variable that can be accesed from outside the script. More usefull for testing.

            #region Boos Meet n Greet
            if (!BossTesting)
            {
                if (FindObjectOfType<SaveSystem>())
                {
                    SaveSystem saving = FindObjectOfType<SaveSystem>();
                    saving.data.bossMeetList[RandomBoss--] = true;
                }
            }
            #endregion

            if (RandomBoss == 1)
            {
                //Do first boss stuff
                SpawnerBossCorporate.GetComponent<EnemySpawning>().gameStarted = true;
                CurrentBGM = Boss1;
                CurrentBGM.Play();
                TheCorporatePrefab.GetComponent<TheCorporate>().Activate();
                TheCorporatePrefab.GetComponent<TheCorporate>().IsActive = true;
            }
            else if(RandomBoss == 2)
            {
                //Do second boss stuff
                SpawnerBossGuardian.GetComponent<EnemySpawning>().gameStarted = true;
                CurrentBGM = Boss2;
                CurrentBGM.Play();
                TheGuardianPrefab.GetComponent<TheGuardian>().Activate();
                TheGuardianPrefab.GetComponent<TheGuardian>().IsActive = true;
            }
            else if(RandomBoss == 3)
            {
                //Do third boss stuff
                SpawnerBossCorruption.GetComponent<EnemySpawning>().gameStarted = true;
                CurrentBGM = Boss3;
                CurrentBGM.Play();
                TheCorruptionPrefab.GetComponent<TheCorruption>().Activate();
                TheCorruptionPrefab.GetComponent<TheCorruption>().IsActive = true;
            }
        }
    }

    public void ReRollHand()
    {
        for (card = 1; card <= 5; card++)
        {
            Deck.GetComponent<DeckScript>().Randomise();

            CurrentCard = GameObject.Find("SCard" + card);
            CurrentCard.GetComponent<CardDisplayer>().CantBeRead = false;
            CurrentCard.GetComponent<CardDisplayer>().card = Deck.GetComponent<DeckScript>().activecard;
            CurrentCard.GetComponent<CardDisplayer>().Read();

            ReSetCard();

            StartCoroutine(CardAnimationOn(CurrentCard, card));//Made to do the animation on start.
        }
    }

    public void CreateFindMaster()
    {
        handEnlarged.SetActive(true);

        int RandomCardInHand = Random.Range(1, 6);
        GameObject SCard = GameObject.Find("SCard" + RandomCardInHand);
        GameObject LCard = GameObject.Find("LCard" + RandomCardInHand);

        SCard.GetComponent<CardDisplayer>().BecomeFindMaster();
        LCard.GetComponent<CardDisplayer>().BecomeFindMaster();

        handEnlarged.SetActive(false);
    }
    #region Animation Controlls
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
    IEnumerator CardAnimationOn(GameObject animCard, int nr) //Initiates the card draw animation
    {
        animCard.GetComponent<Animator>().SetBool("CardUsed " + nr, true); //Turns on the bool that releases the animation
        yield return new WaitForSeconds(0.5f);
        animCard.GetComponent<Animator>().SetBool("CardUsed " + nr, false); //Turns of the bool after 1 second to prevent a loop of animation
    }
    #endregion
    #region Tutorial Public Voids
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
            ////DeckHideButton.SetActive(false);
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
        QuackenWorks = false;
    }

    public void TutorialWin()
    {
        TriggerVoiceOnce();
        Texts[Text].SetActive(true);
        SpeechBubble.SetActive(true);
        BotWave.SetActive(true);
        ContinueText.SetActive(true);
        CantClick = false;
        LookForEnemies = false;
    }

    public void TutorialLose()
    {
        TutorialLoseVoice();
        Texts[19].SetActive(true);
        SpeechBubble.SetActive(true);
        BotWave.SetActive(true);
        ContinueText.SetActive(true);
        LostTutorial = true;
        Time.timeScale = 0;
    }

    private void TutorialDialogueVoice()
    {
        int sound = VoiceDialogue.Count;

        AudioSource PlaySound = VoiceDialogue[Random.Range(0, sound)];

        VoiceDialogue.Remove(PlaySound);
        PlaySound.Play();
    }

    private void TutorialWinVoice()
    {
        int sound = VoiceWin.Count;

        AudioSource PlaySound = VoiceWin[Random.Range(0, sound)];

        VoiceWin.Remove(PlaySound);
        PlaySound.Play();
    }

    private void TutorialLoseVoice()
    {
        int sound = VoiceLose.Count;

        AudioSource PlaySound = VoiceLose[Random.Range(0, sound)];

        VoiceLose.Remove(PlaySound);
        PlaySound.Play();
    }

    private void TutorialSurprisedVoice()
    {
        int sound = VoiceSurprised.Count;

        AudioSource PlaySound = VoiceSurprised[Random.Range(0, sound)];

        VoiceSurprised.Remove(PlaySound);
        PlaySound.Play();
    }

    private void ActivateDialogue()
    {
        if (DialogueChange != Text)
        {
            if(Text <= 10 && Text >= 1)
            {
                TutorialDialogueVoice();
                DialogueChange = Text;
            }
            else if (Text == 12 || Text == 15 || Text == 17)
            {
                TutorialDialogueVoice();
                DialogueChange = Text;
            }
            else if(Text == 18)
            {
                TutorialWinVoice();
                DialogueChange = Text;
            }
        }
    }

    private void TriggerVoiceOnce()
    {
        if (ONCE)
        {
            TutorialWinVoice();
            ONCE = false;
        }
    }
    #endregion
}