using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardHandScript : MonoBehaviour
{
    public bool Smallhand = true;

    //public GameObject cowwithgunPrefab;
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
    #endregion

    void Start()
    {
        handEnlarged.SetActive(true);
        minimizeButton.SetActive(true);
        TowerSpots.SetActive(false);

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
    public void PlayCard2()//Play the second card
    {
        MinimizeButtonPressed();

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
    public void PlayCard3()//Play the third card
    {
        MinimizeButtonPressed();

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
    public void PlayCard4()//Play the fourth card
    {
        MinimizeButtonPressed();

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
    public void PlayCard5()//Play the fifth card
    {
        MinimizeButtonPressed();

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
}