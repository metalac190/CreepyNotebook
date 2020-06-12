using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorySM : StateMachine
{
    [SerializeField] InputController _input = null;
    [SerializeField] StoryController _story = null;

    public StoryIntroState StoryIntroState { get; private set; }
    public StoryRevealState StoryRevealState { get; private set; }
    public StoryChoiceState StoryChoiceState { get; private set; }
    public StoryExitState StoryExitState { get; private set; }

    private void Awake()
    {
        StoryIntroState = new StoryIntroState(this);
        StoryRevealState = new StoryRevealState(this, _input, _story);
        StoryChoiceState = new StoryChoiceState(this);
        StoryExitState = new StoryExitState(this);
    }

    private void Start()
    {
        ChangeState(StoryIntroState);
    }
}
