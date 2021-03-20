using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuackMovement : MonoBehaviour
{
    private Rigidbody2D rg;
    private float moveSpeed = 1800;

    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rg.velocity = new Vector2(moveSpeed * Time.deltaTime, 0);
    }
}