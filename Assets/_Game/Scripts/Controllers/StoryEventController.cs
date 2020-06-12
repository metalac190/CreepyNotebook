using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryEventController : MonoBehaviour
{
    public event Action<StoryEvent> OnNewStoryEvent = delegate { };

    [SerializeField] PlayerStats _stats = null;
    [SerializeField] Inventory _inventory = null;

    [SerializeField] StoryEvent _startingStoryEvent = null;

    public StoryEvent CurrentStoryEvent { get; private set; }
    public StoryEvent NextStoryEvent { get; private set; }

    StoryEventPicker _storyEventPicker = null;

    private void Awake()
    {
        _storyEventPicker = new StoryEventPicker(_stats, _inventory);
    }

    public void SetStoryEvent(StoryEvent newStoryEvent)
    {
        CurrentStoryEvent = newStoryEvent;
        // if it's a branch, resolve it
        if(CurrentStoryEvent is StoryBranch)
        {
            StoryBranch storyBranch = CurrentStoryEvent as StoryBranch;
            CurrentStoryEvent = ChooseNextEvent(storyBranch.PossibleStoryEvents);
        }
    }

    public void StartNewStoryEvent()
    {
        StoryEvent nextStoryEvent = CurrentStoryEvent.ExitEvent;
        
        // if we have a storybranch, we must first resolve the possibilities
        if(nextStoryEvent is StoryBranch)
        {
            StoryBranch storyBranch = nextStoryEvent as StoryBranch;
            nextStoryEvent = ChooseNextEvent(storyBranch.PossibleStoryEvents);
        }
        // store it for later
        CurrentStoryEvent = nextStoryEvent;

        OnNewStoryEvent.Invoke(CurrentStoryEvent);
    }
    /*
    public void ProgressStory()
    {
        if(CurrentStoryEvent is IProgressable)
        {
            CurrentStoryEvent.Progress();
        }
        else
        {
            CurrentStoryEvent = StartNewStoryEvent();
        }
    }
    */

    StoryEvent ChooseNextEvent(StoryEvent[] possibleEvents)
    {
        return _storyEventPicker.PickStoryEvent(possibleEvents);
    }
}
