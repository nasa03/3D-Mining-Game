using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Toptext UI component

public class TopTextUI : MonoBehaviour
{
    TextMeshProUGUI text;
    ColorChange colorchange;
    PlayerScript player;

    public enum TextUIEnum { Cash, Xp}
    public TextUIEnum textUI;
    private bool isEnabled;
    private bool isPositive;
    private double UIValue;
    private double carriedValue;
    private double amountPerTick;
    private double _amountPerTick;
    private string UIText;

    void Start()
    {
        player = Gamemanager.main.getLocalPlayer();
        text = GetComponent<TextMeshProUGUI>();
        text.text = TextFormat(textUI);
        UIValue = carriedValue;

        //Components for Top Text UI
        colorchange = GetComponent<ColorChange>();
    }

    void Update()
    {
        //Changes its text 
        text.text = TextFormat(textUI);
        if (isEnabled) { 
            isPositive = UIValue <= carriedValue;
            UpdateTextUi(isPositive);

            // For ColorChange.cs Component
            if (colorchange != null)
                colorchange.SetColor(isPositive);
        }
    }

    private void UpdateTextUi(bool isPositive)
    {
        if (Gamemanager.main.isTick)
        {
            if (isPositive)
            {
                UIValue += amountPerTick;
                if (UIValue > carriedValue)
                {
                    disableTextUI();
                }
            }
            else
            {
                UIValue -= amountPerTick;
                if (UIValue < carriedValue)
                {
                    disableTextUI();
                }
            }
        }
    }

    // Sets the UI values and returns the text, which the Text Component recieves.
    private string TextFormat(TextUIEnum textUI)
    {
        switch (textUI)
        {
            case TextUIEnum.Cash:
                carriedValue = player.cash;
                return "$ " + GameFormat.toScientificNotation(UIValue);

            case TextUIEnum.Xp:
                carriedValue = player.xp.currentXP;
                return "XP: " + GameFormat.toScientificNotation(UIValue) + "/" + GameFormat.toScientificNotation(player.xp.nextXP);
        }
        return "";
    }

    // Enables the Top Text UI Component
    public void enableTextUI(double amount)
    {
        isEnabled = true;
        setTick(amount);
    }

    // Disables the Top Text UI Component
    public void disableTextUI()
    {
        UIValue = carriedValue;
        amountPerTick = 0;
        isEnabled = false;
    }

    // Sets the tick that the UI text gets
    void setTick(double amount)
    {
        _amountPerTick = System.Math.Round(amount / 50);
        if (_amountPerTick <= 0)
            _amountPerTick = 1;

        amountPerTick += _amountPerTick;
    }
}
