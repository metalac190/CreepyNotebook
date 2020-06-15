using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryText : StoryEvent
{
    [Header("Story Text Settings")]
    [SerializeField] StoryEvent[] _possibleExits = null;
    public StoryEvent[] PossibleExits => _possibleExits;

    [SerializeField] TextBlock[] _textBlocks = null;
    public TextBlock[] TextBlocks => _textBlocks;

    /*
    public override StoryEvent Exit(PlayerStats stats, Inventory inventory)
    {
        List<StoryEvent> validStoryEvents = new List<StoryEvent>();
        foreach (StoryEvent storyEvent in _possibleStoryEvents)
        {
            if (storyEvent.Gate.TestRequirements(stats, inventory) == true)
            {
                validStoryEvents.Add(storyEvent);
            }
        }
        // choose a random story event from valid ones
        //TODO consider adding a system for weighting here
        int randomEventIndex = UnityEngine.Random.Range(0, validStoryEvents.Count);
        return validStoryEvents[randomEventIndex];
    }
    */
}
