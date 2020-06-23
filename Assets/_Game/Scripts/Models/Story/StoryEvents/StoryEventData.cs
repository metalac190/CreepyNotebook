using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewStoryEvent", menuName = "StoryEvent/Event")]
public class StoryEventData : ScriptableObject
{
    [Header("Base Settings")]
    [SerializeField] string _name = "...";
    public string Name => _name;

    [SerializeField] Gate _gate = null;
    public Gate Gate => _gate;

    [SerializeField] StoryPage[] _storyPages = null;
    public StoryPage[] StoryPages => _storyPages;

    [SerializeField] StoryExit _storyExit = null;
    public StoryExit StoryExit => _storyExit;
}
