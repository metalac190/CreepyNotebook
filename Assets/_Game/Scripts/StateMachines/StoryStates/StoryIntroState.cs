using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIntroState : IState
{
    StorySM _stateMachine = null;

    public StoryIntroState(StorySM stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        Debug.Log("STORY: Intro State");
    }

    public void Exit()
    {
        
    }

    public void Tick()
    {
        _stateMachine.ChangeState(_stateMachine.StoryRevealState);
    }
}
