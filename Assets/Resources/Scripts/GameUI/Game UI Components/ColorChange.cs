using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Color Changer. Assign it to a text component to change its text color!

public class ColorChange : MonoBehaviour
{
    TextMeshProUGUI text;
    Text textAlt;
    public Color positiveColor;
    public Color negativeColor;
    Color currentColor;
    Color selectedColor;
    public float duration;
    float savedDuration;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            textAlt = GetComponent<Text>();
            currentColor = textAlt.color;
        }
        else
            currentColor = text.color;

        savedDuration = duration;
    }

    void Update()
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime * 4;
        }
        else
        {
            duration = 0;
        }

        if(text != null)
            text.color = Color.Lerp(currentColor, selectedColor, duration);
        else
            textAlt.color = Color.Lerp(currentColor, selectedColor, duration);
    }

    public void SetColor(bool isPositive)
    {
        selectedColor = (isPositive ? positiveColor : negativeColor);
        duration = savedDuration;
    }
}
