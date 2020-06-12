using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoryEvent : ScriptableObject
{
    [SerializeField] string _name = "...";
    public string Name => _name;

    [SerializeField] Gate _gate = null;
    public Gate Gate => _gate;

    [SerializeField] StoryEvent _exitEvent = null;
    public StoryEvent ExitEvent => _exitEvent;
}
