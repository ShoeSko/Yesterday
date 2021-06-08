using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRewardButtonShowHide : MonoBehaviour
{
    [Header("Option 1")]
    [SerializeField] private GameObject chooseButton1;
    [SerializeField] private GameObject chooseButtonBackground1;
    [SerializeField] private GameObject viewButton1;
    [SerializeField] private GameObject viewButtonBackground1;
    [Header("Option 2")]
    [SerializeField] private GameObject chooseButton2;
    [SerializeField] private GameObject chooseButtonBackground2;
    [SerializeField] private GameObject viewButton2;
    [SerializeField] private GameObject viewButtonBackground2;
    [Header("Option 3")]
    [SerializeField] private GameObject chooseButton3;
    [SerializeField] private GameObject chooseButtonBackground3;
    [SerializeField] private GameObject viewButton3;
    [SerializeField] private GameObject viewButtonBackground3;

    private void Update()
    {
        Option1OnAndOff();
        Option2OnAndOff();
        Option3OnAndOff();
    }

    private void Option1OnAndOff()
    {
        if (chooseButton1.activeInHierarchy)
        {
            chooseButtonBackground1.SetActive(true);
        }
        else
        {
            chooseButtonBackground1.SetActive(false);
        }

        if (viewButton1.activeInHierarchy)
        {
            viewButtonBackground1.SetActive(true);
        }
        else
        {
            viewButtonBackground1.SetActive(false);
        }
    }

    private void Option2OnAndOff()
    {
        if (chooseButton2.activeInHierarchy)
        {
            chooseButtonBackground2.SetActive(true);
        }
        else
        {
            chooseButtonBackground2.SetActive(false);
        }

        if (viewButton2.activeInHierarchy)
        {
            viewButtonBackground2.SetActive(true);
        }
        else
        {
            viewButtonBackground2.SetActive(false);
        }
    }

    private void Option3OnAndOff()
    {
        if (chooseButton3.activeInHierarchy)
        {
            chooseButtonBackground3.SetActive(true);
        }
        else
        {
            chooseButtonBackground3.SetActive(false);
        }

        if (viewButton3.activeInHierarchy)
        {
            viewButtonBackground3.SetActive(true);
        }
        else
        {
            viewButtonBackground3.SetActive(false);
        }
    }
}
