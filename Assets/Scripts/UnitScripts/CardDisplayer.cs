using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplayer : MonoBehaviour
{
    //[Tooltip("Is this a spell card?")]public bool isSpellCard;
    [Tooltip("Does it use a background?")] public bool useBackground;

    [Tooltip("The scriptableobject holding this cards info")]public CardScript card;//Pulls the Scriptableobject, to get the refrences.
    [Tooltip("The scriptableobject holding this spell cards info")]public CardSpellScript spellCard;//Pulls the Scriptableobject, to get the refrences.

    public Image artworkImage; //The display image
    public Image backgroundImage; //The background image
    public TextMeshProUGUI nameText; //The name of the card
    public TextMeshProUGUI manatext; //The cost of the mana
    public TextMeshProUGUI descriptionText; // what does it do
    public TextMeshProUGUI descriptionText2;
    public TextMeshProUGUI descriptionText3;
    public TextMeshProUGUI flavorTexts; //Flavor

    [HideInInspector]public GameObject UnitPrefab; //Stores the prefab
    [HideInInspector]public int manaValue; //Accsess

    [ContextMenu("play")]
    public void Read()
    {
        //if (!isSpellCard)
        //{
            manaValue = card.manaCost;
            artworkImage.sprite = card.image;
            UnitPrefab = card.Prefab;

            nameText.text = card.cardName;
            manatext.text = card.manaCost.ToString();
            descriptionText.text = card.effectDescription;
            descriptionText2.text = card.effectDescription2;
            descriptionText3.text = card.effectDescription3;
            flavorTexts.text = card.flavorTxt;

            if (useBackground)
            {
                backgroundImage.sprite = card.backgroundImage;
            }
        //    }
        //    else if (isSpellCard)
        //    {
        //        manaValue = spellCard.manaCost;
        //        artworkImage.sprite = spellCard.image;
        //        UnitPrefab = spellCard.Prefab;

        //        nameText.text = spellCard.cardName;
        //        manatext.text = spellCard.manaCost.ToString();
        //        descriptionText.text = spellCard.effectDescription;
        //        //flavorTexts.text = spellCard.flavorTxt;// Is flavor needed on spell cards?
        //    }
    }
}