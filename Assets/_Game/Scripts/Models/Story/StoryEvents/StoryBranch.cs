using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryBranch", menuName = "Story/Branch")]
public class StoryBranch : StoryEvent
{
    [SerializeField] StoryEvent[] _possibleStoryEvents = null;
    public StoryEvent[] PossibleStoryEvents => _possibleStoryEvents;
}
