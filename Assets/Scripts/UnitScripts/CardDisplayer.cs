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

    [HideInInspector]public GameObject UnitPrefab; //Stores the prefab
    [HideInInspector]public int manaValue; //Accsess
    private void Start()
    {

    }

    public void Read()
    {
        manaValue = card.manaCost;
        artworkImage.sprite = card.image;
        UnitPrefab = card.Prefab;

        nameText.text = card.cardName;
        manatext.text = card.manaCost.ToString();
        descriptionText.text = card.effectDescription;
        flavorTexts.text = card.flavorTxt;
    }
}
