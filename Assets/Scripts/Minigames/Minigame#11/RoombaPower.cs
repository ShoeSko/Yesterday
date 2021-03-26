using System.Collections.Generic;
using UnityEngine;

public class RoombaPower : MonoBehaviour
{
    #region Variables
    [Header("Material Looks")]
    public Material powerOn;
    [Header("List of the wires to be used")]
    public List<GameObject> connection1 = new List<GameObject>();
    public List<GameObject> connection2 = new List<GameObject>();
    public List<GameObject> connection3 = new List<GameObject>();
    private int conncetion1Count;
    private int conncetion2Count;
    private int conncetion3Count;
    [Header("Switches being used")]
    public List<GameObject> switches = new List<GameObject>();

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
    #endregion

    private void Start()
    {
        starLenght = stars.Count;
        for (int i = 0; i < starLenght; i++)
        {
            stars[i].SetActive(false);
        }
        conncetion1Count = connection1.Count;
        conncetion2Count = connection2.Count;
        conncetion3Count = connection3.Count;

    }
    private void Update()
    {
        WhichSwitch();
    }

    private void WhichSwitch()
    {
        //Switch 1
        if(switches[0].transform.eulerAngles.z == 90)
        {
            ActivateFirstConnection();
            firstSwitch = true;
            switches[0].layer = 2; //Gives the object ignore raycast, prevents more flipping.
        }

        //Switch 2
        if (switches[1].transform.eulerAngles.z == 90 && firstSwitch)
        {
            ActivateSecondConnection();
            secondSwitch = true;
            switches[1].layer = 2;
        }

        //Switch 3
        if (switches[2].transform.eulerAngles.z == 90 && secondSwitch)
        {
            ActivateThirdConnection();
            switches[2].layer = 2;
            RoombaTime();
        }
    }

    private void RoombaTime()
    {
        scoreTimer += Time.deltaTime;
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

        for (int i = 0; i < conncetion1Count; i++)
        {
            connection1[i].GetComponent<MeshRenderer>().material = powerOn;
        }
    }
    private void ActivateSecondConnection()
    {

        for (int i = 0; i < conncetion2Count; i++)
        {
            connection2[i].GetComponent<MeshRenderer>().material = powerOn;
        }
    }
    private void ActivateThirdConnection()
    {

        for (int i = 0; i < conncetion3Count; i++)
        {
            connection3[i].GetComponent<MeshRenderer>().material = powerOn;
        }
    }
    #endregion
}
