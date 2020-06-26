using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StoryChosenPagesState : IState
{
    StorySM _stateMachine = null;
    InputManager _input = null;
    StoryPageController _pageController = null;
    HUDController _hudController = null;
    PlayerStats _stats = null;
    Inventory _inventory = null;

    bool _isValidStory = false;

    public StoryChosenPagesState(StorySM stateMachine, InputManager input,
        StoryPageController storyController, HUDController hudController, 
        PlayerStats stats, Inventory inventory)
    {
        _stateMachine = stateMachine;
        _input = input;
        _pageController = storyController;
        _hudController = hudController;
        _stats = stats;
        _inventory = inventory;
    }

    public void Enter()
    {
        Debug.Log("CHOSEN CONTENT REVEAL");
        // subscribe
        _input.Clicked += OnClicked;
        _pageController.OutOfPages += OnOutOfPages;
        // begin if we have pages. If not, flag it to exit as soon as it's able
        BeginStoryIfValid();
    }

    private void BeginStoryIfValid()
    {
        List<StoryPage> storyPages = _stateMachine.CurrentChoiceOutcome.ChosenStoryPages.ToList();
        if (StoryHelper.IsStoryPagesValid(storyPages))
        {
            _isValidStory = true;
            //_stateMachine.SetStoryPages(storyPages);
            _pageController.Begin(storyPages);
        }
        // otherwise it's an invalid story
        else
        {
            Debug.LogError("No Valid story assigned");
            _isValidStory = false;
        }
    }

    public void Exit()
    {
        // unsubscribe
        _input.Clicked -= OnClicked;
        _pageController.OutOfPages -= OnOutOfPages;
        // clear display
        _pageController.HideContent();
        // reset state defaults
        _isValidStory = false;  // this gets turned to true, if valid
    }

    public void Tick()
    {
        // somewhat hacky. We've decided the story is not valid, but we need to wait until we've
        // finished entering the state before we can exit.
        if (_isValidStory == false)
        {
            _stateMachine.ChangeState(_stateMachine.ExitState);
        }
    }

    void OnClicked()
    {
        _pageController.Progress();
    }

    void OnOutOfPages()
    {
        StartNewStory();
    }

    void OnCompletedShowAnimation()
    {
        Debug.Log("Show Prompt");
        _hudController.ShowPrompt(_pageController.ContinuePromptText);
    }

    void StartNewStory()
    {
        //_stateMachine.ChangeState(_stateMachine.StoryBeginState);
        _pageController.HideContent();
        StoryEventData nextStory = _stateMachine.CurrentChoiceOutcome.ChosenStoryExits.GetExit(_stats, _inventory);
        _stateMachine.SetStory(nextStory);
        _stateMachine.ChangeState(_stateMachine.PageState);
    }
}
