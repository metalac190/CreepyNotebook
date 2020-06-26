using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIntroState : IState
{
    StorySM _stateMachine = null;
    Inventory _inventory = null;    // may need later for initialization
    PlayerStats _stats = null;      // may need later for initialization
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
    }

    public void Exit()
    {
        
    }

    public void Tick()
    {
        StartFirstStory();

    }

    void StartFirstStory()
    {
        // change on first update tick
        _stateMachine.SetStory(_startingStory);
        _stateMachine.ChangeState(_stateMachine.PageState);
    }
}
