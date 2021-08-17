using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionScript : MonoBehaviour
{

    [SerializeField] private List<Sprite> progressionImageList = new List<Sprite>(); // value 0 Question mark, 1-12 Minigames, Day, Evening, boss 1-3

    [SerializeField] private List<Image> stage1ImageList = new List<Image>(); //Contains the 3 first Minigames
    [SerializeField] private List<Image> stage2ImageList = new List<Image>(); //Contains the 3 Minigames after day
    [SerializeField] private List<Image> stage3ImageList = new List<Image>(); //Contains the 3 last Minigames

    [SerializeField] private List<Image> coreGameImageList = new List<Image>(); //Contains Day, evening + all night.


    private void Start()
    {
        if (FindObjectOfType<SaveSystem>())
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>(); //Finds the save system in the scene
            if (MinigameSceneScript.Tutorial)
            {
                //Tutorial has a cut & run order, Minigame 2, Minigame 10 & Minigame 6 + Core Game.
            }
            else
            {
                if(NewCardHandScript.Stage == 1)
                {
                    for (int i = 0; i < stage1ImageList.Count; i++)
                    {
                        stage1ImageList[i].sprite = progressionImageList[saving.data.progressValueList[i]]; //Sets the stage 1 images to match the value stored in the save data.
                    }
                }
                else if(NewCardHandScript.Stage == 2)
                {
                    for (int i = 0; i < stage1ImageList.Count; i++)
                    {
                        stage1ImageList[i].sprite = progressionImageList[saving.data.progressValueList[i]]; //Sets the stage 1 images to match the value stored in the save data.
                    }
                    for (int i = 0; i < stage2ImageList.Count; i++)
                    {
                    stage2ImageList[i].sprite = progressionImageList[saving.data.progressValueList[i+3]]; //Sets the stage 1 images to match the value stored in the save data.
                    }
                }
                else if(NewCardHandScript.Stage == 3)
                {
                    for (int i = 0; i < stage1ImageList.Count; i++)
                    {
                        stage1ImageList[i].sprite = progressionImageList[saving.data.progressValueList[i]]; //Sets the stage 1 images to match the value stored in the save data.
                    }
                    for (int i = 0; i < stage2ImageList.Count; i++)
                    {
                        stage2ImageList[i].sprite = progressionImageList[saving.data.progressValueList[i + 3]]; //Sets the stage 1 images to match the value stored in the save data.
                    }
                    for (int i = 0; i < stage3ImageList.Count; i++)
                    {
                        stage3ImageList[i].sprite = progressionImageList[saving.data.progressValueList[i+6]]; //Sets the stage 1 images to match the value stored in the save data.
                    }
                }

                //coreGameImageList[12].sprite = //Insert permanent Day Icon
                //coreGameImageList[13].sprite = //Insert permanent Evening Icon

                //Boss Is random.
            }

        }
    }
}
