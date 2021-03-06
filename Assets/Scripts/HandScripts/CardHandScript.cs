using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandScript : MonoBehaviour
{
    public GameObject enlargeButton;//arrow up above your hand to enlarge your hand
    public GameObject minimizeButton;//arrow down visible after enlarging your hand
    public GameObject handSmall;//your hand at the bottom of the screen
    public GameObject handEnlarged;//enlarged hand after pressing the scroll up button

    void Start()
    {
        handEnlarged.SetActive(false);
        minimizeButton.SetActive(false);
    }

    void Update()
    {
        
    }

    public void EnlargeButtonPressed()
    {
        enlargeButton.SetActive(false);
        handSmall.SetActive(false);
        minimizeButton.SetActive(true);
        handEnlarged.SetActive(true);
    }

    public void MinimizeButtonPressed()
    {
        enlargeButton.SetActive(true);
        handSmall.SetActive(true);
        minimizeButton.SetActive(false);
        handEnlarged.SetActive(false);
    }
}