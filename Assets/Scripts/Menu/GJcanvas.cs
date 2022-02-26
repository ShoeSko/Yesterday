using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GJcanvas : MonoBehaviour
{
    public GameObject NewCanvasCorporate;
    public GameObject NewCanvasCorruption;
    public GameObject ButtonCanvas;
    public GameObject Dimmer;

    public static bool DefeatedCorporate;
    public static bool DefeatedCorruption;

    private void Start()
    {
        if (DefeatedCorporate)
        {
            ButtonCanvas.GetComponent<GraphicRaycaster>().enabled = false;
            NewCanvasCorporate.SetActive(true);
            Dimmer.SetActive(true);
        }

        if (DefeatedCorruption)
        {
            MinigameSceneScript.HasFinishedCampaign = true;
            ButtonCanvas.GetComponent<GraphicRaycaster>().enabled = false;
            NewCanvasCorruption.SetActive(true);
            Dimmer.SetActive(true);
        }
    }

    public void RestoreUI()
    {
        Dimmer.SetActive(false);
        ButtonCanvas.GetComponent<GraphicRaycaster>().enabled = true;

        if (DefeatedCorporate)
        {
            NewCanvasCorporate.SetActive(false);
            DefeatedCorporate = false;
        }
        else
        {
            NewCanvasCorruption.SetActive(false);
            DefeatedCorruption = false;
        }
    }
}
