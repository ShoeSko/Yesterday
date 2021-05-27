using UnityEngine;

public class FirstRunOfGame : MonoBehaviour
{
    [SerializeField] private bool reset;

    private SaveSystem saving;

    [Header("Wanted volume on the first boot")]
    [SerializeField] [Range(0, 1)] float masterVolume;
    [SerializeField] [Range(0, 1)] float musicVolume;
    [SerializeField] [Range(0, 1)] float sfxVolume;

    [Header("Size of the list of Units/Enemies in the game")]
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
            SetVolume();
            SetListSizes();
        }
    }

    private void SetVolume()
    {
        if (saving.data.isFirstRun == false || reset) //If this is the first time the game is booted up / reset
        {
            saving.data.masterVolLevel = masterVolume;
            saving.data.musicVolLevel = musicVolume;
            saving.data.sfxVolLevel = sfxVolume;
        }

    }

    private void SetListSizes()
    {
        if (saving.data.isFirstRun == false ||reset) //If this is the first time the game is booted up / reset
        {
            saving.data.isFirstRun = true;

            saving.data.unitList = new bool[amountOfUnits];
            saving.data.beastList = new bool[amountOfBeasts];
            saving.data.humanoidList = new bool[amountOfHumanoids];
            saving.data.monstrosityList = new bool[amountOfMonstrosities];
            saving.data.bossList = new bool[amountOfBosses];
        }

    }

    private void SetContinueValue()
    {
        if (saving.data.isFirstRun == false || reset) //If this is the first time the game is booted up / reset
        {
            saving.data.lastScene = "";
        }

    }
}
