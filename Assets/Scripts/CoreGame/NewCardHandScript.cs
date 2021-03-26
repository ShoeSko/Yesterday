using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCardHandScript : MonoBehaviour
{
    public bool Smallhand = true;

    public GameObject cowwithgunPrefab;
    public GameObject PlayedCard;
    public GameObject quackenButton;

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

    void Start()
    {
        handEnlarged.SetActive(false);
        minimizeButton.SetActive(false);
        TowerSpots.SetActive(false);
    }

    void Update()
    {

        if (ManaSystem.CurrentMana >= ManaCost1)
        {
            if(Smallhand)
                CardButton1S.SetActive(true);
            else if(!Smallhand)
                CardButton1L.SetActive(true);
        }
        else
        {
            CardButton1S.SetActive(false);
            CardButton1L.SetActive(false);
        }

        if (ManaSystem.CurrentMana >= ManaCost2)
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

        if (ManaSystem.CurrentMana >= ManaCost3)
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

        if (ManaSystem.CurrentMana >= ManaCost4)
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

        if (ManaSystem.CurrentMana >= ManaCost5)
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

        ReadMana(); //Quick fix to read the mana value
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
        TowerSpots.SetActive(true);
        PlayedCard = card1;
    }
    public void PlayCard2()//Play the second card
    {
        MinimizeButtonPressed();
        TowerSpots.SetActive(true);
        PlayedCard = card2;
    }
    public void PlayCard3()//Play the third card
    {
        MinimizeButtonPressed();
        TowerSpots.SetActive(true);
        PlayedCard = card3;
    }
    public void PlayCard4()//Play the fourth card
    {
        MinimizeButtonPressed();
        TowerSpots.SetActive(true);
        PlayedCard = card4;
    }
    public void PlayCard5()//Play the fifth card
    {
        MinimizeButtonPressed();
        TowerSpots.SetActive(true);
        PlayedCard = card5;
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
}