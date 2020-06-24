using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTransitionState : IState
{
    StorySM _stateMachine = null;
    PlayerStats _stats = null;
    Inventory _inventory = null;

    public StoryTransitionState(StorySM stateMachine, PlayerStats stats, Inventory inventory)
    {
        _stateMachine = stateMachine;
        _stats = stats;
        _inventory = inventory;
    }

    public void Enter()
    {
        Debug.Log("TRANSITION STATE");
    }

    public void Exit()
    {
        
    }

    public void Tick()
    {
        BeginNextEvent();
    }

    void BeginNextEvent()
    {
        SetNextStory();

        _stateMachine.CurrentStoryEvent = _stateMachine.NextStoryEvent;
        // clear the exit, for testing
        _stateMachine.NextStoryEvent = null;
        // transition
        _stateMachine.ChangeState(_stateMachine.PageState);
    }

    private void SetNextStory()
    {
        // if we haven't set our Next Story yet, do it now
        // note: if we've already set a NextStory (in Choice, for example), NextStory will be null and ignore
        if (_stateMachine.NextStoryEvent == null)
        {
            _stateMachine.NextStoryEvent = _stateMachine.CurrentStoryEvent
                .StoryExit.GetExit(_stats, _inventory);

            if (_stateMachine.NextStoryEvent == null)
            {
                Debug.LogError("No exit set on story. Cannot proceed: " + _stateMachine.CurrentStoryEvent.name);
                _stateMachine.ChangeState(_stateMachine.ExitState);
            }
        }
    }
}
