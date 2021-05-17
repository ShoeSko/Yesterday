using UnityEngine;
using TMPro;

public class TextMeshProColour : MonoBehaviour
{
    private TextMeshProUGUI text;
    [Header("Colour of text")]
    public Color colour;

    [Header("Outline of text")]
    public bool useOutline;
    public Color outlineColour;
    [Range(0,1)]public float outlineWitdh;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        text.color = colour;

        text.faceColor = colour;

        if (useOutline)
        {
            text.outlineColor = outlineColour;
            text.outlineWidth = outlineWitdh;
        }
    }
}
