using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryDisplayController : MonoBehaviour
{
    public static event Action OnProgressionComplete = delegate { };
    public static event Action OnStoryEventDisplayComplete = delegate { };

    [SerializeField] StoryController _story = null;
    [SerializeField] StoryTextView _storyTextView = null;
    [SerializeField] StoryDecisionView _storyDecisionView = null;
    [SerializeField] StoryImageView _storyImageView = null;

    private void OnEnable()
    {
        //_story.OnStartedNewStoryEvent += HandleNewStoryEvent;
    }

    private void OnDisable()
    {
        //_story.OnStartedNewStoryEvent -= HandleNewStoryEvent;
    }

    public void CompleteReveal()
    {
        //TODO finish revealing the current progression
    }

    public void ProgressDisplay(StoryEvent storyEvent)
    {
        if(storyEvent is StoryText)
        {
            //_storyTextView.ProgressDisplay(storyEvent as StoryText);
        }
        else if(storyEvent is StoryDecision)
        {
            _storyDecisionView.ProgressDisplay(storyEvent as StoryDecision);
        }
        else if (storyEvent is StoryImage)
        {
            _storyImageView.ProgressDisplay(storyEvent as StoryImage);
        }
    }

    void HandleNewStoryEvent(StoryEvent newStoryEvent)
    {
        ProgressDisplay(newStoryEvent);
    }
}
