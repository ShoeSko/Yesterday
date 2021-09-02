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
                stage1ImageList[0].sprite = progressionImageList[2];
                stage1ImageList[1].sprite = progressionImageList[10];
                stage1ImageList[2].sprite = progressionImageList[6];

                coreGameImageList[0].sprite = progressionImageList[13];

                for (int i = 0; i < stage2ImageList.Count; i++)
                {
                    stage2ImageList[i].gameObject.SetActive(false);
                    stage3ImageList[i].gameObject.SetActive(false);
                }
                coreGameImageList[1].gameObject.SetActive(false);
                coreGameImageList[2].gameObject.SetActive(false);
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

                coreGameImageList[0].sprite = progressionImageList[13];
                coreGameImageList[1].sprite = progressionImageList[14];

                if (saving.data.bossList[0]!)
                {
                    coreGameImageList[2].sprite = progressionImageList[15]; //Corporate boss has yet to be defeated.
                }
                else if (saving.data.bossList[0] && saving.data.bossList[1]!)
                {
                    coreGameImageList[2].sprite = progressionImageList[16]; //The guardian has yet to be defeated.
                }
                //IMPLEMENT THIRD BOSS HERE!
            }

        }
    }
}
