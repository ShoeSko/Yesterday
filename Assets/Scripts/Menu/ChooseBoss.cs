using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBoss : MonoBehaviour
{
    public Slider choosebossSlider;
    public List<GameObject> bosses = new List<GameObject>();

    private void Update()
    {
        //NewCardHandScript.whichBoss = (int)choosebossSlider.value +1;

        for(int i = 0; i < bosses.Count; i++)
        {
            if (i == (int)choosebossSlider.value)
                bosses[i].SetActive(true);
            else
                bosses[i].SetActive(false);
        }
    }
}
