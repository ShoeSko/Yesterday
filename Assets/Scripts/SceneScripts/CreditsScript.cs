using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private Collider2D aboveTheCredits;
    [SerializeField] private Collider2D belowTheCredits;
    [SerializeField] private bool isLastText;
    
    private void FixedUpdate()
    {
        ScrollText();
    }

    private void ScrollText()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, scrollSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision = aboveTheCredits)
        {
            transform.position = belowTheCredits.transform.position;
        }
    }
}
