using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0219 // variable assign but not used

[System.Serializable]
public class Stat
{
    [SerializeField] string statName = "";
    public int BaseValue { get; private set; }
    public int Modifier { get; private set; }
    public int Value { get { return BaseValue + Modifier; } }

    public Stat(int baseValue)
    {
        BaseValue = baseValue;
    }

    public void PermanentAdjustment(int adjustment)
    {
        BaseValue += adjustment;
    }

    public void ModifierAdjustment(int adjustment)
    {
        Modifier += adjustment;
    }
}
