using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimalVictory : MonoBehaviour
{

    public List<GameObject> stars = new List<GameObject>(); //A list of the stars
    private int starLenght;//How long is the star list
    private float scoreTimer; //How long it takes to do the game
    public static int _animalPensFilled; //How many pens have been filled.

    public int star1Time;
    public int star2Time;
    public int star3Time;

    [Header("Level Transition")]
    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int score;

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
        if(_animalPensFilled != 4)
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
                CardReward.Stars = 3;
                score = 3;
            }
            else if (scoreTimer > star1Time && scoreTimer <= star2Time)
            {
                for (int i = 0; i < starLenght - 1; i++)
                {
                    stars[i].SetActive(true);
                }
                //2 stars
                CardReward.Stars = 2;
                score = 2;
            }
            else if (scoreTimer > star2Time && scoreTimer <= star3Time)
            {
                for (int i = 0; i < starLenght - 2; i++)
                {
                    stars[i].SetActive(true);
                }
                //1 star
                CardReward.Stars = 1;
                score = 1;
            }
            else if (scoreTimer > 15)
            {
                //0 stars
            }

            nextSceneButton.SetActive(true);
            levelTransitioner.currentMinigameScore = score;
        }
    }
    private void OnDestroy()
    {
        _animalPensFilled = 0; //Resets the game
    }
}
