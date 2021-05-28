using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    Vector3 mousePos;
    public float moveSpeed = 0.1f;
    Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);
    public bool Boop = false;
    public GameObject Nose;
    public GameObject text;
    public GameObject Objective;

    public AudioSource boopSound;
    private float scoreTimer;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject Blackstar1;
    public GameObject Blackstar2;
    public GameObject Blackstar3;

    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.
    private int score;

    [Header("Time Limit Minigame")]
    [SerializeField] private float minigameTimeLimit = 30f;
    private bool timeIsUp;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        text.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        Blackstar1.SetActive(false);
        Blackstar2.SetActive(false);
        Blackstar3.SetActive(false);

        score = 0;

        StartCoroutine(TimerForMinigame());
    }
    private void Update()
    {

        if (!Boop)//hand movement following the mouse 
        {
            scoreTimer += Time.deltaTime;

            Camera mainCamera = Camera.main;
            #if UNITY_ANDROID //Everything within this, only works if the build is android.
                if (Input.touchCount > 0)
                {
                    mousePos = mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position); //Short temp touch version
                }
            transform.position = mousePos + Vector3.forward * 10;
#else
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            position = Vector2.Lerp(transform.position, mousePos, moveSpeed);
#endif
        }

        if (Boop || timeIsUp)//when the hand touches the nose, add blush
        {
            text.SetActive(true);
            Objective.SetActive(false);


            if(scoreTimer <= 2)//define score for this minigame
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                //3 stars
                score = 3;
                CardReward.Stars = 3;
            }
            else if(scoreTimer >2 && scoreTimer <= 4)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                Blackstar3.SetActive(true);
                //2 stars
                score = 2;
                CardReward.Stars = 2;
            }
            else if(scoreTimer > 4 && scoreTimer <= 6)
            {
                star1.SetActive(true);
                Blackstar2.SetActive(true);
                Blackstar3.SetActive(true);
                //1 star
                score = 1;
                CardReward.Stars = 1;
            }
            else if(scoreTimer > 6)
            {
                Blackstar1.SetActive(true);
                Blackstar2.SetActive(true);
                Blackstar3.SetActive(true);
                //0 stars
            }
        levelTransitioner.currentMinigameScore = score;
        nextSceneButton.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D colliderName)
    {
        if (colliderName.gameObject == Nose)
        {
            boopSound.Play();
            Boop = true;
        }
    }

    IEnumerator TimerForMinigame()
    {
        yield return new WaitForSeconds(minigameTimeLimit);

        timeIsUp = true;
    }
}