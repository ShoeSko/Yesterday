using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bestiary : MonoBehaviour
{
    private SaveSystem saving;

    [SerializeField] private List<GameObject> unitBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> beastBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> humanoidBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> monstrosityBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> bossBestiaryList = new List<GameObject>();


    [SerializeField] private GameObject CardV2;
    [SerializeField] private GameObject CardButton;

    private void Awake()
    {
        HideTheLore(); //Hides the lore by turning lore off.
    }

    private void Start()
    {
        InitiateTheBestiary();
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

    public void ReadCardInBestiary(CardScript UnitRead)
    {
        CardV2.SetActive(true);
        CardButton.SetActive(true);

        CardV2.GetComponent<CardDisplayer>().card = UnitRead;

        CardV2.GetComponent<CardDisplayer>().Read();

    }

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
    }
    #endregion
}
