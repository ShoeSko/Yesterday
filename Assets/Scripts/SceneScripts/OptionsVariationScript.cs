using UnityEngine;
using TMPro;

public class OptionsVariationScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI giveUpText;
    [SerializeField] private string giveUpTextVariation;

    private void Awake()
    {
        if (MinigameSceneScript.Tutorial && giveUpText.enabled)
        {
            ChangeTheText();
        }
    }

    private void ChangeTheText()
    {
        giveUpText.text = giveUpTextVariation;
    }
}
