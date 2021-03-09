using UnityEngine;

[CreateAssetMenu(fileName ="New Card", menuName = "Card")]
public class CardScript : ScriptableObject
{
    public string cardName;

    public Sprite image;
    public int manaCost;
    public string effectDescription;
    public string flavorTxt;
}