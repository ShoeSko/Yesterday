using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bestiary : MonoBehaviour
{
    #region Variables
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
    [SerializeField] private GameObject nextButtonUnit;
    [SerializeField] private GameObject lastButtonUnit;


    [SerializeField] private List<CardScript> unitCardList = new List<CardScript>();
    [SerializeField] private GameObject unitPage2;
    private int firstDiscoveredUnitIndex;
    private int lastDiscoveredUnitIndex;
    private int currentUnitCardIndex;

    [Header("Beast functions")]
    [SerializeField] private GameObject backButtonBeasts;
    [SerializeField] private GameObject nextButtonBeasts;
    [SerializeField] private GameObject lastButtonBeasts;

    [SerializeField] private List<GameObject> beastLoreList = new List<GameObject>();
    private int firstDiscovereBeastIndex;
    private int lastDiscoveredBeastIndex;
    private int currentBeastIndex;

    [Header("Humanoid functions")]
    [SerializeField] private GameObject backButtonHumanoid;
    [SerializeField] private GameObject nextButtonHumanoid;
    [SerializeField] private GameObject lastButtonHumanoid;

    [SerializeField] private List<GameObject> humanoidLoreList = new List<GameObject>();
    private int firstDiscovereHumanoidIndex;
    private int lastDiscoveredHumanoidIndex;
    private int currentHumanoidIndex;

    [Header("Monstrosity functions")]
    [SerializeField] private GameObject backButtonMonstrosity;
    [SerializeField] private GameObject nextButtonMonstrosity;
    [SerializeField] private GameObject lastButtonMonstrosity;

    [SerializeField] private List<GameObject> monstrosityLoreList = new List<GameObject>();
    private int firstDiscoveredMonstrosityIndex;
    private int lastDiscoveredMonstrosityIndex;
    private int currentMonstrosityIndex;

    #endregion
    #region Setup Bestiary
    private void Awake()
    {
        HideTheLore(); //Hides the lore by turning lore off.
    }

    private void Start()
    {
        InitiateTheBestiary();
        CalculateUnitBestiarySlots();
        CalculateBeastBestiarySlots();
        CalculateHumanoidBestiarySlots();
        CalculateMonstrosityBestiarySlots();
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
                beastBestiaryList[index].GetComponent<Button>().interactable = true;
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
                humanoidBestiaryList[index].GetComponent<Button>().interactable = true;
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
                monstrosityBestiaryList[index].GetComponent<Button>().interactable = true;
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

        for (int index = 0; index < beastBestiaryList.Count; index++)
        {
            beastBestiaryList[index].GetComponent<Image>().color = Color.black;
            beastBestiaryList[index].GetComponent<Button>().interactable = false;
        }

        for (int index = 0; index < humanoidBestiaryList.Count; index++)
        {
            humanoidBestiaryList[index].GetComponent<Image>().color = Color.black;
            humanoidBestiaryList[index].GetComponent<Button>().interactable = false;
        }

        for (int index = 0; index < monstrosityBestiaryList.Count; index++)
        {
            monstrosityBestiaryList[index].GetComponent<Image>().color = Color.black;
            monstrosityBestiaryList[index].GetComponent<Button>().interactable = false;
        }



        for (int index = 0; index < bossBestiaryList.Count; index++)
        {
            bossBestiaryList[index].GetComponent<Image>().color = Color.black;
        }
    }
    #endregion
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
        nextButtonUnit.SetActive(true);
        lastButtonUnit.SetActive(true);
        nextButtonUnit.GetComponent<Button>().interactable = true;
        lastButtonUnit.GetComponent<Button>().interactable = true;
        currentUnitCardIndex = UnitIndex;

        if (firstDiscoveredUnitIndex == lastDiscoveredUnitIndex)
        {
            nextButtonUnit.GetComponent<Button>().interactable = false;
            lastButtonUnit.GetComponent<Button>().interactable = false;
        }
        else if(currentUnitCardIndex == firstDiscoveredUnitIndex) //Make this value work with actual library
        {
            lastButtonUnit.GetComponent<Button>().interactable = false;
            nextButtonUnit.GetComponent<Button>().interactable = true;
        }
        else if (currentUnitCardIndex == lastDiscoveredUnitIndex) //Make this value with actual library
        {
            nextButtonUnit.GetComponent<Button>().interactable = false;
            lastButtonUnit.GetComponent<Button>().interactable = true;
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
            nextButtonUnit.GetComponent<Button>().interactable = false;
            lastButtonUnit.GetComponent<Button>().interactable = false;
        }
        else if (currentUnitCardIndex == firstDiscoveredUnitIndex) //Make this value work with actual library
        {
            lastButtonUnit.GetComponent<Button>().interactable = false;
            nextButtonUnit.GetComponent<Button>().interactable = true;
        }
        else if (currentUnitCardIndex == lastDiscoveredUnitIndex) //Make this value with actual library
        {
            nextButtonUnit.GetComponent<Button>().interactable = false;
            lastButtonUnit.GetComponent<Button>().interactable = true;
        }
        else
        {
            nextButtonUnit.GetComponent<Button>().interactable = true;
            lastButtonUnit.GetComponent<Button>().interactable = true;
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
            nextButtonUnit.GetComponent<Button>().interactable = false;
            lastButtonUnit.GetComponent<Button>().interactable = false;
        }
        else if (currentUnitCardIndex == firstDiscoveredUnitIndex) //Make this value work with actual library
        {
            lastButtonUnit.GetComponent<Button>().interactable = false;
            nextButtonUnit.GetComponent<Button>().interactable = true;
        }
        else if (currentUnitCardIndex == lastDiscoveredUnitIndex) //Make this value with actual library
        {
            nextButtonUnit.GetComponent<Button>().interactable = false;
            lastButtonUnit.GetComponent<Button>().interactable = true;
        }
        else
        {
            nextButtonUnit.GetComponent<Button>().interactable = true;
            lastButtonUnit.GetComponent<Button>().interactable = true;
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
    #region Beast Functions
    private void CalculateBeastBestiarySlots()
    {
        bool firstFound = false;
        for (int index = 0; index < saving.data.beastList.Length; index++)
        {
            if (saving.data.beastList[index] == true)
            {
                if (firstFound == false)
                {
                    firstFound = true;
                    firstDiscovereBeastIndex = index;
                }
            }
        }

        bool lastFound = false;
        for (int index = saving.data.beastList.Length - 1; index >= 0; index--)
        {
            if (saving.data.beastList[index] == true)
            {
                if (lastFound == false)
                {
                    lastFound = true;
                    lastDiscoveredBeastIndex = index;
                }
            }
        }
    }

    #endregion
    #region Humanoid Functions

    private void CalculateHumanoidBestiarySlots()
    {
        bool firstFound = false;
        for (int index = 0; index < saving.data.humanoidList.Length; index++)
        {
            if (saving.data.humanoidList[index] == true)
            {
                if (firstFound == false)
                {
                    firstFound = true;
                    firstDiscovereHumanoidIndex = index;
                }
            }
        }

        bool lastFound = false;
        for (int index = saving.data.humanoidList.Length - 1; index >= 0; index--)
        {
            if (saving.data.humanoidList[index] == true)
            {
                if (lastFound == false)
                {
                    lastFound = true;
                    lastDiscoveredHumanoidIndex = index;
                }
            }
        }
    }

    #endregion
    #region Monstrosity Functions

    private void CalculateMonstrosityBestiarySlots()
    {
        bool firstFound = false;
        for (int index = 0; index < saving.data.monstrosityList.Length; index++)
        {
            if (saving.data.monstrosityList[index] == true)
            {
                if (firstFound == false)
                {
                    firstFound = true;
                    firstDiscoveredMonstrosityIndex = index;
                }
            }
        }

        bool lastFound = false;
        for (int index = saving.data.monstrosityList.Length - 1; index >= 0; index--)
        {
            if (saving.data.monstrosityList[index] == true)
            {
                if (lastFound == false)
                {
                    lastFound = true;
                    lastDiscoveredMonstrosityIndex = index;
                }
            }
        }
    }

    #endregion
    #region Boss Functions

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
