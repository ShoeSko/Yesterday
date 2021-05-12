using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public Data data; //Makes all values within the data accsesable
    string path => Application.dataPath + "/SaveData/saveData.json";

    #region Save & Load
    [ContextMenu("Save")] //Makes a menu option in component section,right click to accsess.
    void Save()
    {
        string json = JsonUtility.ToJson(data); //Writes the Data in Json Format
        File.WriteAllText(path, json);  //Stores the Json Data
    }
    [ContextMenu("Load")]//Makes a menu option in component section,right click to accsess.
    void Load()
    {
        string json = File.ReadAllText(path); //Reads the Json Data
        Data loadedData = JsonUtility.FromJson<Data>(json); //Write the Json format in as readable data

        data = loadedData;
    }
    #endregion
    #region At the start
    private void Awake()
    {
        DontDestroyOnLoad(this); //Makes it so that all scenes can see it. (Potentialy other scrip
        if(Directory.Exists(Application.dataPath + "/SaveData/"))
        {
             Load(); //Loads the game at the start.
            print("Data Loaded");
        }
        else 
        { 
            Directory.CreateDirectory(Application.dataPath + "/SaveData/");
        }
    }
    #endregion
    #region When to save
    //Currently this can be called everytime a value is changed (Will prevent lost data from crash, might be heavy?)

    public void SaveTheData()
    {
        Save(); //Saves the data on every loaded level.
        print("Data Saved!");
    }
    #endregion
}

[System.Serializable]
public class Data //Serializable class of data. All inf to be saved should be here.
{
    public float masterVolLevel;
    public float musicVolLevel;
    public float sfxVolLevel;


    //public int minigamesFinished;
    //public int enemiesKilled;
    //public int cardsSummoned;

    public Data(float masterVolLevel, float musicVolLevel, float sfxVolLevel)
    {
        this.masterVolLevel = masterVolLevel;
        this.musicVolLevel = musicVolLevel;
        this.sfxVolLevel = sfxVolLevel;
    }

    //public Data(int minigamesFinished, int enemiesKilled, int cardsSummoned)
    //{


    //    this.minigamesFinished = minigamesFinished;
    //    this.enemiesKilled = enemiesKilled;
    //    this.cardsSummoned = cardsSummoned;
    //}
}