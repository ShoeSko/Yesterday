using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplayer : MonoBehaviour
{
    [Tooltip("The scriptableobject holding this cards info")]public CardScript card;//Pulls the Scriptableobject, to get the refrences.

    public Image artworkImage; //The display image
    public TextMeshProUGUI nameText; //The name of the card
    public TextMeshProUGUI manatext; //The cost of the mana
    public TextMeshProUGUI descriptionText; // what does it do
    public TextMeshProUGUI flavorTexts; //Flavor

    private void Start()
    {
        artworkImage.sprite = card.image;

        nameText.text = card.cardName;
        manatext.text = card.manaCost.ToString();
        descriptionText.text = card.effectDescription;
        flavorTexts.text = card.flavorTxt;
    }
}
