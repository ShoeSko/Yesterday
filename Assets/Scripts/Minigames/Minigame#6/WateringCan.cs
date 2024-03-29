using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Slider waterMeter;
    public AudioSource waterSound;
    private bool waterSoundPlay;
    private bool minigameBeatForSound;
    private float startVolume;

    [Header("Plants")]
    public float wateringTimeNeeded;
    public List<GameObject> plantsA = new List<GameObject>();
    public List<GameObject> plantsD = new List<GameObject>();

    private Vector3 originForAim; //Where to aim from?
    private Vector3 directionForAim;//Which direction?
    private bool watered;//Has the plants been watered yet?

    [Header("Star system")]
    public List<GameObject> stars = new List<GameObject>(); //A list of the stars
    public GameObject blackstar1;
    public GameObject blackstar2;
    public GameObject blackstar3;
    public GameObject WinText;
    public GameObject ObjText;
    public GameObject FadeboxUI;
    private int starLenght;//How long is the star list
    private float scoreTimer; //How long it takes to do the game
    public int star1Time;
    public int star2Time;
    public int star3Time;

    [Header("Scene Transitions")]
    [SerializeField] private GameObject nextSceneButton; //The button to reach next scene
    [SerializeField] private LevelTransitionSystem levelTransitioner; //Refrence to give the score of the game.  
    private int score;


    //Tutorial stuff
    public List<GameObject> Texts = new List<GameObject>();
    private int Text;
    public GameObject BotWave;
    public GameObject SpeechBubble;
    public GameObject ContinueText;
    [SerializeField] private string alternativeAndroidText0;
    [SerializeField] private Text textToChage0;
    [SerializeField] private string alternativeAndroidText;
    [SerializeField] private Text textToChange;

    public List<AudioSource> VoiceDialogue = new List<AudioSource>();
    public List<AudioSource> VoiceWin = new List<AudioSource>();

    public GameObject objective;

    private bool GameStarted;

    [Header("Time Limit Minigame")]
    [SerializeField] private float minigameTimeLimit = 30f;
    private bool timeIsUp;
    private void Awake()
    {
        waterSound.Pause();
        startVolume = waterSound.volume * 6;
        print("The sound is awake and currently " + waterSound.isPlaying);

#if UNITY_ANDROID
        textToChage0.text = alternativeAndroidText0;
        textToChange.text = alternativeAndroidText;
#endif
        GameStarted = true;
    }
    private void Start()
    {
        starLenght = stars.Count;
        if (MinigameSceneScript.Tutorial == false)
        {
            GameStarted = true;
            Water.Play();
        }
        else//Tutorial stuff
        {
            TutorialDialogueVoice();
            Text = 0;
            objective.SetActive(false);

            Texts[Text].SetActive(true);
            BotWave.SetActive(true);
            SpeechBubble.SetActive(true);
            ContinueText.SetActive(true);
        }
        ObjText.SetActive(true);
        WinText.SetActive(false);
        FadeboxUI.SetActive(false);

        waterMeter.maxValue = wateringTimeNeeded; //Makes sure the meter has the same max value as the watering should occur.

        if (!MinigameSceneScript.Tutorial)
        {
            StartCoroutine(TimerForMinigame());
        }
    }

    private void Update()
    {

        if(GameStarted == true)
        {
            if (!watered)
            {
                WateringcanControlls();
                WateringTime();
            }
        }

        //Tutorial stuff
        if(MinigameSceneScript.Tutorial == true)
        {
            if(Text == 0)
            {
                ObjText.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space))
                {
                    GameStarted = true;

                    TutorialDialogueVoice();

                    Texts[Text].SetActive(false);
                    Text++;
                    Texts[Text].SetActive(true);
                }
            }
            else
            {
                if(Text == 1)
                {
#if UNITY_ANDROID
                    ContinueText.SetActive(false);
                    if ((Input.acceleration.x < -rotDeadZone || Input.acceleration.x > rotDeadZone))
                    {
                        objective.SetActive(true);
                        Texts[Text].SetActive(false);
                        BotWave.SetActive(false);
                        SpeechBubble.SetActive(false);
                        Water.Play();
                        ObjText.SetActive(true);
                    }
#else
                    ContinueText.SetActive(false);
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                    {
                        objective.SetActive(true);
                        Texts[Text].SetActive(false);
                        BotWave.SetActive(false);
                        SpeechBubble.SetActive(false);
                        Water.Play();
                        ObjText.SetActive(true);
                    }
#endif
                }
            }
        }
        LowerTheVolume();
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
            waterMeter.value += Time.deltaTime; //Increases the meter as fast as the time decreases.
            if (!waterSoundPlay)
            {
                PlayWaterSound();
            }
        }
        else
        { 
            Water.enableEmission = false;
            if (waterSound.isPlaying)
            {
                PlayWaterSound();
            }
        } //Turns of emission when not watering plants (Very visual when you are doing it correctly)

        if (wateringTimeNeeded <= 0 || timeIsUp)
        {
            print("I do stuff");
            WateredPlants();
        }
    }

    private void WateredPlants() //Plants have been watered enough, end the game.
    {
        if(MinigameSceneScript.Tutorial == false)
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
                blackstar3.SetActive(true);
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
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
                //1 star
                CardReward.Stars = 1;
                score = 1;
            }
            else if (scoreTimer > star3Time)
            {
                blackstar1.SetActive(true);
                blackstar2.SetActive(true);
                blackstar3.SetActive(true);
            }
                WinText.SetActive(true);
                ObjText.SetActive(false);
            FadeboxUI.SetActive(true);
                
        }
        else//Tutorial stuff
        {
            for (int i = 0; i < 2; i++)//Brings the plants to life
            {
                plantsA[i].SetActive(true);
                plantsD[i].SetActive(false);
            }

            for (int i = 0; i < starLenght; i++)
            {
                stars[i].SetActive(true);
            }
            //3 stars
            CardReward.Stars = 3;
            score = 3;

            TutorialWinVoice();
            Text = 2;
            Texts[Text].SetActive(true);
            BotWave.SetActive(true);
            SpeechBubble.SetActive(true);
            objective.SetActive(false);
            CardReward.TutorialMission = 3;
        }

        Water.Stop();
        minigameBeatForSound = true;
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

    IEnumerator TimerForMinigame()
    {
        yield return new WaitForSeconds(minigameTimeLimit);

        timeIsUp = true;
        watered = true;

        WateredPlants();
    }

    private void PlayWaterSound()
    {
        if (!waterSoundPlay)
        {
            waterSound.UnPause();
            waterSoundPlay = true;
            print("Me now");
        }
        else if(waterSoundPlay)
        {
            waterSound.Pause();
            waterSoundPlay = false;
        }
    }
    private void LowerTheVolume()
    {
        if (minigameBeatForSound)
        {
            startVolume -= Time.deltaTime;
            waterSound.volume = startVolume / 6;
        }

        print("Bool is now " + waterSoundPlay);
    }

    private void TutorialDialogueVoice()
    {
        int sound = VoiceDialogue.Count;

        AudioSource PlaySound = VoiceDialogue[Random.Range(0, sound)];

        VoiceDialogue.Remove(PlaySound);
        PlaySound.Play();
    }

    private void TutorialWinVoice()
    {
        int sound = VoiceWin.Count;

        AudioSource PlaySound = VoiceWin[Random.Range(0, sound)];

        VoiceWin.Remove(PlaySound);
        PlaySound.Play();
    }
}
