using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum StatIdentifier
{
    None = 0, Calm, Tenacity, Perception, Survival
}

public class PlayerStats : MonoBehaviour
{
    public int AnxietyLevel { get; private set; }
    public Stat[] Stats { get; private set; }

    public event Action OnStatChange = delegate { };

    private void Awake()
    {
        SetupStats();
        SetupAnxiety();
    }

    void SetupStats()
    {
        Stats = new Stat[5];
        //load in stat values through disk
    }

    void SetupAnxiety()
    {
        SetAnxiety(0);
        // we want to subtract the Calm modifier from our anxiety, to give a buffer
        int anxietyAdjustAmount = -(GetStatValue(StatIdentifier.Calm)*5);
        AnxietyAdjustment(anxietyAdjustAmount);
    }

    public void SetAnxiety(int newAnxietyAmount)
    {
        AnxietyLevel = newAnxietyAmount;
    }

    public void AnxietyAdjustment(int amountToAdjust)
    {
        AnxietyLevel += amountToAdjust;
    }

    public void PermanentStatAdjust(StatIdentifier stat, int adjustment)
    {
        // Bouncer //
        if(stat == StatIdentifier.None || adjustment <= 0)
        {
            return;
        }

        // convert the enum into an index we can use
        int statIndex = (int)stat - 1;
        Stats[statIndex].PermanentAdjustment(adjustment);

        OnStatChange.Invoke();
    }

    public void StatAdjust(StatIdentifier stat, int amount)
    {
        // Bouncer //
        if (stat == StatIdentifier.None || amount <= 0)
        {
            return;
        }

        int statIndex = (int)stat;
        Stats[statIndex].ModifierAdjustment(amount);

        OnStatChange.Invoke();
    }

    public int GetStatValue(StatIdentifier stat)
    {
        // BOUNCER //
        if(stat == StatIdentifier.None)
        {
            Debug.LogError("Cannot retrieve Empty player stat. Don't retrieve 'None'");
            return 0;
        }

        int statIndex = (int)stat - 1;

        return Stats[statIndex].Value;
    }
}
