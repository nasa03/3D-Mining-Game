using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class XPSystem
{
    public float currentXP { get; private set; }

    private float baseXP;

    private int levelGained;

    public float nextXP { get; private set; }

    public float nextXPMultiplier { get; private set; }

    public int level { get; private set; }

    public int highestLevel { get; private set; }

    public int perkpoint { get; private set; }

    public float multiplier { get; private set; }

    public XPSystem (int level, int highestLevel, float baseXP, float nextXPMultiplier)
    {
        this.level = level;
        this.highestLevel = highestLevel;
        this.baseXP = baseXP;
        nextXP = baseXP;
        this.nextXPMultiplier = nextXPMultiplier;
    }

    public void GiveXP(float amount)
    {
        if (amount == 0)
            return;

        currentXP += amount;
        CheckIfOverlevel();
        GameUITop.main.UpdateXpUI(currentXP);
    }

    public void GivePerkPoint(int amount)
    {
        perkpoint += amount;
    }

    void CheckIfOverlevel()
    {
        levelGained = 0;
        while (currentXP >= nextXP)
        {
            levelGained++;
            currentXP -= nextXP;
            //CalcluateNextXP();
            nextXP = Mathf.Floor(baseXP * (1 + (level + levelGained - 1) * 0.25f) * nextXPMultiplier);
        }

        if(levelGained > 0)
            Levelup(levelGained);
    }

    public void Levelup(int amount)
    {
        level += amount;
        if (highestLevel <= level)
        {
            highestLevel = level;
            GivePerkPoint((level % 5 == 0  && amount == 1? 1 : 0) + Mathf.FloorToInt(amount / 5));
        }
           
    }

}
