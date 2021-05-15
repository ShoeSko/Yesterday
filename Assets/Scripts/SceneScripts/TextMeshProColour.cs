using UnityEngine;
using TMPro;

public class TextMeshProColour : MonoBehaviour
{
    private TextMeshProUGUI text;
    public Color colour;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        text.color = colour;
    }
}
