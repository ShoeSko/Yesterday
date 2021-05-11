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
    public GameObject RunningSound;

    public AudioSource jump;
    public AudioSource Run;

    private bool jumpCooldown;
    private float jumpDelay;

    public static bool win;
    public static int score;

    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  

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
        if (!win)
            transform.Translate(Vector2.left * speed * Time.deltaTime);


#if UNITY_ANDROID //Everything within this, only works if the build is android.
        if (Input.touchCount > 0 && !jumpCooldown && !win)
        {
            jump.Play();
            playerRB.velocity = Vector2.up * jumpSpeed;
            jumpCooldown = true;
        }
#else
        if (Input.GetKeyDown(KeyCode.Space) && !jumpCooldown && !win)
        {
            jump.Play();
            playerRB.velocity = Vector2.up * jumpSpeed;
            jumpCooldown = true;
        }
#endif

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
            Destroy(RunningSound);
            if(score == 3)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                CardReward.Stars = 3;
            }
            else if(score == 2)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                CardReward.Stars = 2;
            }
            else if(score == 1)
            {
                star1.SetActive(true);
                CardReward.Stars = 1;
            }

            nextSceneButton.SetActive(true);
            levelTransitioner.currentMinigameScore = score;
        }
    }
}