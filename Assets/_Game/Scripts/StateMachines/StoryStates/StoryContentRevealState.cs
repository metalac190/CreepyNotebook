using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryContentRevealState : IState
{
    StorySM _stateMachine = null;
    InputManager _input = null;

    public StoryContentRevealState(StorySM stateMachine, InputManager input)
    {
        _stateMachine = stateMachine;
        _input = input;
    }

    public void Enter()
    {
        Debug.Log("CONTENT REVEAL");
        // subscribe
        _input.Clicked += HandleClicked;
        // handle story
        //_story.ProgressStory();
    }

    public void Exit()
    {
        // unsubscribe
        _input.Clicked -= HandleClicked;
    }

    public void Tick()
    {

    }

    void HandleClicked()
    {
        _stateMachine.ChangeState(_stateMachine.ContentIdleState);
    }

    void HandleStoryRevealCompleted()
    {
        _stateMachine.ChangeState(_stateMachine.ContentIdleState);
    }
}
