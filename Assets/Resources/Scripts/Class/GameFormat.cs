using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameFormat
{
    public static string toScientificNotation(double number)
    {
        if (Math.Floor(Math.Log10(number)) >= 12)
            return number.ToString("E5");
        else
            return number.ToString("N0");
    }

}
