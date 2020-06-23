using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryContentIdleState : IState
{
    private StorySM _stateMachine = null;
    private InputManager _input = null;

    public StoryContentIdleState(StorySM stateMachine, InputManager input)
    {
        _stateMachine = stateMachine;
        _input = input;
    }

    public void Enter()
    {
        Debug.Log("CONTENT IDLE");
        _input.Clicked += HandleClicked;
    }

    public void Exit()
    {
        //TODO STOP listening for button clicks
        _input.Clicked -= HandleClicked;
    }

    public void Tick()
    {
        
    }

    void HandleClicked()
    {
        //TODO make this functional
        _stateMachine.ChangeState(_stateMachine.ContentRevealState);
    }
}
