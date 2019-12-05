using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Base class for Stats
public abstract class Stats
{
    public abstract float baseValue { get; protected set; }
    public abstract int level { get; protected set; }
    public abstract int maxLevel { get; protected set; }
    public abstract string type { get; protected set; }
    public abstract string format { get; protected set; }
    public abstract float finalValue { get; protected set; }

    public void CheckIfMaxLevel()
    {
        if (level > maxLevel)
            level = maxLevel;
    }

    public void LevelUp(int amount)
    {
        if (maxLevel <= level)
        {
            return;
        }
        level += amount;
        CheckIfMaxLevel();
    }

    public void LevelDown(int amount)
    {
        if(level > 1)
        {
            level -= amount;
        }
        CheckIfMaxLevel();
    }
    public abstract float CalculateFinal();
}

[System.Serializable]
public class PlayerStats : Stats
{
    //The Default Value for our stat
    public override float baseValue { get; protected set; }

    //The Increase Value. Each level up adds a stack and gets calculated in the Final Value.
    public float increaseValue { get; set; }

    //The level of the stat. Default should be 1. 
    public override int level { get; protected set; }

    //The maximum level that can be reached. 
    public override int maxLevel { get; protected set; }

    //The definition of the stat.
    public override string type { get; protected set; }

    //The format for the stat. Example: s -> seconds, b -> blocks, % -> Percent.
    public override string format { get; protected set; }

    //The amount the stat will cost for each levelup
    public float baseCost { get; set; }

    //The increment for each levelup cost.
    public float increaseCost { get; set; }

    // The totalcost
    public float finalCost { get; set; }

    //A list of modifiers. Self explainatory
    public List<StatModifier> modifiers { get; private set; }

    //A total of every level-based calculations and attached modifiers.
    public override float finalValue { get; protected set; }

    public PlayerStats(string type, string format, float baseValue, float addValue, int level, int maxLevel, float cost, float addCost)
    {
        this.type = type;
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

    // Leveling up Stat by purchasing currency
    public void BuyLevelUp(PlayerScript player, int level)
    {
        if(maxLevel <= this.level)
            return;

        if (player.cash >= finalCost)
            player.GiveCash(-finalCost);

        LevelUp(level);
        calculateAll();
    }

    //Adds a stat modifier to the stat
    public void AddModifier(StatModifier mod, int amount) {

        if (!modifiers.Contains(mod)) 
            modifiers.Add(mod);

        mod.LevelUp(amount);
        calculateAll();
    }

    //Subtracts a stat modifier to the stat
    public void SubtractModifier(StatModifier mod, int amount) {
        if (modifiers.Contains(mod) && mod.level > 1)
        {
            mod.LevelDown(amount);
            if (mod.level < 1)
            {
                modifiers.Remove(mod);
            }
        }
        calculateAll();
    }

    //Retrieve every assigned modifier
    public List<StatModifier> GetModifiers() {
        return modifiers;
    }

    //Calculate the final value for the stat
    public override float CalculateFinal() {
        finalValue = baseValue + increaseValue * (level - 1);

        for (int i = 0; i < modifiers.Count; i++)
        {
            switch (modifiers[i].operant)
            {
                case Operant.add:
                    finalValue += modifiers[i].CalculateFinal();
                break;

                case Operant.percent:
                    finalValue *= (1 + modifiers[i].CalculateFinal());
                    break;

                default:
                    finalValue += modifiers[i].CalculateFinal();
                break;
            }
        }
        return finalValue;
    }

    //Calculate the price for the stats currency
    public float CalculatePrice()
    {
        //Current formular: basecCost + increaseCost * level * (level * 0.05)
        finalCost = Mathf.FloorToInt(baseCost + increaseCost * (level - 1) * (1 + (0.05f * (level - 1))));
        return finalCost;
    }

    //Qick way to calculate stuff by inserting methods
    public void calculateAll()
    {
        SortModifier();
        CalculatePrice();
        CalculateFinal();
    }

    //Sort modifier based on Operant enum
    void SortModifier()
    {
        modifiers = modifiers.OrderBy(o => o.operant).ToList();
    }

    override
    public string ToString()
    {
        string modifierText = "";

        for (int i = 0; i<modifiers.Count; i++)
        {
            modifierText += modifiers[i].modifierName + " Modifier: x\t" + GameFormat.toScientificNotation(modifiers[i].finalValue) + modifiers[i].format + "\n";
        }
         
        string text = "<b>" + type + "</b>\n" + "Base " + type + ": x\t" + GameFormat.toScientificNotation(baseValue) + format + 
            "\n" + modifierText + "Final " + type + ": x\t" + GameFormat.toScientificNotation(finalValue) + format + "\n\n";
        return text;
    }
}
