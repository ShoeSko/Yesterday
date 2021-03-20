using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightwall : MonoBehaviour
{
    private Rigidbody2D basket;
    private float moveSpeed = BasketController.moveSpeed;

    private void OnTriggerEnter2D(Collider2D collider)//change the direction of the basket
    {
        basket = collider.GetComponent<Rigidbody2D>();
        basket.velocity = Vector2.left * moveSpeed;
    }
}
