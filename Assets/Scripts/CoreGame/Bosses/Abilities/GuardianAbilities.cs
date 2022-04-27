using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianAbilities : TheBossClass_Parent
{
    public override void Ability1()
    {
        Debug.Log("I used Guardian ability 1");

        int spawnLocation = Random.Range(0, 4);
        GameObject wrath = GameObject.Find("The Boss").GetComponent<CoreBossManager>().Natureswrath;
        Instantiate(wrath);
        wrath.transform.position = new Vector3(13, (spawnLocation * 1.8f) - 1.4f, 0);
    }

    public override void Ability2()
    {
        Debug.Log("I used Guardian ability 2");
    }

    public override void Ability3()
    {
        Debug.Log("I used Guardian ability 3");
    }
}
