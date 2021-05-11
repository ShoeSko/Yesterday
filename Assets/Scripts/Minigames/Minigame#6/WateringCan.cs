using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WateringCan : MonoBehaviour
{
    [Header("Wateringcan settings")]
    [Range(-0.5f, 0.5f)] public float rotDeadZone = 0.1f;
    public Transform wateringTrans;
    [Range(0,5)]public float rotationsSpeed = 1;
    public Vector3 rotationalDirection;
    [SerializeField] private LayerMask targetLayer;
    public GameObject water;
    public ParticleSystem Water;

    [Header("Plants")]
    public float wateringTimeNeeded;
    public List<GameObject> plantsA = new List<GameObject>();
    public List<GameObject> plantsD = new List<GameObject>();

    private Vector3 originForAim; //Where to aim from?
    private Vector3 directionForAim;//Which direction?
    private bool watered;//Has the plants been watered yet?

    [Header("Star system")]
    public List<GameObject> stars = new List<GameObject>(); //A list of the stars
    private int starLenght;//How long is the star list
    private float scoreTimer; //How long it takes to do the game
    public int star1Time;
    public int star2Time;
    public int star3Time;

    [Header("Scene Transitions")]
    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int score;

    private void Start()
    {
        starLenght = stars.Count;
        Water.Play();
    }

    private void Update()
    {
        if (!watered)
        {
        WateringcanControlls();
        WateringTime();
        }
    }

    private void WateringTime()//How long to water the plants + raycast for knowing when to water
    {
        scoreTimer += Time.deltaTime; //Score timer

        originForAim = transform.position;
        directionForAim = transform.right;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(originForAim, directionForAim, targetLayer);
        if (hit)
        {
            Water.enableEmission = true; //Turn on water emission
            wateringTimeNeeded -= Time.deltaTime; //Counts down how long one has been watering the plants
        }
        else { Water.enableEmission = false; } //Turns of emission when not watering plants (Very visual when you are doing it correctly)

        if (wateringTimeNeeded <= 0)
        {
            WateredPlants();
        }
    }

    private void WateredPlants() //Plants have been watered enough, end the game.
    {
        for (int i = 0; i < 2; i++)//Brings the plants to life
        {
            plantsA[i].SetActive(true);
            plantsD[i].SetActive(false);
        }

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
        else
        {
            //0 stars
        }
        Water.Stop();
        watered = true; //Plants have now been watered

        nextSceneButton.SetActive(true);
        levelTransitioner.currentMinigameScore = score;
    }

    private void WateringcanControlls()
    {
#if UNITY_ANDROID //Everything within this, only works if the build is android.
        if (Input.acceleration.x < -rotDeadZone)//move left with phone rotation
            wateringTrans.Rotate(rotationalDirection * (rotationsSpeed * Time.deltaTime));//Rotates the object to the left

        else if (Input.acceleration.x > rotDeadZone)//move right with phone rotation
            wateringTrans.Rotate(-rotationalDirection * (rotationsSpeed * Time.deltaTime)); //Rotates the object to the right

        //else if (Input.acceleration.x > -rotDeadZone || Input.acceleration.x < rotDeadZone) //Stops the wateringcan from rotating

#else
        if (Input.GetKey(KeyCode.A))//rotate left
            wateringTrans.Rotate(rotationalDirection * (rotationsSpeed * Time.deltaTime));

        else if (Input.GetKey(KeyCode.D))//rotate right
            wateringTrans.Rotate(-rotationalDirection * (rotationsSpeed * Time.deltaTime));
#endif
    }
}
