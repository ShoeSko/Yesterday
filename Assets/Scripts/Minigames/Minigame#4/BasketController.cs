using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    private Rigidbody2D rb;
    public static int moveSpeed = 7;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-moveSpeed, 0);
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A))//move left
            rb.velocity = new Vector2(-moveSpeed, 0);

        else if (Input.GetKey(KeyCode.D))//move right
            rb.velocity = new Vector2(moveSpeed, 0);
    }
}
