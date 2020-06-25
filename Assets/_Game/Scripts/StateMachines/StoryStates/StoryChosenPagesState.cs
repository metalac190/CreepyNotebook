using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryChosenPagesState : IState
{
    StorySM _stateMachine = null;
    InputManager _input = null;
    StoryPageController _pageController = null;
    HUDController _hudController = null;

    bool _isValidStory = false;

    public StoryChosenPagesState(StorySM stateMachine, InputManager input,
        StoryPageController storyController, HUDController hudController)
    {
        _stateMachine = stateMachine;
        _input = input;
        _pageController = storyController;
        _hudController = hudController;
    }

    public void Enter()
    {
        Debug.Log("CHOSEN CONTENT REVEAL");
        // subscribe
        _input.Clicked += OnClicked;
        _pageController.OutOfPages += OnOutOfPages;
        _pageController.CompletedShowAnimation += OnCompletedShowAnimation;
        // begin if we have pages. If not, flag it to exit as soon as it's able
        BeginStoryIfValid();
    }

    private void BeginStoryIfValid()
    {
        StoryPage[] storyPages = _stateMachine.ChosenStoryResult;
        if (storyPages.Length > 0 && storyPages != null)
        {
            _isValidStory = true;
            _pageController.Begin(_stateMachine.CurrentStoryEvent.StoryPages);
        }
        // otherwise it's an invalid story
        else
        {
            _isValidStory = false;
        }
    }

    public void Exit()
    {
        // unsubscribe
        _input.Clicked -= OnClicked;
        _pageController.OutOfPages -= OnOutOfPages;
        _pageController.CompletedShowAnimation -= OnCompletedShowAnimation;
        // reset state defaults
        _isValidStory = false;  // this gets turned to true, if valid
    }

    public void Tick()
    {
        // somewhat hacky. We've decided the story is not valid, but we need to wait until we've
        // finished entering the state before we can exit.
        if (_isValidStory == false)
        {
            ExitStory();
        }
    }

    void OnClicked()
    {
        _pageController.Progress();
    }

    void OnOutOfPages()
    {
        ExitStory();
    }

    void OnCompletedShowAnimation()
    {
        Debug.Log("Show Prompt");
        _hudController.ShowPrompt(_pageController.ContinuePromptText);
    }

    void ExitStory()
    {
        _stateMachine.ChangeState(_stateMachine.TransitionState);
    }
}
