using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    private SaveSystem saving;

    [SerializeField] private List<GameObject> unitBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> beastBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> humanoidBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> monstrosityBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> bossBestiaryList = new List<GameObject>();

    private void Awake()
    {
        HideTheLore(); //Hides the lore by turning lore off.
    }

    private void Start()
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
                unitBestiaryList[index].SetActive(true);
            }
        }
    }

    private void LoadBeastBestiary()
    {
        for (int index = 0; index < beastBestiaryList.Count; index++)
        {
            if (saving.data.beastList[index] == true)
            {
                beastBestiaryList[index].SetActive(true);
            }
        }
    }

    private void LoadHumanoidBestiary()
    {
        for (int index = 0; index < humanoidBestiaryList.Count; index++)
        {
            if(saving.data.humanoidList[index] == true)
            {
                humanoidBestiaryList[index].SetActive(true);
            }
        }
    }

    private void LoadMonstrosityBestiary()
    {
        for (int index = 0; index < monstrosityBestiaryList.Count; index++)
        {
            if (saving.data.monstrosityList[index] == true)
            {
                monstrosityBestiaryList[index].SetActive(true);
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
            unitBestiaryList[index].SetActive(false);
        }

        for (int index = 0; index < beastBestiaryList.Count; index++)
        {
            beastBestiaryList[index].SetActive(false);
        }

        for (int index = 0; index < bossBestiaryList.Count; index++)
        {
            bossBestiaryList[index].SetActive(false);
        }
    }
}
