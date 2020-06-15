using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryIntroState : IState
{
    StorySM _stateMachine = null;
    StoryController _story = null;
    Inventory _inventory = null;
    PlayerStats _stats = null;

    public StoryIntroState(StorySM stateMachine, StoryController story, PlayerStats stats, Inventory inventory)
    {
        _stateMachine = stateMachine;
        _story = story;
        _inventory = inventory;
        _stats = stats;
    }

    public void Enter()
    {
        Debug.Log("STORY: Intro State");
        _story.Initialize();
        
    }

    public void Exit()
    {
        
    }

    public void Tick()
    {
        _stateMachine.ChangeState(_stateMachine.StoryRevealState);
    }
}
