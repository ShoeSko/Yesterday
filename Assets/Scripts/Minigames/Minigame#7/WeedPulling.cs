using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedPulling : MonoBehaviour
{
    Vector3 mousePos;
    [SerializeField]private float moveSpeed = 0.15f;
    Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);

    public GameObject mouse;

    public bool PulledOut;
    private bool hasLetGo; //Has the carrot been let go?

    private Vector2 yeetVector;
    private float yeetForce = 100;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()//plant movement - script for following the mouse when being dragged
    {
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); //This now exists to prevent the carrots from being dragged downwards

        if (PulledOut)
        {
            yeet();
        }
    }

    private void OnMouseDrag() //This runs as long as you press down on a colider hold down, then move.
    {
        if (!hasLetGo)
        {

            if (mousePos.y >= 0) //Prevents the carrots from going down when pulling
            {
                position = Vector2.Lerp(transform.position, mousePos, moveSpeed);
            }

            rb.MovePosition(position);
        }
    }

    void yeet()
    {

        yeetVector = new Vector2(Random.Range(-10, 11), Random.Range(3, 6));
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = 1f;
        rb.AddForce(yeetVector * yeetForce);
        mouse.GetComponent<MouseCollider>().score++;
        PulledOut = false;
        hasLetGo = true;
        GetComponent<WeedPulling>().enabled = false;
    }
}