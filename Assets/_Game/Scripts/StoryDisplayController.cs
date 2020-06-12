using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryDisplayController : MonoBehaviour
{
    public static event Action OnProgressionComplete = delegate { };
    public static event Action OnStoryEventDisplayComplete = delegate { };

    [SerializeField] StoryTextView _storyTextView = null;
    [SerializeField] StoryDecisionView _storyDecisionView = null;
    [SerializeField] StoryImageView _storyImageView = null;

    private void OnEnable()
    {
        StorySM.OnNewStoryEvent += HandleNewStoryEvent;
    }

    private void OnDisable()
    {
        StorySM.OnNewStoryEvent -= HandleNewStoryEvent;
    }

    public void CompleteReveal()
    {
        //TODO finish revealing the current progression
    }

    public void DisplayStoryEvent(StoryEvent storyEvent)
    {
        if(storyEvent is StoryText)
        {
            _storyTextView.Display(storyEvent as StoryText);
        }
        else if(storyEvent is StoryDecision)
        {
            _storyDecisionView.Display(storyEvent as StoryDecision);
        }
        else if (storyEvent is StoryImage)
        {
            //_storyImageView.Display(storyEvent as StoryImage);
        }
        else if(storyEvent is StoryEnd)
        {
            //TODO
        }
    }

    void HandleNewStoryEvent(StoryEvent newStoryEvent)
    {
        DisplayStoryEvent(newStoryEvent);
    }
}
