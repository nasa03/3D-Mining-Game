using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Operant { add, percent }

[System.Serializable]
public class StatModifier : Stats
{
    public string modifierName;
    public string assignedStat;
    public string modifierId { get; private set; }
    public override float baseValue { get; protected set; }
    public override int level { get; protected set; }
    public override int maxLevel { get; protected set; }
    public override string type { get; protected set; }
    public override string format { get; protected set; }
    public override float finalValue { get; protected set; }

    public Operant operant;

    private float _stackValue;
    public float stackValue
    {
        get { return _stackValue; }
        set { _stackValue = value; }
    }

    private int _expireTime;
    public int expireTime
    {
        get { return _expireTime; }
        set { _expireTime = value; }
    }

    public StatModifier(string name, string format, Operant calculation, string assignedStat, float baseValue, int maxLevel, float stackValue, int expireTime)
    {
        this.modifierName = name;
        this.assignedStat = assignedStat;
        this.format = format;
        operant = calculation;
        this.baseValue = baseValue;
        this.level = 0;
        this.maxLevel = maxLevel;
        this.stackValue = stackValue;
        this.expireTime = expireTime;
    }

    //Constructor without Expire Timer
    public StatModifier(string name, string format, Operant calculation, string assignedStat, float baseValue, int maxStack, float stackValue) : this(name, format, calculation, assignedStat, baseValue, maxStack, stackValue, 0)
    {
    }

    //Constructor without Stack
    public StatModifier(string name, string format, Operant calculation, string assignedStat, float baseValue, int expireTime) : this(name, format, calculation, assignedStat, baseValue, 1, 0, expireTime)
    {
    }

    //Constructor without Stack or Expire Timer
    public StatModifier(string name, string format, Operant calculation, string assignedStat, float baseValue) : this(name, format, calculation, assignedStat, baseValue, 1, 0, 0)
    {
    }

    public void assignModifierId(string modifierId)
    {
        this.modifierId = modifierId;
    }

    public void ChangeBaseValue(float amount)
    {
        if(baseValue == amount)
        {
            return;
        }
        baseValue = amount;
        Gamemanager.main.player.stats[assignedStat].CalculateFinal();
        CalculateFinal();
    }

    public void ChangeStackValue(float amount)
    {
        if (stackValue == amount)
        {
            return;
        }
        stackValue = amount;
        Gamemanager.main.player.stats[assignedStat].CalculateFinal();
        CalculateFinal();
    }

    public override float CalculateFinal()
    {
        finalValue = baseValue + stackValue * (level - 1);
        return finalValue;
    }
}


public class ModifierSystem : MonoBehaviour
{
    public static Dictionary<string, StatModifier> currentModifiers { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        currentModifiers = new Dictionary<string, StatModifier>();
        initModifiers();
    }

    //TODO: Save Modifiers when applied
    void initModifiers()
    {
        /*   !!! WTF ARE MODIFIERS?! !!!
         *   
         *   Modifiers are... well.. modifiers, that change the value of a Playerstat. They can be added and removed without affecting the Playerstat and are useful for a lot of stuff.
         *   Here, you can initialize all the modifiers. Make sure you leave a comment to which modifier belongs to who.
         *  
         *   --------------------------------------------------
         *   | Modifier Legend                                |
         *   |-------------------------------------------------
         *   | id                => int                       |  -> ID of Modifier
         *   | name              => string                    |  -> Name of Modifier
         *   | format            => string                    |  -> Format of Modifier. Used to display in Menues
         *   | calculation       => enum Operant              |  -> Calculation. Can choose between Percantage or Add
         *   | desc              => string                    |  -> Description of Modifier
         *   | baseValue         => float                     |  -> Base Value when applied to stat.
         *   | maxStack          => int                       |  -> (Optional) Max Stack that modifier can have.
         *   | stackValue        => float                     |  -> (Optional) Value that increases Base Value each Stack.
         *   | expireTime        => float                     |  -> (Optional) Modifier expiration. Modifier will be remove from stat if expired.
         *   --------------------------------------------------
         *   
         */

        //Perks
        currentModifiers.Add("perk_damage", new StatModifier("Damage Perk", "%", Operant.percent, "damage", 0.005f, 100, 0.005f));
        currentModifiers.Add("perk_speed", new StatModifier("Speed Perk", "%", Operant.percent, "speed", -0.001f, 100, -0.001f));

        //Speed
        currentModifiers.Add("debug_speed", new StatModifier("DEBUG", "%", Operant.add, "speed", -999f));
        currentModifiers.Add("lava_debuff_speed", new StatModifier("Lava", "%", Operant.add, "speed", 0.8f, 5, 0.5f));
    }

    public static void ApplyModifier(string statType, string modifierName, int amount)
    {
        statType = statType.ToLower();

        if (!currentModifiers.ContainsKey(modifierName))
        {
            Debug.LogError("Modifier " + modifierName + " doesn't exist!");
            return;
        }

        if (!Gamemanager.main.player.stats.ContainsKey(statType))
        {
            Debug.LogError("Player stat " + statType + " doesn't exist!");
            return;
        }

        //Debug.Log(currentModifiers[modifierName].level + " - " + (amount - currentModifiers[modifierName].level));
        Gamemanager.main.player.stats[statType].AddModifier(currentModifiers[modifierName], amount);
        currentModifiers[modifierName].assignedStat = statType;
        //currentModifiers[modifierName].assignModifierId(modifierName);
    }

    void Update()
    {
        if(Gamemanager.main.isTick) {
            currentModifiers["perk_damage"].ChangeBaseValue(Gamemanager.main.player.xp.level * 0.01f);
            currentModifiers["perk_damage"].ChangeStackValue(Gamemanager.main.player.xp.level * 0.005f);
            
            //Debug.Log(currentModifiers["perk_damage"].finalValue);
        }
    }
}
