using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEventPicker
{
    PlayerStats _stats = null;
    Inventory _inventory = null;

    public StoryEventPicker(PlayerStats stats, Inventory inventory)
    {
        _stats = stats;
        _inventory = inventory;
    }

    public StoryEvent PickStoryEvent(StoryEvent[] possibleStoryEvents)
    {
        List<StoryEvent> validStoryEvents = new List<StoryEvent>();
        foreach(StoryEvent storyEvent in possibleStoryEvents)
        {
            if(storyEvent.Gate.TestRequirements(_stats, _inventory) == true)
            {
                validStoryEvents.Add(storyEvent);
            }
        }
        // choose a random story event from valid ones
        //TODO consider adding a system for weighting here
        int randomEventIndex = UnityEngine.Random.Range(0, validStoryEvents.Count);
        return validStoryEvents[randomEventIndex];
    }
}
