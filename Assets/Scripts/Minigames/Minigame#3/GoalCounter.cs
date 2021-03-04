using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalCounter : MonoBehaviour
{
    public static int counter;
    private float timer;
    public GameObject text;

    public void Start()
    {
        counter = 0;
        text.SetActive(false);
    }
    public void Update()
    {
        if (counter == 4)
        {
            timer += Time.deltaTime;
            text.SetActive(true);
        }

        if (timer >= 5)
            SceneManager.LoadScene("CoreGame");
    }
}
