using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaSystem : MonoBehaviour
{
    public GameObject manaBox;//black box inicating mana filling up
    private Vector2 startingPos;//starting position of fillable manabox ^
    private Vector2 offset;//this is used to move the fillable manabox
    public static int CurrentMana = 0;//your current mana
    public static int MaximumMana = 10;//maximum possible mana

    public Text mana;//your current mana

    private float speed = 0.5f;//speed how fast mana fills up // Current formula: 2/speed // current speed at which mana fills up: 4 seconds
    private float time;//clock
    private bool start;
    void Start()
    {
        CurrentMana = 0; // Resets the mana value for a fresh game.
        startingPos = manaBox.transform.position;

        offset = new Vector2(0, 2);
    }
    void Update()
    {
        mana.text = CurrentMana + "/" + MaximumMana;//this is the text visualizing how much mana the player has

        if (CurrentMana < MaximumMana && start)//cap the mana recovery at maximum mana
        {
            time += Time.deltaTime;
            manaBox.transform.position = Vector2.MoveTowards(manaBox.transform.position, startingPos + offset, speed * Time.deltaTime);
        }

        if(time >= 4)//time after getting 1 mana. (When changing mana recovery speed, change this manually acordingly after the formula)
        {
            manaBox.transform.position = startingPos;
            CurrentMana++;
            time = 0;
        }

        if(CurrentMana < 0)//This ensures that the player cannot go below 0 mana when fighting "The Corporate" boss
            CurrentMana++;

    }
    public void GameStarted()
    {
        start = true;
    }
}