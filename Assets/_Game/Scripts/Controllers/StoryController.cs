using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryController : MonoBehaviour
{
    //public event Action<StoryEvent> OnStartedNewStoryEvent = delegate { };
    //public event Action<StoryEvent> OnProgressed = delegate { };
    //public event Action<StoryEvent> OnEndedStoryEvent = delegate { };
    [Header("Dependencies")]
    [SerializeField] StoryTextController _storyTextController = null;

    [Header("Story Settings")]
    [SerializeField] StoryEvent _startingStoryEvent = null;
    public StoryEvent StartingStoryEvent => _startingStoryEvent;

    public StoryEvent CurrentStoryEvent { get; private set; }

    bool _isBeginning = true;

    public void Initialize()
    {
        // set flag for starting story
        _isBeginning = true;
    }

    public void ProgressStoryText()
    {

    }

    public void ProgressStory()
    {
        // if we just entered story, use the starting Story Event
        if (_isBeginning)
        {
            CurrentStoryEvent = _startingStoryEvent;
            return;
        }
        // handle our current story
        /*
        if (CurrentStoryEvent.CanProgress())
        {
            OnProgressed.Invoke(CurrentStoryEvent);
        }
        else if(!CurrentStoryEvent.CanProgress())
        {
            OnStoryEventEnd.Invoke(CurrentStoryEvent);
        }
        */
    }
}
