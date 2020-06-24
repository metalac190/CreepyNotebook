using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIntroState : IState
{
    StorySM _stateMachine = null;
    Inventory _inventory = null;
    PlayerStats _stats = null;
    StoryEventData _startingStory = null;

    public StoryIntroState(StorySM stateMachine, PlayerStats stats, Inventory inventory, StoryEventData startingStory)
    {
        _stateMachine = stateMachine;
        _inventory = inventory;
        _stats = stats;
        _startingStory = startingStory;
    }

    public void Enter()
    {
        Debug.Log("INTRO");
        _stateMachine.CurrentStoryEvent = _startingStory;
    }

    public void Exit()
    {
        
    }

    public void Tick()
    {
        // change on first update tick
        _stateMachine.ChangeState(_stateMachine.PageState);
    }
}
