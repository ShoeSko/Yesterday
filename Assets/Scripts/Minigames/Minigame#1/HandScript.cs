using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandScript : MonoBehaviour
{
    Vector3 mousePos;
    public float moveSpeed = 0.1f;
    Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);
    public bool Boop = false;
    public GameObject Nose;
    public GameObject boopeffect;
    public GameObject text;
    private float clock = 0;
    public AudioSource boopSound;
    private float scoreTimer;

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        text.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }
    private void Update()
    {
        scoreTimer += Time.deltaTime;

        if (!Boop)//hand movement following the mouse 
        {
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

        if (Boop)//when the hand touches the nose, add blush
        {
            boopeffect.transform.position = new Vector3(3.700827f, 0.2415431f, 0.5916452f);
            text.SetActive(true);
            clock += Time.deltaTime;

            if(scoreTimer <= 2)//define score for this minigame
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                //3 stars
            }
            else if(scoreTimer >2 && scoreTimer <= 3)
            {
                star1.SetActive(true);
                star2.SetActive(true);
                //2 stars
            }
            else if(scoreTimer >3 && scoreTimer <= 4)
            {
                star1.SetActive(true);
                //1 star
            }
            else if(scoreTimer > 5)
            {
                //0 stars
            }
        }

        if (clock >= 5)//load next scene after 5 sec
            SceneManager.LoadScene("Minigame#2");
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
}