using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandScript : MonoBehaviour
{
    Vector3 mousePos;
    public float moveSpeed = 0.1f;
    Rigidbody2D rb;
    Vector2 position = new Vector2(0f, 0f);
    public bool Boop = false;
    public GameObject Nose;
    public GameObject boopeffect;
    public GameObject text;
    private float clock = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        text.SetActive(false);
    }
    private void Update()
    {
        if (!Boop)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            position = Vector2.Lerp(transform.position, mousePos, moveSpeed);
        }

        if (Boop)
        {
            boopeffect.transform.position = new Vector3(3.700827f, 0.2415431f, 0.5916452f);
            text.SetActive(true);
            clock += Time.deltaTime;
        }

        if (clock >= 5)
            SceneManager.LoadScene("CoreGame");
    }

    private void FixedUpdate()
    {
        rb.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D colliderName)
    {
        if (colliderName.gameObject == Nose)
            Boop = true;
    }
}