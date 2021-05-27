using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bestiary : MonoBehaviour
{
    private SaveSystem saving;
    [Header("All base Content")]
    [SerializeField] private List<GameObject> unitBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> beastBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> humanoidBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> monstrosityBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> bossBestiaryList = new List<GameObject>();

    [Header("Unit functions")]
    [SerializeField] private GameObject cardV2;
    [SerializeField] private GameObject cardButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject lastButton;


    [SerializeField] private List<CardScript> unitCardList = new List<CardScript>();
    [SerializeField] private GameObject unitPage2;
    private int firstDiscoveredUnitIndex;
    private int lastDiscoveredUnitIndex;
    private int currentUnitCardIndex;


    private void Awake()
    {
        HideTheLore(); //Hides the lore by turning lore off.
    }

    private void Start()
    {
        InitiateTheBestiary();
        CalculateUnitBestiarySlots();
    }

    private void InitiateTheBestiary()
    {
        if (FindObjectOfType<SaveSystem>())
        {
            saving = FindObjectOfType<SaveSystem>();

            LoadUnitBestiary();
            LoadBeastBestiary();
            LoadHumanoidBestiary();
            LoadMonstrosityBestiary();
            LoadBossBestiary();
        }
    }

    private void LoadUnitBestiary()
    {
        for (int index = 0; index < unitBestiaryList.Count; index++)
        {
            if(saving.data.unitList[index] == true)
            {
                unitBestiaryList[index].GetComponent<Image>().color = Color.white;
                unitBestiaryList[index].GetComponent<Button>().interactable = true;
            }
        }
    }

    private void LoadBeastBestiary()
    {
        for (int index = 0; index < beastBestiaryList.Count; index++)
        {
            if (saving.data.beastList[index] == true)
            {
                beastBestiaryList[index].GetComponent<Image>().color = Color.white;
            }
        }
    }

    private void LoadHumanoidBestiary()
    {
        for (int index = 0; index < humanoidBestiaryList.Count; index++)
        {
            if(saving.data.humanoidList[index] == true)
            {
                humanoidBestiaryList[index].GetComponent<Image>().color = Color.white;
            }
        }
    }

    private void LoadMonstrosityBestiary()
    {
        for (int index = 0; index < monstrosityBestiaryList.Count; index++)
        {
            if (saving.data.monstrosityList[index] == true)
            {
                monstrosityBestiaryList[index].GetComponent<Image>().color = Color.white;
            }
        }
    }

    private void LoadBossBestiary()
    {
        for (int index = 0; index < bossBestiaryList.Count; index++)
        {
            if (saving.data.bossList[index] == true)
            {
                bossBestiaryList[index].SetActive(true);
            }
        }
    }

    private void HideTheLore()
    {
        for (int index = 0; index < unitBestiaryList.Count; index++)
        {
            unitBestiaryList[index].GetComponent<Image>().color = Color.black;
            unitBestiaryList[index].GetComponent<Button>().interactable = false;
        }

        for (int index = 0; index < humanoidBestiaryList.Count; index++)
        {
            humanoidBestiaryList[index].GetComponent<Image>().color = Color.black;
        }

        for (int index = 0; index < monstrosityBestiaryList.Count; index++)
        {
            monstrosityBestiaryList[index].GetComponent<Image>().color = Color.black;
        }

        for (int index = 0; index < beastBestiaryList.Count; index++)
        {
            beastBestiaryList[index].GetComponent<Image>().color = Color.black;
        }

        for (int index = 0; index < bossBestiaryList.Count; index++)
        {
            bossBestiaryList[index].GetComponent<Image>().color = Color.black;
        }
    }

    #region Unit Functions
    private void CalculateUnitBestiarySlots()
    {
        bool firstFound = false;
        for (int index = 0; index < saving.data.unitList.Length; index++)
        {
            if(saving.data.unitList[index] == true)
            {
                if(firstFound == false)
                {
                    firstFound = true;
                    firstDiscoveredUnitIndex = index;
                }
            }
        }

        bool lastFound = false;
        for (int index = saving.data.unitList.Length-1; index >= 0; index--)
        {
            if (saving.data.unitList[index] == true)
            {
                if (lastFound == false)
                {
                    lastFound = true;
                    lastDiscoveredUnitIndex = index;
                }
            }
        }
    }

    public void ReadCardInBestiary(int UnitIndex)
    {
        cardV2.SetActive(true);
        cardButton.SetActive(true);
        nextButton.SetActive(true);
        lastButton.SetActive(true);
        nextButton.GetComponent<Button>().interactable = true;
        lastButton.GetComponent<Button>().interactable = true;
        currentUnitCardIndex = UnitIndex;

        if (firstDiscoveredUnitIndex == lastDiscoveredUnitIndex)
        {
            nextButton.GetComponent<Button>().interactable = false;
            lastButton.GetComponent<Button>().interactable = false;
        }
        else if(currentUnitCardIndex == firstDiscoveredUnitIndex) //Make this value work with actual library
        {
            lastButton.GetComponent<Button>().interactable = false;
            nextButton.GetComponent<Button>().interactable = true;
        }
        else if (currentUnitCardIndex == lastDiscoveredUnitIndex) //Make this value with actual library
        {
            nextButton.GetComponent<Button>().interactable = false;
            lastButton.GetComponent<Button>().interactable = true;
        }

        cardV2.GetComponent<CardDisplayer>().card = unitCardList[currentUnitCardIndex];

        cardV2.GetComponent<CardDisplayer>().Read();

    }

    public void ReadNextCardInBestiary()
    {
        bool indexIsFound = false;
        for (int index = 0; index < saving.data.unitList.Length; index++)
        {
            if (index > currentUnitCardIndex && saving.data.unitList[index] == true && indexIsFound == false)
            {
                indexIsFound = true;
                currentUnitCardIndex = index;
            }
        }
        cardV2.GetComponent<CardDisplayer>().card = unitCardList[currentUnitCardIndex];

        cardV2.GetComponent<CardDisplayer>().Read();

        if (firstDiscoveredUnitIndex == lastDiscoveredUnitIndex)
        {
            nextButton.GetComponent<Button>().interactable = false;
            lastButton.GetComponent<Button>().interactable = false;
        }
        else if (currentUnitCardIndex == firstDiscoveredUnitIndex) //Make this value work with actual library
        {
            lastButton.GetComponent<Button>().interactable = false;
            nextButton.GetComponent<Button>().interactable = true;
        }
        else if (currentUnitCardIndex == lastDiscoveredUnitIndex) //Make this value with actual library
        {
            nextButton.GetComponent<Button>().interactable = false;
            lastButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            nextButton.GetComponent<Button>().interactable = true;
            lastButton.GetComponent<Button>().interactable = true;
        }

        if(currentUnitCardIndex >= 20)
        {
            unitPage2.SetActive(true);
        }
    }

    public void ReadLastCardInBestiary()
    {
        bool indexIsFound = false;
        for (int index = saving.data.unitList.Length-1; index >= 0; index--) //A reverse for loop (This might explode...
        {
            if (index < currentUnitCardIndex && saving.data.unitList[index] == true && indexIsFound == false)
            {
                indexIsFound = true;
                currentUnitCardIndex = index;
            }
        }
        cardV2.GetComponent<CardDisplayer>().card = unitCardList[currentUnitCardIndex];

        cardV2.GetComponent<CardDisplayer>().Read();

        if (firstDiscoveredUnitIndex == lastDiscoveredUnitIndex)
        {
            nextButton.GetComponent<Button>().interactable = false;
            lastButton.GetComponent<Button>().interactable = false;
        }
        else if (currentUnitCardIndex == firstDiscoveredUnitIndex) //Make this value work with actual library
        {
            lastButton.GetComponent<Button>().interactable = false;
            nextButton.GetComponent<Button>().interactable = true;
        }
        else if (currentUnitCardIndex == lastDiscoveredUnitIndex) //Make this value with actual library
        {
            nextButton.GetComponent<Button>().interactable = false;
            lastButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            nextButton.GetComponent<Button>().interactable = true;
            lastButton.GetComponent<Button>().interactable = true;
        }

        if(currentUnitCardIndex <= 19)
        {
            unitPage2.SetActive(false);
        }
    }

    private void Update()
    {
        print("The first card is " + firstDiscoveredUnitIndex + " and the last card is " + lastDiscoveredUnitIndex + ". The current card is " + currentUnitCardIndex);
    }
    #endregion

    #region Cheat
    [ContextMenu("Meet the cheat")]
    private void MeetAllThePeople()
    {
        for (int index = 0; index < saving.data.unitList.Length; index++)
        {
            saving.data.unitList[index] = true;
        }
        for (int inde = 0; inde < saving.data.beastList.Length; inde++)
        {
            saving.data.beastList[inde] = true;
        }

        for (int ind = 0; ind < saving.data.humanoidList.Length; ind++)
        {
            saving.data.humanoidList[ind] = true;
        }

        for (int index = 0; index < saving.data.monstrosityList.Length; index++)
        {
            saving.data.monstrosityList[index] = true;
        }

        for (int index = 0; index < saving.data.bossList.Length; index++)
        {
            saving.data.bossList[index] = true;
        }
        InitiateTheBestiary();

        CalculateUnitBestiarySlots();
    }
    #endregion
}
