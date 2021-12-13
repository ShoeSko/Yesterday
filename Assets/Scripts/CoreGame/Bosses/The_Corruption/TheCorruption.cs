using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheCorruption : MonoBehaviour
{
    private List<string> Abilities = new List<string>();

    public List<GameObject> DeactivateTowerSpots = new List<GameObject>();
    private GameObject CurrentTowerSpot;

    public GameObject BossHealthbar;
    public Slider BossHealth;
    public GameObject WinCondition;
    private bool hasBeenDefeated;

    //Setup
    private Rigidbody2D rb;
    public bool IsActive;
    private Vector2 NewPos;
    private float speed;
    public int Health = 4;//boss' health

    //This will keep track of boss' cooldowns
    private int CD1;
    private int CD2;
    private int CD3;

    private int RandomAbility;

    //Ability 1


    //Ability 2


    //Ability 3



    private float timer;
    private int loop;

    private bool IsDead;

    private void Start()
    {
        Health = 4;
        BossHealthbar.SetActive(false);//Sets his healthbar to inactive
    }

    public void Activate()
    {
        BossHealthbar.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        NewPos = new Vector2(6.6f, 1.59707f);
        speed = 0.9f;

        Abilities.Add("Wrath_of_the_Forest");//Damage spikes
        Abilities.Add("Eclipse");//Mist screen coverup
        Abilities.Add("Motherly_Embrace");//Buff

        for (int deactivated = 0; deactivated < DeactivateTowerSpots.Count; deactivated++)//make space for the boss by destroying the last row
        {
            CurrentTowerSpot = DeactivateTowerSpots[deactivated];
            Destroy(CurrentTowerSpot);
        }
    }

    private void Update()
    {
        if (IsActive)
        {
            BossHealth.value = Health;
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, NewPos, speed * Time.deltaTime);//move dramatically to position

            timer += Time.deltaTime;

            if(timer >= 18)//time ebfore boss uses ability;
            {

            }
        }
    }
}
