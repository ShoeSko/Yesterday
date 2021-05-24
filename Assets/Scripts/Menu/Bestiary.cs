using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    private SaveSystem saving;

    [SerializeField] private List<GameObject> unitBestiaryList = new List<GameObject>();
    [SerializeField] private List<GameObject> enemyBestiaryList = new List<GameObject>();
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
            LoadEnemyBestiary();
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

    private void LoadEnemyBestiary()
    {
        for (int index = 0; index < enemyBestiaryList.Count; index++)
        {
            if (saving.data.enemyList[index] == true)
            {
                enemyBestiaryList[index].SetActive(true);
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

        for (int index = 0; index < enemyBestiaryList.Count; index++)
        {
            enemyBestiaryList[index].SetActive(false);
        }

        for (int index = 0; index < bossBestiaryList.Count; index++)
        {
            bossBestiaryList[index].SetActive(false);
        }
    }
}
