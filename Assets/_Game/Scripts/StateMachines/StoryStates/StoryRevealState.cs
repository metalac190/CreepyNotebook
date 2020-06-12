using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryRevealState : IState
{
    StorySM _stateMachine = null;
    InputController _input = null;
    StoryEventController _story = null;
    StoryDisplayController _display = null;

    bool _isStartingStoryEvent = true;

    public StoryRevealState(StorySM stateMachine, InputController input, 
        StoryEventController story, StoryDisplayController display)
    {
        _stateMachine = stateMachine;
        _input = input;
        _story = story;
        _display = display;
    }

    public void Enter()
    {
        Debug.Log("STORY: Display");
        _input.OnClicked += HandleClicked;
        StoryDisplayController.OnProgressionComplete += HandleStoryRevealCompleted;
        // if this is our starting point, use that, otherwise progress
        if (!_isStartingStoryEvent)
        {
            _story.StartNewStoryEvent();
        }
        
    }

    public void Exit()
    {
        _input.OnClicked -= HandleClicked;
        StoryDisplayController.OnProgressionComplete -= HandleStoryRevealCompleted;
    }

    public void Tick()
    {

    }

    void HandleClicked()
    {
        _stateMachine.ChangeState(_stateMachine.StoryIdleState);
    }

    void HandleStoryRevealCompleted()
    {
        //
    }
}
