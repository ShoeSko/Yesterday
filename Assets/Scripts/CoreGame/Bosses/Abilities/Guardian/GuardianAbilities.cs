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

        GameObject.Find("Darkness").GetComponent<DarknessMover>().UsedEclipse = true;
    }

    public override void Ability3()
    {
        Debug.Log("I used Guardian ability 3");

        GameObject VisibleEnemy = GameObject.FindGameObjectWithTag("Enemy");
        if (VisibleEnemy != null)
        {
            if (VisibleEnemy.GetComponent<BasicEnemyMovement>().chosenByMom == false)//Can't buff the same guy twice
            {
                VisibleEnemy.GetComponent<BasicEnemyMovement>().MotherlyEmbraceBuff();
                VisibleEnemy.transform.localScale = VisibleEnemy.transform.localScale * 1.5f;
            }
        }
    }
}
