using System.Collections.Generic;
using UnityEngine;

public class AnimalShuffler : MonoBehaviour
{
    public List<GameObject> animalLocationsList = new List<GameObject>();
    public List<GameObject> animalList = new List<GameObject>();

    public List<GameObject> animalPenLocationsList = new List<GameObject>();
    public List<GameObject> animalPenList = new List<GameObject>();

    private int row0A = 5;
    private int row1A = 5;
    private int row2A = 5;
    private int row3A = 5;

    private int row0AP = 5;
    private int row1AP = 5;
    private int row2AP = 5;
    private int row3AP = 5;

    private int randomA = 5;
    private int randomAP = 5;

    private void Start()
    {
        ShuffleTheAnimalsAndTheirHomes();
    }

    private void ShuffleTheAnimalsAndTheirHomes()
    {

        for (int currentRow = 0; currentRow < animalList.Count; currentRow++)
        {
            while (randomA == row0A || randomA == row1A || randomA == row2A)
            {
                randomA = Random.Range(0, animalList.Count);
            }

            while (randomAP == row0AP || randomAP == row1AP || randomAP == row2AP)
            {
                randomAP = Random.Range(0, animalList.Count);
            }

            print("RandomA = " + randomA + " and RandomAP = " + randomAP);

            if(currentRow == 0)
            {
                row0A = randomA;
                row0AP = randomAP;
            }
            else if(currentRow == 1)
            {
                row1A = randomA;
                row1AP = randomAP;
            }
            else if(currentRow == 2)
            {
                row2A = randomA;
                row2AP = randomAP;
            }
            else if(currentRow == 3)
            {
                row3A = randomA;
                row3AP = randomAP;
            }
        }
            PlaceAnimalsAndTheirHomes(); //Places animals in the shuffled locations
    }

    private void PlaceAnimalsAndTheirHomes()
    {
        for (int currentRow = 0; currentRow < animalList.Count; currentRow++) //Runs through the lists
        {

            if (currentRow == 0)
            {
                animalList[0].transform.position = animalLocationsList[row0A].transform.position; // Places animal 1 in the first random row for animals
                animalPenList[0].transform.position = animalPenLocationsList[row0AP].transform.position; //Places Pen 1 in the first random row for Pens
            }
            else if (currentRow == 1)
            {
                animalList[1].transform.position = animalLocationsList[row1A].transform.position;
                animalPenList[1].transform.position = animalPenLocationsList[row1AP].transform.position;
            }
            else if (currentRow == 2)
            {
                animalList[2].transform.position = animalLocationsList[row2A].transform.position;
                animalPenList[2].transform.position = animalPenLocationsList[row2AP].transform.position;
            }
            else if (currentRow == 3)
            {
                animalList[3].transform.position = animalLocationsList[row3A].transform.position;
                animalPenList[3].transform.position = animalPenLocationsList[row3AP].transform.position;
            }

        }
    }

}
