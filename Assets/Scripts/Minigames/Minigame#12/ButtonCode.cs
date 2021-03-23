using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCode : MonoBehaviour
{
    public List<GameObject> setactiveButtonlist = new List<GameObject>();//list of objects to activate

    public List<GameObject> setfalseButtonlist = new List<GameObject>();//list of objects to deactivate

    public GameObject player;


    public void move()
    {
        player.transform.position = transform.position;

        {
            for (int i = 0; i < setactiveButtonlist.Count; i++)//activate all units from "to activate" list 
            {
                setactiveButtonlist[i].SetActive(true);
            }

            for (int j = 0; j < setfalseButtonlist.Count; j++)//deactivate all units from "to deactivate" list 
            {
                setfalseButtonlist[j].SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }
}