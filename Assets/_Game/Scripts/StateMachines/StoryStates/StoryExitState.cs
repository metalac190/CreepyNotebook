using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryExitState : IState
{
    StorySM _stateMachine = null;

    public StoryExitState(StorySM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        Debug.Log("STORE: Exit State");
    }

    public void Exit()
    {
        
    }

    public void Tick()
    {
        
    }
}
