using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftwall : MonoBehaviour
{
    private Rigidbody2D basket;
    private float moveSpeed = BasketController.moveSpeed;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        basket = collider.GetComponent<Rigidbody2D>();
        basket.velocity = Vector2.right * moveSpeed;
    }
}
