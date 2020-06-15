using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(StoryTextView))]
public class StoryTextController : MonoBehaviour
{
    StoryTextView _storyTextView = null;
    StoryText _storyText = null;

    int _currentTextProgressionIndex = 0;

    private void Awake()
    {
        _storyTextView = GetComponent<StoryTextView>();
    }
    /*
    public override void OnBegin(StoryText storyText)
    {
        // store data
        //TODO double check this cast
        _storyText = storyText;
        // progress state
        _currentTextProgressionIndex = 0;
        // display first story
        TextBlock nextTextBlock = GetTextBlock(_storyText, _currentTextProgressionIndex);
    }

    public override void OnProgress()
    {
        // test our textblock array to see if we can keep going
        _currentTextProgressionIndex++;
        //TODO display
        TextBlock nextTextBlock = GetTextBlock(_storyText, _currentTextProgressionIndex);
        _storyTextView.Display(GetTextBlock(_storyText, _currentTextProgressionIndex));
    }

    public override bool CanProgress()
    {
        if (ArrayHelper.IsValidIndex(_currentTextProgressionIndex,
            _storyText.TextBlocks.Length))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override StoryEvent OnDetermineExit()
    {
        List<StoryEvent> validStoryEvents = new List<StoryEvent>();
        foreach (StoryEvent storyEvent in _storyText.PossibleExits)
        {
            if (storyEvent.Gate.TestRequirements(Stats, Inventory) == true)
            {
                validStoryEvents.Add(storyEvent);
            }
        }
        // choose a random story event from valid ones
        //TODO consider adding a system for weighting here
        int randomEventIndex = UnityEngine.Random.Range(0, validStoryEvents.Count);
        return validStoryEvents[randomEventIndex];
    }

    private TextBlock GetTextBlock(StoryText storyText, int index)
    {
        TextBlock nextTextBlock = _storyText.TextBlocks[_currentTextProgressionIndex];
        return nextTextBlock;
    }
    */
}
