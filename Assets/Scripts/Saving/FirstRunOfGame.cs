using UnityEngine;

public class FirstRunOfGame : MonoBehaviour
{
    private SaveSystem saving;

    [SerializeField] private int amountOfUnits;
    [SerializeField] private int amountOfEnemies;
    [SerializeField] private int amountOfBosses;

    private void Start()
    {
        if (FindObjectOfType<SaveSystem>())
        {
            saving = FindObjectOfType<SaveSystem>();
            if(saving.data.isFirstRun == false) //If this is the first time the game is booted up / reset
            {
                saving.data.isFirstRun = true;

                saving.data.unitList = new bool[amountOfUnits];
                saving.data.enemyList = new bool[amountOfEnemies];
                saving.data.bossList = new bool[amountOfBosses];
            }

        }
    }
}
