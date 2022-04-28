using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionAbilities : TheBossClass_Parent
{
    public override void Ability1()
    {
        Debug.Log("I used Corruption ability 1");

        int RandomLane = UnityEngine.Random.Range(0, 4);
        GameObject Lane = GameObject.Find("The Boss").GetComponent<CoreBossManager>().Lanes[RandomLane];
        GameObject CorLane = null;

        switch (RandomLane)//Add more scripts when implementing more bosses
        {
            case 0:
                Lane.SetActive(false);
                CorLane = GameObject.Find("CorruptionLine1");
                break;
            case 1:
                Lane.SetActive(false);
                CorLane = GameObject.Find("CorruptionLine2");
                break;
            case 2:
                Lane.SetActive(false);
                CorLane = GameObject.Find("CorruptionLine3");
                break;
            case 3:
                Lane.SetActive(false);
                CorLane = GameObject.Find("CorruptionLine4");
                break;
        }

        CorLane.GetComponent<CorruptedLane>().ActiveLane = Lane;
        CorLane.GetComponent<CorruptedLane>().Move();
    }

    public override void Ability2()
    {
        Debug.Log("I used Corruption ability 2");
    }

    public override void Ability3()
    {
        Debug.Log("I used Corruption ability 3");
    }
}