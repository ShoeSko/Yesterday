using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GJcanvas : MonoBehaviour
{
    public GameObject NewCanvas;
    public GameObject ButtonCanvas;
    public GameObject Dimmer;

    public static bool DefeatedCorporate;

    private void Start()
    {
        if (DefeatedCorporate)
        {
            ButtonCanvas.GetComponent<GraphicRaycaster>().enabled = false;
            NewCanvas.SetActive(true);
            Dimmer.SetActive(true);
        }
    }

    public void RestoreUI()
    {
        Dimmer.SetActive(false);
        ButtonCanvas.GetComponent<GraphicRaycaster>().enabled = true;
        NewCanvas.SetActive(false);
        DefeatedCorporate = false;
    }
}
