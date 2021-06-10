using UnityEngine;
using UnityEngine.UI;

public class CreditsLeaveLoad : MonoBehaviour
{
    public float loadSpeed;
    public float timeBeforeIconDissapears;

    public Animator hamsterAnimator;
    public Image circleImage;

    private float loadValue;
    private float loadLeftTime;
    private CanvasGroup canvasGroup;
    private bool hamsterIsRunning;
    private bool isQuittingCredits;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        HamsterLoad();
    }

    private void HamsterLoad()
    {
        if(Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Escape))
        {
            canvasGroup.alpha = 1;
            loadLeftTime = timeBeforeIconDissapears;
            if (!hamsterIsRunning)
            {
                hamsterAnimator.SetTrigger("Charge");
                hamsterIsRunning = true;
            }

            loadValue += Time.deltaTime / loadSpeed;

            circleImage.fillAmount = loadValue;
        }
        else
        {
            if (hamsterIsRunning)
            {
                hamsterAnimator.SetTrigger("Full");
                hamsterIsRunning = false;
            }
            if(loadLeftTime <= 0)
            {
                circleImage.fillAmount = 0;
                canvasGroup.alpha = 0;
                loadValue = 0;
            }
            else
            {
                loadLeftTime -= Time.deltaTime;
            }
        }

        if(loadValue >= 1)
        {
            if (!isQuittingCredits)
            {
                BackToMainMenuFromCredits();
                isQuittingCredits = true;
            }
        }
    }

    private void BackToMainMenuFromCredits()
    {
        if (FindObjectOfType<LevelTransitionSystem>())
        {
            FindObjectOfType<LevelTransitionSystem>().GameOverButtonPress();
        }
    }
}
