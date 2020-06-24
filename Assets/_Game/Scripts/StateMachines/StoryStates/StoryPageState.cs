using System.Collections;
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
        // begin if we have pages
        StoryPage[] storyPages = _stateMachine.CurrentStoryEvent.StoryPages;
        if(storyPages != null)
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
    }

    public void Tick()
    {
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
        Debug.Log("Show Prompt");
        _hudController.ShowPrompt(_pageController.ContinuePromptText);
    }

    void ExitStory()
    {
        Debug.Log("Out of pages!");
        if (_stateMachine.CurrentStoryEvent.StoryDecision != null
            && _stateMachine.CurrentStoryEvent.ExitType == ExitType.Decision)
        {
            Debug.Log("Attempting decision state");
            _stateMachine.ChangeState(_stateMachine.ChooseState);
        }
        else
        {
            Debug.Log("Attempting transition state");
            _stateMachine.ChangeState(_stateMachine.TransitionState);
        }
    }
}
