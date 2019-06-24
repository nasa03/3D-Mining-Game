using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Operant { add, percent }

public class StatModifier
{
    public string modifierName;
    public string description;
    public Operant operant;

    private float _baseValue;
    public float baseValue
    {
        get { return _baseValue; }
        set { _baseValue = value; }
    }

    private float _stackValue;
    public float stackValue
    {
        get { return _stackValue; }
        set { _stackValue = value; }
    }

    private int _stack;
    public int stack
    {
        get { return _stack; }
        set { _stack = value; }
    }

    private int _maxStack;
    public int maxStack
    {
        get { return _maxStack; }
        set { _maxStack = value; }
    }

    private float _finalValue;
    public float finalValue
    {
        get { return _finalValue; }
        private set { _finalValue = value; }
    }

    //The format for the stat. Example: s -> seconds, b -> blocks, % -> Percent.
    private string _format;
    public string format
    {
        get { return _format; }
        set { _format = value; }
    }

    public StatModifier(string name, string format, Operant calculation ,string desc, float baseValue, int maxStack)
    {
        this.modifierName = name;
        this.description = desc;
        this.format = format;
        operant = calculation;
        this.baseValue = baseValue;
        this.stack = 1;
        this.maxStack = maxStack;
    }

    public float combineValue() {
        finalValue = baseValue + stackValue * (stack - 1);
        return finalValue;
    }
}
