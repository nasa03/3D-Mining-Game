using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    //The Default Value for our stat
    private float _baseValue;
    public float baseValue
    {
        get { return _baseValue; }
        set { _baseValue = value; }
    }

    //The Increase Value. Each level up adds a stack and gets calculated in the Final Value.
    private float _increaseValue;
    public float increaseValue
    {
        get { return _increaseValue; }
        set { _increaseValue = value; }
    }

    //The level of the stat. Default should be 1. 
    private int _level;
    public int level
    {
        get { return _level; }
        set { _level = value; }
    }

    //The maximum level that can be reached. 
    private int _maxLevel;
    public int maxLevel
    {
        get { return _maxLevel; }
        set { _maxLevel = value; }
    }

    //The definition of the stat.
    private string _statType;
    public string statType
    {
        get { return _statType; }
        set { _statType = value; }
    }

    //The format for the stat. Example: s -> seconds, b -> blocks, % -> Percent.
    private string _format;
    public string format
    {
        get { return _format; }
        set { _format = value; }
    }

    //The amount the stat will cost for each levelup
    private float _baseCost;
    public float baseCost
    {
        get { return _baseCost; }
        set { _baseCost = value; }
    }

    //The increment for each levelup cost.
    private float _increaseCost;
    public float increaseCost
    {
        get { return _increaseCost; }
        set { _increaseCost = value; }
    }

    private float _finalCost;
    public float finalCost
    {
        get { return _finalCost; }
        set { _finalCost = value; }
    }

    //A list of modifiers. Self explainatory
    private List<StatModifier> _modifiers;
    public List<StatModifier> modifiers
    {
        get { return _modifiers; }
        private set { _modifiers = value; }
    }

    //A total of every level-based calculations and attached modifiers.
    private float _finalValue;
    public float finalValue
    {
        get { return _finalValue; }
        private set { _finalValue = value; }
    }

    public PlayerStats(string type, string format, float baseValue, float addValue, int level, int maxLevel, float cost, float addCost)
    {
        this.statType = type;
        this.format = format;
        this.baseValue = baseValue;
        this.increaseValue = addValue;
        this.level = level;
        this.maxLevel = maxLevel;
        this.baseCost = cost;
        this.increaseCost = addCost;

        modifiers = new List<StatModifier>();
        finalValue = baseValue;

        calculateAll();
    }

    public void levelUp(int level)
    {
        this.level += level;
        calculateAll();
    }

    public void addModifier(StatModifier mod) {
        if(modifiers.Contains(mod)) {
            mod.stack += 1;
            return;
        }
        modifiers.Add(mod);
        calculateAll();
    }

    public void subtractModifier(StatModifier mod) {
        if (modifiers.Contains(mod) && mod.stack > 1)
        {
            mod.stack -= 1;
            return;
        }
        calculateAll();
    }

    public void removeModifier(StatModifier mod) {
        if(modifiers.Contains(mod))
        {
            modifiers.Remove(mod);
            return;
        }
        calculateAll();
    }

    public List<StatModifier> getModifiers() {
        return modifiers;
    }

    public float calculateFinal() {
        finalValue = baseValue + increaseValue * (level - 1);

        for (int i = 0; i < modifiers.Count; i++)
        {
            switch (modifiers[i].operant)
            {
                case Operant.add:
                    finalValue += modifiers[i].combineValue();
                break;

                case Operant.percent:
                    finalValue *= (1 + modifiers[i].combineValue());
                    break;

                default:
                    finalValue += modifiers[i].combineValue();
                break;
            }
        }
        return finalValue;
    }

    public float calculatePrice()
    {
        finalCost = Mathf.FloorToInt(baseCost + increaseCost * (level - 1) * (1 + (0.05f * (level - 1))));
        return finalCost;
    }

    public void calculateAll()
    {
        calculatePrice();
        calculateFinal();
    }

    override
    public string ToString()
    {
        string modifierText = "";

        for (int i = 0; i<modifiers.Count; i++)
        {
            modifierText += modifiers[i].modifierName + " Modifier: x\t" + modifiers[i].finalValue + modifiers[i].format + "\n";
        }
         
        string text = "<b>" + statType + "</b>\n" + "Base " + statType + ": x\t" + baseValue + format + 
            "\n" + modifierText + "Final " + statType + ": x\t" + finalValue + format + "\n\n";
        return text;
    }


}
