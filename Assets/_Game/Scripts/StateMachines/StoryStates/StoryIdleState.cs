using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIdleState : IState
{
    private StorySM _stateMachine = null;

    public StoryIdleState(StorySM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        //TODO listen for button clicks
    }

    public void Exit()
    {
        //TODO STOP listening for button clicks
    }

    public void Tick()
    {
        
    }
}
