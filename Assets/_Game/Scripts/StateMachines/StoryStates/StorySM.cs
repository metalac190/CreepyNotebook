using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StorySM : StateMachine
{
    [Header("Dependencies")]
    [SerializeField] InputController _input = null;
    [SerializeField] StoryController _story = null;
    [SerializeField] StoryDisplayController _display = null;
    // player settings
    [Header("Player Settings")]
    [SerializeField] PlayerStats _stats = null;
    [SerializeField] Inventory _inventory = null;
    // states
    public StoryIntroState StoryIntroState { get; private set; }
    public StoryRevealState StoryRevealState { get; private set; }
    public StoryIdleState StoryIdleState { get; private set; }
    public StoryExitState StoryExitState { get; private set; }

    private void Awake()
    {
        StoryIntroState = new StoryIntroState(this, _story, _stats, _inventory);
        StoryRevealState = new StoryRevealState(this, _input, _story, _display);
        StoryIdleState = new StoryIdleState(this, _input);
        StoryExitState = new StoryExitState(this);
    }

    private void Start()
    {
        ChangeState(StoryIntroState);
    }
}
