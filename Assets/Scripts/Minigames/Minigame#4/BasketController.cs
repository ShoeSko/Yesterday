using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    private Rigidbody2D rb;
    public static float moveSpeed = 8.5f;

    [Range(-0.5f, 0.5f)] public float rotDeadZone = 0.1f;
    public GameObject otherWalls; //The walls to use if it does not support gyro
    public GameObject androidWalls;//The walls to use if it supports gyro

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-moveSpeed, 0);
    }


    void Update()
    {
        BasketControlls();
    }

    private void BasketControlls()
    {
#if UNITY_ANDROID //Everything within this, only works if the build is android.
        if (Input.acceleration.x < -rotDeadZone)//move left with phone rotation
            rb.velocity = new Vector2(-moveSpeed, 0);

        else if (Input.acceleration.x > rotDeadZone)//move right with phone rotation
            rb.velocity = new Vector2(moveSpeed, 0);

        else if (Input.acceleration.x > -rotDeadZone || Input.acceleration.x < rotDeadZone) //Stops the basket from moving
            rb.velocity = new Vector2(0, 0);

        otherWalls.SetActive(false);
        androidWalls.SetActive(true);
#else
        if (Input.GetKey(KeyCode.A))//move left
            rb.velocity = new Vector2(-moveSpeed, 0);

        else if (Input.GetKey(KeyCode.D))//move right
            rb.velocity = new Vector2(moveSpeed, 0);
#endif
    }
}
