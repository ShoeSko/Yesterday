using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (score != 3)
            ScoreTimer += Time.deltaTime;//Overall timer for score

        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        position = Vector2.Lerp(transform.position, mousePos, moveSpeed);

        if (score == 3)//Win Condition
        {
            gamoverTimer += Time.deltaTime;

            if (ScoreTimer <= 9)//3 stars
            {
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                CardReward.Stars = 3;
            }
            else if (ScoreTimer > 9 && ScoreTimer <= 15)//2 stars
            {
                star1.SetActive(true);
                star2.SetActive(true);
                CardReward.Stars = 2;
            }
            else if (ScoreTimer > 15 && ScoreTimer <= 20)//1 stars
            {
                star1.SetActive(true);
                CardReward.Stars = 1;
            }


            if (gamoverTimer >= 5)//change scenes after 5 sec
            {
                if (ScoreTimer > 20)//skip card reward
                {
                    if (MinigameSceneScript.activeMinigame == 1)
                    {
                        MinigameSceneScript.activeMinigame++;
                        SceneManager.LoadScene("Minigame#" + MinigameSceneScript.scene2);
                    }
                    else if (MinigameSceneScript.activeMinigame == 2)
                    {
                        MinigameSceneScript.activeMinigame++;
                        SceneManager.LoadScene("Minigame#" + MinigameSceneScript.scene3);
                    }
                    else if (MinigameSceneScript.activeMinigame == 3)
                    {
                        SceneManager.LoadScene("CoreGame");
                    }
                }
                else//go to card reward
                    SceneManager.LoadScene("CardReward");
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }
}