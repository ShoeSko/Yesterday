using System.Collections.Generic;
using UnityEngine;

public class AnimalVictory : MonoBehaviour
{

    public List<GameObject> stars = new List<GameObject>(); //A list of the stars
    private int starLenght;//How long is the star list
    private float scoreTimer; //How long it takes to do the game
    public static int _animalPensFilled; //How many pens have been filled.

    public int star1Time;
    public int star2Time;
    public int star3Time;

    private void Awake()
    {
        _animalPensFilled = 0; //Resets the game
    }

    private void Start()
    {
        starLenght = stars.Count;
        for (int i = 0; i < starLenght; i++)
        {
            stars[i].SetActive(false);
        }
    }
    private void Update()
    {
        AllAnimalsInPens();
    }

    private void AllAnimalsInPens()
    {
        scoreTimer += Time.deltaTime;
        if (_animalPensFilled == 4)
        {
            if (scoreTimer <= star1Time)//define score for this minigame
            {
                for (int i = 0; i < starLenght; i++)
                {
                    stars[i].SetActive(true);
                }
                //3 stars
            }
            else if (scoreTimer > star1Time && scoreTimer <= star2Time)
            {
                for (int i = 0; i < starLenght - 1; i++)
                {
                    stars[i].SetActive(true);
                    print("Did it");
                }
                    //2 stars
                }
            else if (scoreTimer > star2Time && scoreTimer <= star3Time)
            {
                for (int i = 0; i < starLenght - 2; i++)
                {
                    stars[i].SetActive(true);
                }
                    //1 star
                }
            else if (scoreTimer > 5)
                {
                    //0 stars
                }
            print("victory");
            _animalPensFilled = 0; //Resets the game
        }
        print(_animalPensFilled);
    }
    private void OnDestroy()
    {
        _animalPensFilled = 0; //Resets the game
    }
}
