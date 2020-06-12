using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Stat
{
    public event Action<int> OnModified = delegate { };

    int _maxValue = 99;

    public string Name { get; private set; } = "";
    public int Value { get; private set; }

    public Stat(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public void Adjust(int amount)
    {
        // guard clause
        if(amount == 0) { return; }

        Value += amount;
        // ensure we don't go over the max value
        Value = Mathf.Clamp(Value, 0, _maxValue);
    }
}
