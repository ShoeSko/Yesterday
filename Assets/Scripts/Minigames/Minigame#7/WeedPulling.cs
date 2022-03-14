using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedPulling : MonoBehaviour
{
    Rigidbody2D rb;

    public GameObject mouse;

    public bool PulledOut;

    private Vector2 yeetVector;
    private float yeetForce = 100;


    private Vector3 gameObjectSreenPoint;
    private Vector3 mousePreviousLocation;
    private Vector3 mouseCurLocation;
    private Vector3 force;

    public float topSpeed = 10;
    public float speed = 2f;

    public bool NewControlls;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()//plant movement - script for following the mouse when being dragged
    {

        if (PulledOut)
        {
            yeet();
        }

        //Debug.Log("Current velocity is " + rb.velocity.magnitude);
    }


    private void OnMouseDown()
    {
        if (!NewControlls)
        {
            //This grabs the position of the object in the world and turns it into the position on the screen
            gameObjectSreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            //Sets the mouse pointers vector3
            mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
        }
        else
        {
            transform.position += new Vector3(0, 0.26f, 0);
        }
    }

    private void OnMouseDrag() //This runs as long as you press down on a colider hold down, then move.
    {
        if (!NewControlls)
        {
            if (Input.mousePosition.y > mousePreviousLocation.y) //As long as the new input goes up, preventing the carrot from being dragged down.
            {
                mouseCurLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
                force = mouseCurLocation - mousePreviousLocation;//Changes the force to be applied
                mousePreviousLocation = mouseCurLocation;
            }
        }
    }


    private void OnMouseUp()
    {
        if (!NewControlls)
        {
            //Makes sure there isn't a ludicrous speed
            if (rb.velocity.magnitude > topSpeed)
                force = rb.velocity.normalized * topSpeed;
        }
    }

    public void FixedUpdate()
    {
        if(rb.velocity.magnitude < topSpeed)
        {
            rb.velocity = force * speed * Time.deltaTime;
        }
    }

    void yeet()
    {
        force = new Vector3(0, 0, 0);
        yeetVector = new Vector2(Random.Range(-10, 11), Random.Range(3, 6));
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = 1f;
        rb.AddForce(yeetVector * yeetForce);
        mouse.GetComponent<MouseCollider>().score++;
        PulledOut = false;
        GetComponent<WeedPulling>().enabled = false;
    }
}