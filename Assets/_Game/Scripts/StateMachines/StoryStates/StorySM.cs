using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StorySM : StateMachine
{
    [Header("Dependencies")]
    [SerializeField] InputManager _input = null;
    [SerializeField] PlayerManager _player = null;
    [SerializeField] StoryUIManager _uiManager = null;

    [Header("Story")]
    [SerializeField] StoryEventData _startingStory = null;

    // story data
    public StoryEventData CurrentStoryEvent { get; set; }
    public StoryEventData NextStoryEvent { get; set; }
    public StoryPage[] ChosenStoryResult { get; set; }
    // states
    public StoryIntroState IntroState { get; private set; }
    public StoryPageState PageState { get; private set; }
    public StoryChooseState ChooseState { get; private set; }
    public StoryTransitionState TransitionState { get; private set; }
    public StoryChosenPagesState ChosenPagesState { get; private set; }
    public StoryExitState ExitState { get; private set; }

    private void Awake()
    {
        Debug.Log("Game State Initialize");
        // initialize states
        IntroState = new StoryIntroState(this, _player.Stats, _player.Inventory, _startingStory);
        PageState = new StoryPageState(this, _input, _uiManager.StoryPageController, _uiManager.HUDController);
        ChooseState = new StoryChooseState(this, _input, _uiManager.StoryDecisionController);
        TransitionState = new StoryTransitionState(this, _player.Stats, _player.Inventory);
        ChosenPagesState = new StoryChosenPagesState(this, _input, _uiManager.StoryPageController, _uiManager.HUDController);
        ExitState = new StoryExitState(this);
    }

    private void Start()
    {
        ChangeState(IntroState);
    }
}
