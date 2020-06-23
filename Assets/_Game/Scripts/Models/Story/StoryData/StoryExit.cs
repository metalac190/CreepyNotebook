using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryExit
{
    [Header("Story Exit Settings")]

    [SerializeField] StoryEventData[] _possibleExits;
    public StoryEventData[] PossibleExits => _possibleExits;

    public virtual StoryEventData GetExit(PlayerStats stats, Inventory inventory)
    {
        List<StoryEventData> validStoryEvents = new List<StoryEventData>();
        foreach (StoryEventData storyEvent in _possibleExits)
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
}
