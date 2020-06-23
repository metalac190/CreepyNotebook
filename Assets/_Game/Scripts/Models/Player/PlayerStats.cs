using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public event Action OnHealthEmpty = delegate { };
    public event Action OnSanityEmpty = delegate { };
    // resource
    public int Health { get; private set; }    // stat for physical health
    public int Sanity { get; private set; }    // stat for mental health
    // resource stats
    public Stat MaxHealth { get; private set; }
    public Stat MaxSanity { get; private set; }
    // action stats
    public Stat Calm { get; private set; }  // stat for keeping cool under pressure
    public Stat Survival { get; private set; }  // stat for fleeing/avoidance
    public Stat Tenacity { get; private set; }  // stat for combat and fighting

    private void Awake()
    {
        LoadStats();
    }

    public void LoadStats()
    {
        MaxHealth = new Stat("MaxHealth", 10);
        MaxSanity = new Stat("MaxSanity", 10);
        //TODO load from save file
        Calm = new Stat("Calm", 0);
        Survival = new Stat("Survival", 0);
        Tenacity = new Stat("Tenacity", 0);
    }

    public void LoseHealth(int amount)
    {
        Health -= amount;
        Health = Mathf.Clamp(Health, 0, MaxHealth.Value);
        CheckHealthEmpty();
    }

    public void LoseSanity(int amount)
    {
        Sanity -= amount;
        Sanity = Mathf.Clamp(Sanity, 0, MaxHealth.Value);
        CheckSanityEmpty();
    }

    public void CheckHealthEmpty()
    {
        if(Health <= 0)
        {
            OnHealthEmpty.Invoke();
        }
    }

    public void CheckSanityEmpty()
    {
        if(Sanity <= 0)
        {
            OnSanityEmpty.Invoke();
        }
    }

    // this function allows access to a stat given an enum
    // mainly this is useful for Designer editing/inspector
    public void AdjustStat(StatIdentifier stat, int amount)
    {
        Stat statToAdjust;

        int statIndex = (int)stat;
        switch (statIndex)
        {
            case 0:
                statToAdjust = null;
                break;
            case 1:
                statToAdjust = MaxHealth;
                break;
            case 2:
                statToAdjust = MaxSanity;
                break;
            case 3:
                statToAdjust = Calm;
                break;
            case 4:
                statToAdjust = Survival;
                break;
            case 5:
                statToAdjust = Tenacity;
                break;
            default:
                Debug.LogWarning("Invald choice of stat identifier");
                statToAdjust = null;
                break;
        }

        // if we have a valid stat to adjust, adjust it
        if(statToAdjust != null)
        {
            statToAdjust.Adjust(amount);
        }
    }
}
