using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    private float speed = 8;
    private float jumpSpeed = 15;
    private Rigidbody2D playerRB;
    public GameObject player;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private bool jumpCooldown;
    private float jumpDelay;

    public static bool win;
    public static int score;

    void Start()
    {
        win = false;
        score = 0;

        playerRB = player.GetComponent<Rigidbody2D>();

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    void Update()
    {
        if(!win)
            transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && !jumpCooldown && !win)
        {
            playerRB.velocity = Vector2.up * jumpSpeed;
            jumpCooldown = true;
        }

        if (jumpCooldown)
        {
            jumpDelay += Time.deltaTime;
        }

        if (jumpDelay >= 1.15)
        {
            jumpCooldown = false;
            jumpDelay = 0;
        }

        if (win)
        {
            if(score == 3)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
            }
            else if(score == 2)
            {
                star1.SetActive(true);
                star2.SetActive(true);
            }
            else if(score == 1)
            {
                star1.SetActive(true);
            }
        }
    }
}