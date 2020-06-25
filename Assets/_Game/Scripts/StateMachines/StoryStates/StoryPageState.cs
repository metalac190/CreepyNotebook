﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPageState : IState
{
    StorySM _stateMachine = null;
    InputManager _input = null;
    StoryPageController _pageController = null;
    HUDController _hudController = null;

    bool _isValidStory = false;

    public StoryPageState(StorySM stateMachine, InputManager input, 
        StoryPageController storyController, HUDController hudController)
    {
        _stateMachine = stateMachine;
        _input = input;
        _pageController = storyController;
        _hudController = hudController;
    }

    public void Enter()
    {
        Debug.Log("CONTENT REVEAL");
        // subscribe
        _input.Clicked += OnClicked;
        _pageController.OutOfPages += OnOutOfPages;
        _pageController.CompletedShowAnimation += OnCompletedShowAnimation;
        // begin if we have pages. If not, flag it to exit as soon as it's able
        BeginStoryIfValid();
    }

    private void BeginStoryIfValid()
    {
        StoryPage[] storyPages = _stateMachine.CurrentStoryEvent.StoryPages; ;
        if (storyPages.Length > 0 && storyPages != null)
        {
            // begin story
            _isValidStory = true;
            _pageController.Begin(_stateMachine.CurrentStoryEvent.StoryPages);
        }
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
        // clear display
        _pageController.HideContent();
        // reset state defaults
        _isValidStory = false;  // this gets turned to true, if valid
    }

    public void Tick()
    {
        // somewhat hacky. We've decided the story is not valid, but we need to wait until we've
        // finished entering the state before we can exit.
        if(_isValidStory == false)
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
        _hudController.ShowPrompt(_pageController.ContinuePromptText);
    }

    void ExitStory()
    {
        if (_stateMachine.CurrentStoryEvent.StoryChoice != null &&    // we have a story decision assigned
            _stateMachine.CurrentStoryEvent.ExitType == ExitType.Choice)     // we've assigned Choice as the exit type
        {
            _stateMachine.ChangeState(_stateMachine.ChooseState);
        }
        else
        {
            _stateMachine.ChangeState(_stateMachine.TransitionState);
        }
    }
}
