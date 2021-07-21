using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    public static int CurrentMana = 0;//your current mana
    public static int MaximumMana = 10;//maximum possible mana
    public float manaGainSpeed = 3f; //How long it takes to gain mana (seconds).

    public Text mana;//your current mana

    private float time;//clock
    private bool start;

    [Header("Hamster Animation Variables")]
    [SerializeField] private Animator hamsterAnimation;
    private bool isHamsterRunning;

    [SerializeField] private Image hamsterCircle;

    void Start()
    {
        CurrentMana = 0; // Resets the mana value for a fresh game.
    }
    void Update()
    {
        mana.text = CurrentMana + "/" + MaximumMana;//this is the text visualizing how much mana the player has

        ManaGainVisual();
    }

    private void ManaGainVisual()
    {
        if(CurrentMana < MaximumMana && start)
        {
            time += Time.deltaTime;

            hamsterCircle.fillAmount = time / manaGainSpeed; //Theory of Math. Circle goes to a max of 1, want the time to be represented in this with the circle going at that speed.

            if (!isHamsterRunning)
            {
                Debug.Log("I Hamster Now");
                hamsterAnimation.SetTrigger("Charge");
                isHamsterRunning = true;
            }
        }

        if (CurrentMana >= MaximumMana)
        {
            if (isHamsterRunning)
            {
                hamsterAnimation.SetTrigger("Full");
                isHamsterRunning = false;
            }
        }

        if(time >= manaGainSpeed)
        {
            CurrentMana++;
            time = 0;
        }

        if(CurrentMana < 0) { CurrentMana++; } //Prevents negative Mana
    }

    public void GameStarted()
    {
        start = true;
    }

    [ContextMenu("Cheat for full mana")]
    private void ManaCheat()
    {
        CurrentMana = 99;//Cheat amount of mana
    }
}