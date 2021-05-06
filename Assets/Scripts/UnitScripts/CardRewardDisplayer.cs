using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardRewardDisplayer : MonoBehaviour
{
    [Tooltip("Is this a spell card?")] public bool isSpellCard;

    [Tooltip("The scriptableobject holding this cards info")] public CardScript card;//Pulls the Scriptableobject, to get the refrences.
    [Tooltip("The scriptableobject holding this spell cards info")] public CardSpellScript spellCard;//Pulls the Scriptableobject, to get the refrences.

    public Image artworkImage; //The display image
    public TextMeshProUGUI nameText; //The name of the card
    public TextMeshProUGUI manatext; //The cost of the mana

    [HideInInspector] public int manaValue; //Accsess

    [ContextMenu("play")]
    public void Read()
    {
        if (!isSpellCard)
        {
            manaValue = card.manaCost;
            artworkImage.sprite = card.image;

            nameText.text = card.cardName;
            manatext.text = card.manaCost.ToString();
        }
        else if (isSpellCard)
        {
            manaValue = spellCard.manaCost;
            artworkImage.sprite = spellCard.image;

            nameText.text = spellCard.cardName;
            manatext.text = spellCard.manaCost.ToString();
        }
    }
}
