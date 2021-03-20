using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fallingEggs : MonoBehaviour
{
    private int EggsToSpawn = 10;
    public GameObject egg;
    private float min = -7.97f;
    private float max = 8.01f;
    private Vector2 spawnLocation;
    private int countdownEgg;
    public static int collectedEggs;

    private float timer = 0;
    private float wintimer = 0;
    private bool IWon;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public void Start()
    {
        collectedEggs = 0;
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        StartCoroutine(itsRainingEgg());
    }

    IEnumerator itsRainingEgg()
    {
        for (int spawnedEggs = 0; spawnedEggs < EggsToSpawn; spawnedEggs++)
        {
            spawnLocation = new Vector2(Random.Range(min, max), 5.46f);
            Instantiate(egg, spawnLocation, transform.rotation);
            countdownEgg++;

            yield return new WaitForSeconds(1.3f);
        }
    }

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 18 && timer <= 19)
            win();

        if (wintimer >= 5)
        {
            //SceneManager.LoadScene("");
            print("i changed scene");
        }

        if(IWon == true)
            wintimer += Time.deltaTime;
    }

    private void win()
    {
        IWon = true;

        if(collectedEggs == 10)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if(collectedEggs == 9)
        {
            star1.SetActive(true);
            star2.SetActive(true);
        }
        else if (collectedEggs == 8)
        {
            star1.SetActive(true);
        }
    }
}