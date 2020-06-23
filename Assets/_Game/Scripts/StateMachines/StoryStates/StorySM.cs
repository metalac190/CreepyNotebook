using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StorySM : StateMachine
{
    [Header("Dependencies")]
    [SerializeField] InputManager _input = null;
    [SerializeField] PlayerManager _player = null;
    // states
    public StoryIntroState IntroState { get; private set; }
    public StoryContentRevealState ContentRevealState { get; private set; }
    public StoryContentIdleState ContentIdleState { get; private set; }
    public StoryExitState ExitState { get; private set; }

    private void Awake()
    {
        // initialize states
        IntroState = new StoryIntroState(this, _player.Stats, _player.Inventory);
        ExitState = new StoryExitState(this);
        ContentRevealState = new StoryContentRevealState(this, _input);
        ContentIdleState = new StoryContentIdleState(this, _input);
    }

    private void Start()
    {
        ChangeState(IntroState);
    }
}
