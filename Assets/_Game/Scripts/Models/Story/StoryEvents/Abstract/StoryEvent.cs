using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StoryEvent : ScriptableObject
{
    /*
    public event Action<StoryEvent> OnStoryExit = delegate { };

    public abstract void OnProgress();
    public abstract bool CanProgress();
    public abstract StoryEvent Exit(PlayerStats stats, Inventory inventory);

    // use this to store progression inside of the event
    protected int ProgressionIndex { get; set; } = 0;

    bool _firstProgression = true;
    PlayerStats _stats = null;
    Inventory _inventory = null;
    */
    [Header("Base Settings")]
    [SerializeField] string _name = "...";
    public string Name => _name;

    [SerializeField] Gate _gate = null;
    public Gate Gate => _gate;

    /*
    public void Initialize(PlayerStats stats, Inventory inventory)
    {
        _stats = stats;
        _inventory = inventory;
        // flag for first progression so that we can begin properly;
        _firstProgression = true;
    }

    public void Progress()
    {
        if (_firstProgression)
        {
            OnBegin();
        }
        else
        {
            if (CanProgress())
            {
                OnProgress();
            }
            else
            {
                StoryEvent NextStory = Exit(_stats, _inventory);
                OnStoryExit.Invoke(NextStory);
            }
        }
    }
    */
}
