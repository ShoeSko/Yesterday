using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public Data data; //Makes all values within the data accsesable
    string path => Application.dataPath + "/saveData.json";

    [ContextMenu("Save")] //Makes a menu option in component section,right click to accsess.
    void Save()
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }
    [ContextMenu("Load")]//Makes a menu option in component section,right click to accsess.
    void Load()
    {
        string json = File.ReadAllText(path);
        Data loadedData = JsonUtility.FromJson<Data>(json);

        data = loadedData;
    }

    private void Start()
    {
        DontDestroyOnLoad(this); //Makes it so that all scenes can see it. (Potentialy other scrip
    }
}

[System.Serializable]
public class Data //Serializable class of data. All inf to be saved should be here.
{
    public int minigamesFinished;
    public int enemiesKilled;
    public int cardsSummoned;

    public Data(int minigamesFinished, int enemiesKilled, int cardsSummoned)
    {
        this.minigamesFinished = minigamesFinished;
        this.enemiesKilled = enemiesKilled;
        this.cardsSummoned = cardsSummoned;
    }
}