using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corporate_Intro : MonoBehaviour
{
    public GameObject BossManager;

    public GameObject Player;
    public GameObject Corporate;
    public GameObject Guardian;
    public GameObject background;

    private Rigidbody2D RBplayer;
    private Rigidbody2D RBboss;

    private float CharacterSpeed;
    private float CharacterSpeed2;
    private float speedFunction;
    private float speedFunction2;

    private bool PlayMe1;
    private bool PlayMe2;
    private bool PlayMe3;

    public int Intro;//Test variable to test different intros

    private bool SpawnDelay;


    private void Start()
    {
        Player.SetActive(false);
        Corporate.SetActive(false);
        Guardian.SetActive(false);
        background.SetActive(false);
    }

    void FixedUpdate()
    {
        //Skip function
        if (Input.GetKeyDown(KeyCode.Escape))
            ImDone();

        if(PlayMe1 == true)
        {
            speedFunction += Time.deltaTime;
            CharacterSpeed = (speedFunction * speedFunction) +1;

            //Player controller
            RBplayer.velocity = new Vector2(CharacterSpeed, 0);

            //Boss controler
            RBboss.velocity = new Vector2(CharacterSpeed * -1, 0);

            if (speedFunction >= 4)
                ImDone();
        }
        else if(PlayMe2 == true)
        {
            speedFunction += Time.deltaTime;
            CharacterSpeed = (1 / (speedFunction * speedFunction));
            CharacterSpeed = CharacterSpeed * 5;

            //Player controller
            RBplayer.velocity = new Vector2(CharacterSpeed, 0);

            //Boss controler
            RBboss.velocity = new Vector2(CharacterSpeed * -1, 0);

            if (speedFunction >= 6)
                ImDone();
        }
        else if(PlayMe3 == true)
        {
            if (speedFunction >= 3)
                SpawnDelay = true;

            speedFunction += Time.deltaTime;
            CharacterSpeed = (1 / (speedFunction * speedFunction));
            CharacterSpeed = CharacterSpeed * 5;

            if(SpawnDelay == true)
            {
                speedFunction2 += Time.deltaTime;
                CharacterSpeed2 = (1 / (speedFunction2 * speedFunction2));
                CharacterSpeed2 = CharacterSpeed2 * 6;
            }

            //Player controller
            RBplayer.velocity = new Vector2(CharacterSpeed, 0);

            //Boss controler
            RBboss.velocity = new Vector2(CharacterSpeed2 * -1, 0);

            if (speedFunction >= 8)
                ImDone();
        }
    }

    public void PlayIntro()
    {
        if (Intro == 1)
            Intro1();
        else if (Intro == 2)
            Intro2();
        else if (Intro == 3)
            Intro3();
    }

    void Intro1()
    {
        Player.SetActive(true);
        background.SetActive(true);
        RBplayer = Player.GetComponent<Rigidbody2D>();

        if(BossManager.GetComponent<NewCardHandScript>().RandomBoss == 1)
        {
            Corporate.SetActive(true);
            RBboss = Corporate.GetComponent<Rigidbody2D>();
        }
        else if (BossManager.GetComponent<NewCardHandScript>().RandomBoss == 2)
        {
            Guardian.SetActive(true);
            RBboss = Guardian.GetComponent<Rigidbody2D>();
        }

        speedFunction = -2.5f;

        PlayMe1 = true;
    }

    void Intro2()
    {
        Player.SetActive(true);
        background.SetActive(true);
        RBplayer = Player.GetComponent<Rigidbody2D>();
        if (BossManager.GetComponent<NewCardHandScript>().RandomBoss == 1)
        {
            Corporate.SetActive(true);
            RBboss = Corporate.GetComponent<Rigidbody2D>();
        }
        else if (BossManager.GetComponent<NewCardHandScript>().RandomBoss == 2)
        {
            Guardian.SetActive(true);
            RBboss = Guardian.GetComponent<Rigidbody2D>();
        }
        speedFunction = 0.5f;

        PlayMe2 = true;
    }

    void Intro3()
    {
        Player.SetActive(true);
        background.SetActive(true);
        RBplayer = Player.GetComponent<Rigidbody2D>();
        if (BossManager.GetComponent<NewCardHandScript>().RandomBoss == 1)
        {
            Corporate.SetActive(true);
            RBboss = Corporate.GetComponent<Rigidbody2D>();
        }
        else if (BossManager.GetComponent<NewCardHandScript>().RandomBoss == 2)
        {
            Guardian.SetActive(true);
            RBboss = Guardian.GetComponent<Rigidbody2D>();
        }
        speedFunction = 0.5f;
        speedFunction2 = 0.5f;

        PlayMe3 = true;
    }

    void ImDone()
    {
        Destroy(background);
        Destroy(Corporate);
        Destroy(Guardian);
        Destroy(Player);
        Destroy(this);
    }
}
