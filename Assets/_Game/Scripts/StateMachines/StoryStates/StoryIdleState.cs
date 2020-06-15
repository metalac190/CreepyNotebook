using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIdleState : IState
{
    private StorySM _stateMachine = null;
    private InputController _input = null;

    public StoryIdleState(StorySM stateMachine, InputController input)
    {
        _stateMachine = stateMachine;
        _input = input;
    }

    public void Enter()
    {
        _input.OnClicked += HandleClicked;
    }

    public void Exit()
    {
        //TODO STOP listening for button clicks
        _input.OnClicked -= HandleClicked;
    }

    public void Tick()
    {
        
    }

    void HandleClicked()
    {

    }
}
