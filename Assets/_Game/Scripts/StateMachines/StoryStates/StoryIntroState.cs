using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIntroState : IState
{
    StorySM _stateMachine = null;
    Inventory _inventory = null;
    PlayerStats _stats = null;

    public StoryIntroState(StorySM stateMachine, PlayerStats stats, Inventory inventory)
    {
        _stateMachine = stateMachine;
        _inventory = inventory;
        _stats = stats;
    }

    public void Enter()
    {
        Debug.Log("INTRO");
        
    }

    public void Exit()
    {
        
    }

    public void Tick()
    {
        _stateMachine.ChangeState(_stateMachine.ContentRevealState);
    }
}
