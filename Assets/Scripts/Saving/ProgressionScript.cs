using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionScript : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<Sprite> progressionImageList = new List<Sprite>(); // value 0 Question mark, 1-12 Minigames, Day, Evening, boss 1-3

    [SerializeField] private List<Image> stageMinigameImageSlotList = new List<Image>(); //Contains the 3 slots for Minigames

    [SerializeField] private Image coreGameImage; //Contains Day, evening + all night.
    #endregion
    #region Tutorial
    private void Start()
    {
    
        if (FindObjectOfType<SaveSystem>())
        {
            SaveSystem saving = FindObjectOfType<SaveSystem>(); //Finds the save system in the scene
            if (MinigameSceneScript.Tutorial)
            {
                stageMinigameImageSlotList[0].sprite = progressionImageList[2]; //Sets the first minigame into egg click
                stageMinigameImageSlotList[1].sprite = progressionImageList[10]; //Sets the second minigame into sort animals
                stageMinigameImageSlotList[2].sprite = progressionImageList[6]; //Sets the third minigame into water plants

                coreGameImage.sprite = progressionImageList[13]; //Sets the core game into daytime (tutorial)
            }
    #endregion
    #region Stage 1
            else
            {
                if(NewCardHandScript.Stage == 1)
                {
                    for (int i = 0; i < stageMinigameImageSlotList.Count; i++)
                    {
                        stageMinigameImageSlotList[i].sprite = progressionImageList[saving.data.progressValueList[i]]; //Sets the stage 1 images to match the value stored in the save data.
                    }
                    coreGameImage.sprite = progressionImageList[13]; //Sets the core game into daytime(RAT)
                }
    #endregion
    #region Stage 2
                else if (NewCardHandScript.Stage == 2)
                {
                    for (int i = 0; i < stageMinigameImageSlotList.Count; i++)
                    {
                    stageMinigameImageSlotList[i].sprite = progressionImageList[saving.data.progressValueList[i+3]]; //Sets the stage 1 images to match the value stored in the save data.
                    }
                    coreGameImage.sprite = progressionImageList[14]; //Sets the core game into evening(Business)
                }
    #endregion
    #region Stage 3
                else if (NewCardHandScript.Stage == 3)
                {
                    for (int i = 0; i < stageMinigameImageSlotList.Count; i++)
                    {
                        stageMinigameImageSlotList[i].sprite = progressionImageList[saving.data.progressValueList[i+6]]; //Sets the stage 1 images to match the value stored in the save data.
                    }

                    if (saving.data.bossList[0]!) //Corporate boss has yet to be defeated.
                    {
                        coreGameImage.sprite = progressionImageList[15]; //Sets the core game image to Corporation.
                    }
                    else if (saving.data.bossList[1]!) //The guardian has yet to be defeated.
                    {
                        coreGameImage.sprite = progressionImageList[16];  //Sets the core game image to Guardian.
                    }
                    else if (saving.data.bossList[2]!) //The Corruption has yet to be defeated.
                    {
                        coreGameImage.sprite = progressionImageList[17]; //Sets the core game image to Corruption.
                    }
                }
            }
        }
    }
    #endregion
}
