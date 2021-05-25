using UnityEngine;

public class FirstRunOfGame : MonoBehaviour
{
    private SaveSystem saving;

    [SerializeField] private int amountOfUnits;
    [SerializeField] private int amountOfBeasts;
    [SerializeField] private int amountOfHumanoids;
    [SerializeField] private int amountOfMonstrosities;
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
                saving.data.beastList = new bool[amountOfBeasts];
                saving.data.humanoidList = new bool[amountOfHumanoids];
                saving.data.monstrosityList = new bool[amountOfMonstrosities];
                saving.data.bossList = new bool[amountOfBosses];
            }

        }
    }
}
