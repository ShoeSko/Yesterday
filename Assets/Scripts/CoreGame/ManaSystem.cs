using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    public static int CurrentMana = 0;//your current mana
    public static int MaximumMana = 10;//maximum possible mana
    private float manaGainSpeed = 3f; //How long it takes to gain mana.

    public Slider manaSlider; //Slider to indicate mana gain
    public Text mana;//your current mana

    private float time;//clock
    private bool start;
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
            manaSlider.value = time;
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