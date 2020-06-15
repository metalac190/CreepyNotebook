using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StoryEventController : MonoBehaviour
{
    /*
    public event Action<StoryEvent> OnEventEnd = delegate { };

    public abstract void OnBegin(StoryEvent storyEvent);
    public abstract void OnProgress();
    public abstract bool CanProgress();
    public abstract StoryEvent OnDetermineExit();    // return an exit event

    protected PlayerStats Stats;
    protected Inventory Inventory;

    public void Begin(StoryEvent storyEvent)
    {
        OnBegin(storyEvent);
    }

    public void Progress()
    {
        if (CanProgress())
        {
            OnProgress();
        }
        else
        {
            OnEventEnd.Invoke(OnDetermineExit());
        }
    }
    */
}
