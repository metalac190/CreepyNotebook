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
    public StoryEventData CurrentStoryEvent { get; private set; }
    public List<StoryPage> CurrentStoryPages { get; private set; } = new List<StoryPage>();
    public ChoiceOutcome CurrentChoiceOutcome { get; private set; }
    //public StoryEventData NextStoryEvent { get; set; }
    //public StoryPage[] ChosenStoryResult { get; set; }
    // states
    public StoryIntroState IntroState { get; private set; }
    public StoryPageState PageState { get; private set; }
    public StoryChooseState ChooseState { get; private set; }
    public StoryBeginState StoryBeginState { get; private set; }
    public StoryChosenPagesState ChosenPagesState { get; private set; }
    public StoryExitState ExitState { get; private set; }

    private void Awake()
    {
        Debug.Log("Game State Initialize");
        // initialize states
        IntroState = new StoryIntroState(this, _player.Stats, _player.Inventory, _startingStory);
        PageState = new StoryPageState(this, _input, _uiManager.StoryPageController, _uiManager.HUDController);
        ChooseState = new StoryChooseState(this, _input, _uiManager.StoryDecisionController);
        //StoryBeginState = new StoryBeginState(this, _player.Stats, _player.Inventory);
        ChosenPagesState = new StoryChosenPagesState(this, _input, _uiManager.StoryPageController, _uiManager.HUDController, _player.Stats, _player.Inventory);
        ExitState = new StoryExitState(this);
    }

    public void SetStory(StoryEventData newStory)
    {
        if(newStory == null)
        {
            Debug.LogWarning("New story is empty. Cannot set.");
            return;
        }

        CurrentStoryEvent = newStory;
    }

    public void SetStoryPages(List<StoryPage> storyPages)
    {
        CurrentStoryPages = storyPages;
    }

    public void SetChoiceOutcome(ChoiceOutcome choiceOutcome)
    {
        CurrentChoiceOutcome = choiceOutcome;
    }

    public void SetStoryFromExit()
    {
        if (CurrentStoryEvent.StoryExit == null)
        {
            Debug.LogWarning("No StoryExit assigned in current story event. Cannot Assign from Exit");
        }

        CurrentStoryEvent = CurrentStoryEvent.StoryExit.GetExit(_player.Stats, _player.Inventory);
        ResetEventState();
    }

    private void Start()
    {
        ChangeState(IntroState);
    }

    void ResetEventState()
    {
        CurrentChoiceOutcome = null;
    }
}
