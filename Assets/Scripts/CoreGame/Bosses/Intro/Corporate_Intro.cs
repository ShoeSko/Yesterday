using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corporate_Intro : MonoBehaviour
{
    public GameObject Player;
    public GameObject Boss;
    public GameObject background;

    private Rigidbody2D RBplayer;
    private Rigidbody2D RBboss;

    private float CharacterSpeed;
    private float speedFunction;

    private bool PlayMe;

    private void Start()
    {
        Player.SetActive(false);
        Boss.SetActive(false);
        background.SetActive(false);
    }

    void FixedUpdate()
    {
        if(PlayMe == true)
        {
            speedFunction += Time.deltaTime;
            CharacterSpeed = (speedFunction * speedFunction);

            //Player controller
            RBplayer.velocity = new Vector2(CharacterSpeed, 0);

            //Boss controler
            RBboss.velocity = new Vector2(CharacterSpeed * -1, 0);

            if (speedFunction >= 4)
                ImDone();
        }
    }

    public void PlayIntro()
    {
        Player.SetActive(true);
        Boss.SetActive(true);
        background.SetActive(true);

        RBplayer = Player.GetComponent<Rigidbody2D>();
        RBboss = Boss.GetComponent<Rigidbody2D>();
        speedFunction = -3;

        PlayMe = true;
    }

    void ImDone()
    {
        Destroy(background);
        Destroy(Boss);
        Destroy(Player);
        Destroy(this);
    }
}
