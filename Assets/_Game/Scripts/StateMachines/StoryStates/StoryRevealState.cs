using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryRevealState : IState
{
    StorySM _stateMachine = null;
    InputController _input = null;
    StoryController _story = null;
    StoryDisplayController _display = null;

    public StoryRevealState(StorySM stateMachine, InputController input, 
        StoryController story, StoryDisplayController display)
    {
        _stateMachine = stateMachine;
        _input = input;
        _story = story;
        _display = display;
    }

    public void Enter()
    {
        Debug.Log("STORY: Display");
        // subscribe
        _input.OnClicked += HandleClicked;
        StoryDisplayController.OnProgressionComplete += HandleStoryRevealCompleted;
        // handle story
        _story.ProgressStory();
        _display.ProgressDisplay(_story.CurrentStoryEvent);
    }

    public void Exit()
    {
        // unsubscribe
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
        _stateMachine.ChangeState(_stateMachine.StoryIdleState);
    }
}
