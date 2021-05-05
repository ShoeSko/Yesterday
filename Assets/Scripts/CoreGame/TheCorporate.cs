using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCorporate : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool IsActive;
    private Vector2 NewPos;
    private float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        NewPos = new Vector2(8.44f, 1.34f);
        speed = 0.8f;
    }

    void Update()
    {
        if (IsActive)//when the boss is meant to be active
        {
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, NewPos, speed * Time.deltaTime);//move dramatically to position


        }
    }
}
