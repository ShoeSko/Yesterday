using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanctuaryCode : MonoBehaviour
{
    private EnemySpawning Spawner;
    private float SanctuaryTimer;
    private float timer;
    private CorruptionAbilities boss;
    private Color RageColor = new Color(0.9622642f, 0.4039694f, 0.4039694f, 1);
    private Color BaseColor = new Color(1, 1, 1, 1);
    private GameObject TheBoss;
    public GameObject activeSpot;
    public bool SaintLives;
    public UnitPrototypeScript CurrentSaint;
    private void Start()
    {
        Spawner = GameObject.Find("Spawner(Corruption)").GetComponent<EnemySpawning>();
        boss = GameObject.Find("New Game Object").GetComponent<CorruptionAbilities>();//The object with this code is spawned in the scene named "New Game Object"
        TheBoss = GameObject.Find("The Boss");
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 17 && !SaintLives)//Time window before the sanctuary disappears
        {
            Spawner.delayBetweenSpawnsMin = boss.MinimumSpawnDelay;
            Spawner.delayBetweenSpawnsMax = boss.MaximumSpawnDelay;
            activeSpot.GetComponent<TowerSpotsScript>().IsSanctuary = false;
            activeSpot.GetComponent<Image>().color = Color.black;
            TheBoss.GetComponent<SpriteRenderer>().color = BaseColor;

            Destroy(this.gameObject);
        }

        if (SaintLives)//If spot becomes occupied, boss becomes angy
        {
            SanctuaryTimer += Time.deltaTime;

            //Reduce the spawning time
            TheBoss.GetComponent<SpriteRenderer>().color = RageColor;
            Spawner.delayBetweenSpawnsMin = 3;
            Spawner.delayBetweenSpawnsMax = 4;
        }

        if (SanctuaryTimer >= 15)//Complete Purification
        {
            TheBoss.GetComponent<CoreBossManager>().TakeDamage();

            boss.MinimumSpawnDelay--;
            boss.MaximumSpawnDelay -= 2;

            //Un-Sanctify the unit
            CurrentSaint.UnSanctifyUnit();

            Spawner.delayBetweenSpawnsMin = boss.MinimumSpawnDelay;
            Spawner.delayBetweenSpawnsMax = boss.MaximumSpawnDelay;
            activeSpot.GetComponent<TowerSpotsScript>().IsSanctuary = false;
            activeSpot.GetComponent<Image>().color = Color.black;
            TheBoss.GetComponent<SpriteRenderer>().color = BaseColor;

            boss.Desperate_Rage();

            Destroy(this.gameObject);
        }
    }

    public void PurifierDied()
    {
        Spawner.delayBetweenSpawnsMin = boss.MinimumSpawnDelay;
        Spawner.delayBetweenSpawnsMax = boss.MaximumSpawnDelay;
        activeSpot.GetComponent<TowerSpotsScript>().IsSanctuary = false;
        activeSpot.GetComponent<Image>().color = Color.black;
        GetComponent<SpriteRenderer>().color = BaseColor;

        Destroy(this.gameObject);
    }
}
