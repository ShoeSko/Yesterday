using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BasketTestController : MonoBehaviour
{
    private Rigidbody2D rb;
    public static int moveSpeed = 7;
    public Transform gyroObjectTransform;
    private Vector3 gyroRotation;
    [Range(-0.5f,0.5f)]public float rotDeadZone = 0.1f;

    public TextMeshProUGUI textValuesShower;
    public TextMeshProUGUI textToShow;

    private float rotateValue;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //gyroObjectTransform = GetComponent<Transform>();
        rb.velocity = new Vector2(-moveSpeed, 0);
    }


    void Update()
    {
        GyroReader();
        MovingPlatform();
        ReadValue();
    }

    private void MovingPlatform()
    {

        if (Input.acceleration.x < -rotDeadZone)//move left  ...
            rb.velocity = new Vector2(-moveSpeed, 0);

        else if (Input.acceleration.x > rotDeadZone)//move right
            rb.velocity = new Vector2(moveSpeed, 0);

        else if (Input.acceleration.x > -0.1 || Input.acceleration.x < rotDeadZone)
            rb.velocity = new Vector2(0, 0);
    }

    private void GyroReader()
    {
        gyroRotation = gyroObjectTransform.rotation.eulerAngles;
        print(gyroRotation + " Current rotation value");
        rotateValue = gyroRotation.z;
    }

    private void ReadValue()
    {
        textValuesShower.text = rotateValue.ToString();

        textToShow.text = Input.acceleration.x.ToString();

    }
}
