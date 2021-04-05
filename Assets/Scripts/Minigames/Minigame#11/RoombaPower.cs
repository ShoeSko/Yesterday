using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoombaPower : MonoBehaviour
{
    #region Variables
    [Header("Material Looks")]
    public Material powerOn;
    [Header("List of the wires to be used")]
    public List<GameObject> connection0 = new List<GameObject>();
    public List<GameObject> connection1 = new List<GameObject>();
    public List<GameObject> connection2 = new List<GameObject>();
    public List<GameObject> connection3 = new List<GameObject>();

    [Header("Switches being used")]
    public List<GameObject> switches1 = new List<GameObject>();
    public GameObject switch2;
    public List<GameObject> switches3 = new List<GameObject>();

    private bool firstSwitch;
    private bool secondSwitch;
    private bool thirdSwitch;

    [Header("Star system")]
    public List<GameObject> stars = new List<GameObject>(); //A list of the stars
    private int starLenght;//How long is the star list
    private float scoreTimer; //How long it takes to do the game
    public int star1Time;
    public int star2Time;
    public int star3Time;

    [Header("The Roomba")]
    public GameObject roombaObject;
    public int roombaSpeed;
    private int roombaflipInt = 1;

    private int randomiser1;
    private int randomiser2;
    #endregion

    private void Start()
    {
        randomiser1 = Random.Range(0, 3);
        randomiser2 = Random.Range(0, 3);
        connection0[randomiser1].GetComponent<MeshRenderer>().material = powerOn; //Choses the first wire.
        starLenght = stars.Count;
        for (int i = 0; i < starLenght; i++)
        {
            stars[i].SetActive(false);
        }
    }
    private void Update()
    {
        scoreTimer += Time.deltaTime;
        WhichSwitch();
    }

    private void WhichSwitch()
    {
        //Switch 1
        if(switches1[randomiser1].transform.eulerAngles.z == 90)
        {
            ActivateFirstConnection();
            firstSwitch = true;
            switches1[randomiser1].layer = 2; //Gives the object ignore raycast, prevents more flipping.
        }

        //Switch 2
        if (switch2.transform.eulerAngles.z == 90 && firstSwitch)
        {
            ActivateSecondConnection();
            secondSwitch = true;
            switch2.layer = 2;
        }

        //Switch 3
        if (switches3[randomiser2].transform.eulerAngles.z == 90 && secondSwitch)
        {
            ActivateThirdConnection();
            switches3[randomiser2].layer = 2;
            RoombaTime();
        }
    }

    private void RoombaTime()
    {
        if (!thirdSwitch)
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
        }
        RoombaMove();

        thirdSwitch = true;//Prevents star script from repeating.
    }

    private void RoombaMove() //Tiny piece to make the roomba goe back and forth after completing the circuit
    {
        Rigidbody2D roombaRD= roombaObject.GetComponent<Rigidbody2D>();
        roombaRD.velocity = new Vector2(roombaSpeed * roombaflipInt, 0);
        if (roombaObject.transform.position.x > 7.5)
        {
            roombaflipInt = -1;
        }
        else if (roombaObject.transform.position.x < -7.5)
        {
            roombaflipInt = 1;
        }
    }
    #region Connections
    private void ActivateFirstConnection() //Turns the wires on (Might just be a object with 2 anim states instead
    {
            connection1[randomiser1].GetComponent<MeshRenderer>().material = powerOn;
    }
    private void ActivateSecondConnection()
    {
            connection2[randomiser2].GetComponent<MeshRenderer>().material = powerOn;
    }
    private void ActivateThirdConnection()
    {
            connection3[randomiser2].GetComponent<MeshRenderer>().material = powerOn;
    }
    #endregion
}
