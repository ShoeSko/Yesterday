using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedPulling : MonoBehaviour
{
    Vector3 mousePos;
    private float moveSpeed = 0.02f;
    Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);
    public GameObject mouse;

    public Collider2D mouseCollider;//this is the "mouse collider"

    private bool setActive;
    public bool PulledOut;
    private bool inactive;

    private Vector2 yeetVector;
    private float yeetForce = 100;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()//plant movement - script for following the mouse when being dragged
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        position = Vector2.Lerp(transform.position, mousePos, moveSpeed);

        if (PulledOut)
            yeet();
    }

    private void FixedUpdate()
    {
        if (setActive == true && Input.GetKey(KeyCode.Mouse0) && !inactive)//If the mouse is within the hitbox of the plant, you can drag it
            rb.MovePosition(position);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider == mouseCollider)
            setActive = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider == mouseCollider)
            setActive = false;
    }

    void yeet()
    {
        inactive = true;
        yeetVector = new Vector2(Random.Range(-10, 11), Random.Range(3, 6));
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = 1f;
        rb.AddForce(yeetVector * yeetForce);
        mouse.GetComponent<MouseCollider>().score++;
        PulledOut = false;
    }
}