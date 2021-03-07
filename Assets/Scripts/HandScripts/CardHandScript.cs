using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandScript : MonoBehaviour
{
    private bool Smallhand = true;

    public GameObject cowwithgunPrefab;

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


    void Start()
    {
        handEnlarged.SetActive(false);
        minimizeButton.SetActive(false);
        TowerSpots.SetActive(false);
    }

    void Update()
    {
        if(ManaSystem.CurrentMana >= 2)//all of this is temporary solution for the prototype
        {
            if (Smallhand)
            {
                CardButton1S.SetActive(true);
                CardButton2S.SetActive(true);
                CardButton3S.SetActive(true);
                CardButton4S.SetActive(true);
                CardButton5S.SetActive(true);
            }
            else if (!Smallhand)
            {
                CardButton1L.SetActive(true);
                CardButton2L.SetActive(true);
                CardButton3L.SetActive(true);
                CardButton4L.SetActive(true);
                CardButton5L.SetActive(true);
            }
        }
        else
        {
            CardButton1S.SetActive(false);
            CardButton2S.SetActive(false);
            CardButton3S.SetActive(false);
            CardButton4S.SetActive(false);
            CardButton5S.SetActive(false);
            CardButton1L.SetActive(false);
            CardButton2L.SetActive(false);
            CardButton3L.SetActive(false);
            CardButton4L.SetActive(false);
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
    }

    public void MinimizeButtonPressed()//minimize your hand
    {
        Smallhand = true;
        enlargeButton.SetActive(true);
        handSmall.SetActive(true);
        minimizeButton.SetActive(false);
        handEnlarged.SetActive(false);
    }

    public void PlayCard()//this happens when you click on a playable card
    {
        TowerSpots.SetActive(true);
        handEnlarged.SetActive(false);
        enlargeButton.SetActive(true);
        handSmall.SetActive(true);
        minimizeButton.SetActive(false);
    }
}