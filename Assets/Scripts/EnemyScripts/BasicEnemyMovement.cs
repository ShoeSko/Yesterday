using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{

    [Header("Enemy Controls")]
    [Range(0,100)]public float moveSpeed = 5;

    [Range(0,100)]public float enemyHealth;

    [Tooltip("The tag corresponding to what the enemy will care about")]public string obstacleTags;

    private bool obstacleInTheWay = false;
    private Rigidbody2D rg2D;
    private void Start()
    {
        rg2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovingEnemy();
        EnemyDeath();
    }

    void MovingEnemy()
    {
        if (!obstacleInTheWay)//If there are no obstacles the enemy will start moving
        {
            rg2D.velocity = new Vector2(-moveSpeed * Time.deltaTime, 0);
        }
        else { /*Whatever "harm" the enemy will do*/}
    }

    private void OnCollisionEnter2D(Collision2D other) //Stops movement upon reaching a blockade
    {
        if (other.collider.CompareTag(obstacleTags))
        {
            obstacleInTheWay = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) //Restarts movement upon reaching a blockade
    {
        if (other.collider.CompareTag(obstacleTags))
        {
            obstacleInTheWay = false;
        }
    }

    private void EnemyDeath()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
