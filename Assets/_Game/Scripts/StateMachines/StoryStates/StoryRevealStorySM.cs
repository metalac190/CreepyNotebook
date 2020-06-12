using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryRevealState : IState
{
    StorySM _stateMachine = null;
    InputController _input = null;
    StoryController _story = null;

    public StoryRevealState(StorySM stateMachine, InputController input, StoryController story)
    {
        _stateMachine = stateMachine;
        _input = input;
        _story = story;
    }

    public void Enter()
    {
        Debug.Log("STORY: Display");
        _input.OnClicked += HandleClicked;
        //TODO progress story here
    }

    public void Exit()
    {
        _input.OnClicked -= HandleClicked;
    }

    public void Tick()
    {
        
    }

    void HandleClicked()
    {

    }
}
