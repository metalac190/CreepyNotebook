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

    [Header("Requirements")]
    [SerializeField] Gate _gate = null;
    public Gate Gate => _gate;

    [Header("Story Pages")]
    [SerializeField] StoryPage[] _storyPages = null;
    public StoryPage[] StoryPages => _storyPages;

    [Header("Exit Settings")]
    [SerializeField] ExitType _exitType = ExitType.Story;
    public ExitType ExitType => _exitType;

    [SerializeField] StoryExit _storyExit = null;
    public StoryExit StoryExit => _storyExit;

    [SerializeField] StoryChoice _storyChoice = null;
    public StoryChoice StoryChoice => _storyChoice;
}
