using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCollider : MonoBehaviour
{
    Vector3 mousePos;
    private float moveSpeed = 1f;
    Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private float ScoreTimer;
    private float gamoverTimer;
    public bool IWin;
    public int score;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }
    void Update()
    {
        ScoreTimer += Time.deltaTime;//Overall timer for score

        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        position = Vector2.Lerp(transform.position, mousePos, moveSpeed);

        if (score == 3)//Win Condition
        {
            gamoverTimer += Time.deltaTime;

            if (ScoreTimer <= 10)//3 stars
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
            }
            else if (ScoreTimer > 10 && ScoreTimer <= 17)//2 stars
            {
                star1.SetActive(true);
                star2.SetActive(true);
            }
            else if (ScoreTimer > 17 && ScoreTimer <= 25)//1 stars
            {
                star1.SetActive(true);
            }

            if (gamoverTimer >= 5)
            {
                print("i changed scene");
                //change scene
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }
}