using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    public event Action OnHealthEmpty = delegate { };
    public event Action OnSanityEmpty = delegate { };
    // resource stats
    public Stat Health { get; private set; }    // stat for physical health
    public Stat Sanity { get; private set; }    // stat for mental health
    // action stats
    public Stat Calm { get; private set; }  // stat for keeping cool under pressure
    public Stat Tenacity { get; private set; }  // stat for combat and fighting
    public Stat Perception { get; private set; }    // stat for observation and searching
    public Stat Survival { get; private set; }  // stat for fleeing/avoidance

    private void Awake()
    {
        LoadStats();
    }

    public void LoadStats()
    {
        Health = new Stat("Health", 10);
        Sanity = new Stat("Sanity", 10);
        //TODO load from save file
        Calm = new Stat("Calm", 0);
        Tenacity = new Stat("Tenacity", 0);
        Perception = new Stat("Perception", 0);
        Survival = new Stat("Survival", 0);
    }

    private void OnEnable()
    {
        Health.OnModified += CheckHealthEmpty;
        Sanity.OnModified += CheckSanityEmpty;
    }

    private void OnDisable()
    {
        Health.OnModified -= CheckHealthEmpty;
        Sanity.OnModified -= CheckSanityEmpty;
    }

    public void CheckHealthEmpty(int healthAdjustment)
    {
        if(Health.Value <= 0)
        {
            OnHealthEmpty.Invoke();
        }
    }

    public void CheckSanityEmpty(int sanityAdjustment)
    {
        if(Sanity.Value <= 0)
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
                statToAdjust = Health;
                break;
            case 2:
                statToAdjust = Sanity;
                break;
            case 3:
                statToAdjust = Calm;
                break;
            case 4:
                statToAdjust = Tenacity;
                break;
            case 5:
                statToAdjust = Perception;
                break;
            case 6:
                statToAdjust = Survival;
                break;
            default:
                Debug.Log("Invald choice of stat identifier");
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
