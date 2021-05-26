using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturesWrath : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed;
    private int damage;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = -600;
        damage = 50;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Units")
        {
            Debug.Log("I dealth damage");
            collider.GetComponent<UnitPrototypeScript>().TakeDamage(damage);//Deal damage to that unit (hopefully)
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
