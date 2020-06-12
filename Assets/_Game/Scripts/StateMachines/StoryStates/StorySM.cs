using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StorySM : StateMachine
{
    [SerializeField] InputController _input = null;
    [SerializeField] StoryEventController _story = null;
    [SerializeField] StoryDisplayController _display = null;

    public static event Action<StoryEvent> OnNewStoryEvent = delegate { };

    public StoryIntroState StoryIntroState { get; private set; }
    public StoryRevealState StoryRevealState { get; private set; }
    public StoryIdleState StoryIdleState { get; private set; }
    public StoryExitState StoryExitState { get; private set; }

    private void Awake()
    {
        StoryIntroState = new StoryIntroState(this);
        StoryRevealState = new StoryRevealState(this, _input, _story, _display);
        StoryIdleState = new StoryIdleState(this);
        StoryExitState = new StoryExitState(this);
    }

    private void Start()
    {
        ChangeState(StoryIntroState);
    }
}
