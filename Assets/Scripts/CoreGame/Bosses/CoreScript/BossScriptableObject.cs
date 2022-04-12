using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossScriptableObject", menuName = "BossCore")]

public class BossScriptableObject : ScriptableObject
{
    [Header("Parameters")]
    public string Name;
    public int BossHealth;

    [Header("Visuals")]
    public List<Sprite> BossSprites = new List<Sprite>();
    public Color BossSliderColor;

    [Header("Abilities")]
    public int UseAbilityDelay;
    public List<int> AbilityCooldown = new List<int>();
    public List<int> StartAbilityCooldown = new List<int>();

    [Header("Audio")]
    public List<AudioClip> AbilitySFX = new List<AudioClip>();
    public AudioClip Soundtrack;

    [Header("Physics")]
    public float Xpos;
    public float Ypos;
    public float IntroSpeed;
    public float ScaleModifier;

}
