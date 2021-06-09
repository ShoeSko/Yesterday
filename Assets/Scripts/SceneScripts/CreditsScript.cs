using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsScript : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private Collider2D aboveTheCredits;
    [SerializeField] private Collider2D belowTheCredits;
    [SerializeField] private bool isLastText;

    private void Start()
    {

    }
    
    private void FixedUpdate()
    {
        ScrollText();
    }

    private void ScrollText()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, scrollSpeed * Time.deltaTime);
    }

    public void BackToMainMenuFromCredits()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision = aboveTheCredits)
        {
            transform.position = belowTheCredits.transform.position;
        }
    }
}
