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
    [SerializeField] private GameObject leaveUnitPageButton;
    [SerializeField] private GameObject unitPage1Button;
    [SerializeField] private GameObject unitPage2Button;


    [SerializeField] private List<CardScript> unitCardList = new List<CardScript>();
    [SerializeField] private GameObject unitPage2;
    private int firstDiscoveredUnitIndex;
    private int lastDiscoveredUnitIndex;
    private int currentUnitCardIndex;

    [Header("Beast functions")]
    [SerializeField] private GameObject backButtonBeasts;
    [SerializeField] private GameObject nextButtonBeasts;
    [SerializeField] private GameObject lastButtonBeasts;
    [SerializeField] private GameObject leaveBeastPageButton;

    [SerializeField] private List<GameObject> beastLoreList = new List<GameObject>();
    private int firstDiscoveredBeastIndex;
    private int lastDiscoveredBeastIndex;
    private int currentBeastIndex;

    [Header("Humanoid functions")]
    [SerializeField] private GameObject backButtonHumanoid;
    [SerializeField] private GameObject nextButtonHumanoid;
    [SerializeField] private GameObject lastButtonHumanoid;
    [SerializeField] private GameObject leaveHumanoidPageButton;

    [SerializeField] private List<GameObject> humanoidLoreList = new List<GameObject>();
    private int firstDiscoveredHumanoidIndex;
    private int lastDiscoveredHumanoidIndex;
    private int currentHumanoidIndex;

    [Header("Monstrosity functions")]
    [SerializeField] private GameObject backButtonMonstrosity;
    [SerializeField] private GameObject nextButtonMonstrosity;
    [SerializeField] private GameObject lastButtonMonstrosity;
    [SerializeField] private GameObject leaveMonstrosityPageButton;

    [SerializeField] private List<GameObject> monstrosityLoreList = new List<GameObject>();
    private int firstDiscoveredMonstrosityIndex;
    private int lastDiscoveredMonstrosityIndex;
    private int currentMonstrosityIndex;

    [Header("Boss Functions")]
    [SerializeField] private List<GameObject> boss1ContentList = new List<GameObject>();
    [SerializeField] private List<GameObject> boss2ContentList = new List<GameObject>();

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

        //Debugg
        //BestiaryDebugIndex();
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

    private void BestiaryDebugIndex()
    {
        Debug.Log("The first card is " + firstDiscoveredUnitIndex + " and the last card is " + lastDiscoveredUnitIndex + ". The current card is " + currentUnitCardIndex);
        Debug.Log("The first beast is " + firstDiscoveredBeastIndex + " and the last beast is " + lastDiscoveredBeastIndex + ". The current beast is " + currentBeastIndex);
        Debug.Log("The first humanoid is " + firstDiscoveredHumanoidIndex + " and the last humanoid is " + lastDiscoveredHumanoidIndex + ". The current humanoid is " + currentHumanoidIndex);
        Debug.Log("The first monstrosity is " + firstDiscoveredMonstrosityIndex + " and the last monstrosity is " + lastDiscoveredMonstrosityIndex + ". The current monstrosity is " + currentMonstrosityIndex);
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
                bossBestiaryList[index].GetComponent<Image>().color = Color.white;
                bossBestiaryList[index].GetComponent<Button>().interactable = true;
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
            bossBestiaryList[index].GetComponent<Button>().interactable = false;
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
        leaveUnitPageButton.SetActive(false);
        unitPage1Button.SetActive(false);
        unitPage2Button.SetActive(false);
        nextButtonUnit.GetComponent<Button>().interactable = true;
        lastButtonUnit.GetComponent<Button>().interactable = true;
        currentUnitCardIndex = UnitIndex;

        UnitButtonInteraction();

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

        UnitButtonInteraction();

        //Flip Page
        if (currentUnitCardIndex >= 20)
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

        UnitButtonInteraction();

        //Flip Page
        if(currentUnitCardIndex <= 19)
        {
            unitPage2.SetActive(false);
        }
    }

    private void UnitButtonInteraction()
    {
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
                    firstDiscoveredBeastIndex = index;
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

    public void ReadBeastLoreInBestiary(int beastIndex)
    {
        backButtonBeasts.SetActive(true);
        nextButtonBeasts.SetActive(true);
        lastButtonBeasts.SetActive(true);
        leaveBeastPageButton.SetActive(false);
        currentBeastIndex = beastIndex;

        BeastButtonInteraction();

        beastLoreList[currentBeastIndex].SetActive(true);
    }

    public void ReadNextBeastLoreInBestiary()
    {
        beastLoreList[currentBeastIndex].SetActive(false); //Turns of the last Lore

        bool indexIsFound = false;
        for (int index = 0; index < saving.data.beastList.Length; index++)
        {
            if (index > currentBeastIndex && saving.data.beastList[index] == true && indexIsFound == false) //Finds the next lore that has been unclocked.
            {
                indexIsFound = true;
                currentBeastIndex = index;
            }
        }

        beastLoreList[currentBeastIndex].SetActive(true); //Turns on the next Lore

        BeastButtonInteraction();

    }

    public void ReadLastBeastLoreInBestiary()
    {
        beastLoreList[currentBeastIndex].SetActive(false);

        bool indexIsFound = false;
        for (int index = saving.data.beastList.Length - 1; index >= 0; index--) //A reverse for loop (This might explode...
        {
            if (index < currentBeastIndex && saving.data.beastList[index] == true && indexIsFound == false)
            {
                indexIsFound = true;
                currentBeastIndex = index;
            }
        }

        beastLoreList[currentBeastIndex].SetActive(true);

        BeastButtonInteraction();
    }

    private void BeastButtonInteraction()
    {
        if (firstDiscoveredBeastIndex == lastDiscoveredBeastIndex)
        {
            nextButtonBeasts.GetComponent<Button>().interactable = false;
            lastButtonBeasts.GetComponent<Button>().interactable = false;
        }
        else if (currentBeastIndex == lastDiscoveredBeastIndex)
        {
            nextButtonBeasts.GetComponent<Button>().interactable = false;
            lastButtonBeasts.GetComponent<Button>().interactable = true;
        }
        else if (currentBeastIndex == firstDiscoveredBeastIndex)
        {
            nextButtonBeasts.GetComponent<Button>().interactable = true;
            lastButtonBeasts.GetComponent<Button>().interactable = false;
        }
        else
        {
            nextButtonBeasts.GetComponent<Button>().interactable = true;
            lastButtonBeasts.GetComponent<Button>().interactable = true;
        }
    }

    /// <summary>
    /// I turn off all the lore for beasts when leaving beast screen.
    /// </summary>
    public void BeastLoreCleanup()
    {
        for (int index = 0; index < beastLoreList.Count; index++)
        {
            beastLoreList[index].SetActive(false);
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
                    firstDiscoveredHumanoidIndex = index;
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

    public void ReadHumanoidLoreInBestiary(int humanoidIndex)
    {
        backButtonHumanoid.SetActive(true);
        nextButtonHumanoid.SetActive(true);
        lastButtonHumanoid.SetActive(true);
        leaveHumanoidPageButton.SetActive(false);
        currentHumanoidIndex = humanoidIndex;

        HumanoidButtonInteraction();

        humanoidLoreList[currentHumanoidIndex].SetActive(true);
    }

    public void ReadNextHumanoidLoreInBestiary()
    {
        humanoidLoreList[currentHumanoidIndex].SetActive(false); //Turns of the last Lore

        bool indexIsFound = false;
        for (int index = 0; index < saving.data.humanoidList.Length; index++)
        {
            if (index > currentHumanoidIndex && saving.data.humanoidList[index] == true && indexIsFound == false) //Finds the next lore that has been unclocked.
            {
                indexIsFound = true;
                currentHumanoidIndex = index;
            }
        }

        humanoidLoreList[currentHumanoidIndex].SetActive(true); //Turns on the next Lore

        HumanoidButtonInteraction();

    }

    public void ReadLastHumanoidLoreInBestiary()
    {
       humanoidLoreList[currentHumanoidIndex].SetActive(false);

        bool indexIsFound = false;
        for (int index = saving.data.humanoidList.Length - 1; index >= 0; index--) //A reverse for loop (This might explode...
        {
            if (index < currentHumanoidIndex && saving.data.humanoidList[index] == true && indexIsFound == false)
            {
                indexIsFound = true;
                currentHumanoidIndex = index;
            }
        }

        humanoidLoreList[currentHumanoidIndex].SetActive(true);

        HumanoidButtonInteraction();
    }

    private void HumanoidButtonInteraction()
    {
        if (firstDiscoveredHumanoidIndex == lastDiscoveredHumanoidIndex)
        {
            nextButtonHumanoid.GetComponent<Button>().interactable = false;
            lastButtonHumanoid.GetComponent<Button>().interactable = false;
        }
        else if (currentHumanoidIndex == lastDiscoveredHumanoidIndex)
        {
            nextButtonHumanoid.GetComponent<Button>().interactable = false;
            lastButtonHumanoid.GetComponent<Button>().interactable = true;
        }
        else if (currentHumanoidIndex == firstDiscoveredHumanoidIndex)
        {
            nextButtonHumanoid.GetComponent<Button>().interactable = true;
            lastButtonHumanoid.GetComponent<Button>().interactable = false;
        }
        else
        {
            nextButtonHumanoid.GetComponent<Button>().interactable = true;
            lastButtonHumanoid.GetComponent<Button>().interactable = true;
        }
    }

    /// <summary>
    /// I turn off all the lore for humanoids when leaving beast screen.
    /// </summary>
    public void HumanoidLoreCleanup()
    {
        for (int index = 0; index < humanoidLoreList.Count; index++)
        {
            humanoidLoreList[index].SetActive(false);
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

    public void ReadMonstrosityLoreInBestiary(int monstrosityIndex)
    {
        backButtonMonstrosity.SetActive(true);
        nextButtonMonstrosity.SetActive(true);
        lastButtonMonstrosity.SetActive(true);
        leaveMonstrosityPageButton.SetActive(false);
        currentMonstrosityIndex = monstrosityIndex;

        MonstrosityButtonInteraction();

        monstrosityLoreList[currentMonstrosityIndex].SetActive(true);
    }

    public void ReadNextMonstrosityLoreInBestiary()
    {
        monstrosityLoreList[currentMonstrosityIndex].SetActive(false); //Turns of the last Lore

        bool indexIsFound = false;
        for (int index = 0; index < saving.data.monstrosityList.Length; index++)
        {
            if (index > currentMonstrosityIndex && saving.data.monstrosityList[index] == true && indexIsFound == false) //Finds the next lore that has been unclocked.
            {
                indexIsFound = true;
                currentMonstrosityIndex = index;
            }
        }

        monstrosityLoreList[currentMonstrosityIndex].SetActive(true); //Turns on the next Lore

        MonstrosityButtonInteraction();

    }

    public void ReadLastMonstrosityLoreInBestiary()
    {
        monstrosityLoreList[currentMonstrosityIndex].SetActive(false);

        bool indexIsFound = false;
        for (int index = saving.data.monstrosityList.Length - 1; index >= 0; index--) //A reverse for loop (This might explode...
        {
            if (index < currentMonstrosityIndex && saving.data.monstrosityList[index] == true && indexIsFound == false)
            {
                indexIsFound = true;
                currentMonstrosityIndex = index;
            }
        }

        monstrosityLoreList[currentMonstrosityIndex].SetActive(true);

        MonstrosityButtonInteraction();
    }

    private void MonstrosityButtonInteraction()
    {
        if (firstDiscoveredMonstrosityIndex == lastDiscoveredMonstrosityIndex)
        {
            nextButtonMonstrosity.GetComponent<Button>().interactable = false;
            lastButtonMonstrosity.GetComponent<Button>().interactable = false;
        }
        else if (currentMonstrosityIndex == lastDiscoveredMonstrosityIndex)
        {
            nextButtonMonstrosity.GetComponent<Button>().interactable = false;
            lastButtonMonstrosity.GetComponent<Button>().interactable = true;
        }
        else if (currentMonstrosityIndex == firstDiscoveredMonstrosityIndex)
        {
            nextButtonMonstrosity.GetComponent<Button>().interactable = true;
            lastButtonMonstrosity.GetComponent<Button>().interactable = false;
        }
        else
        {
            nextButtonMonstrosity.GetComponent<Button>().interactable = true;
            lastButtonMonstrosity.GetComponent<Button>().interactable = true;
        }
    }

    /// <summary>
    /// I turn off all the lore for monstrosities when leaving beast screen.
    /// </summary>
    public void MonstrosityLoreCleanup()
    {
        for (int index = 0; index < monstrosityLoreList.Count; index++)
        {
            monstrosityLoreList[index].SetActive(false);
        }
    }


    #endregion
    #region Boss Functions
    public void Boss0ContentShow()
    {
        for (int index = 0; index < boss1ContentList.Count; index++)
        {
            boss1ContentList[index].SetActive(true); //Turns on all Boss 1 content features
        }
    }

    public void Boss1ContentShow()
    {
        for (int index = 0; index < boss1ContentList.Count; index++)
        {
            boss2ContentList[index].SetActive(true); //Turns on all Boss 2 content features
        }
    }

    public void BossContentHider()
    {
        for (int index = 0; index < boss1ContentList.Count; index++)
        {
            boss1ContentList[index].SetActive(false);
        }

        for (int index = 0; index < boss1ContentList.Count; index++)
        {
            boss1ContentList[index].SetActive(false);
        }
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
        CalculateBeastBestiarySlots();
        CalculateHumanoidBestiarySlots();
        CalculateMonstrosityBestiarySlots();

        //Debugg
        BestiaryDebugIndex();
    }
    #endregion
}
