using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryChooseState : IState
{
    private StorySM _stateMachine = null;
    private InputManager _input = null;
    private StoryDecisionController _decisionController = null;

    public StoryChooseState(StorySM stateMachine, InputManager input, StoryDecisionController decisionController)
    {
        _stateMachine = stateMachine;
        _input = input;
        _decisionController = decisionController;
    }

    public void Enter()
    {
        Debug.Log("CONTENT IDLE");
        _input.Clicked += OnClicked;
        _decisionController.ChoiceMade += OnChoiceMade;
    }

    public void Exit()
    {
        //TODO STOP listening for button clicks
        _input.Clicked -= OnClicked;
        _decisionController.ChoiceMade -= OnChoiceMade;
    }

    public void Tick()
    {
        
    }

    void OnClicked()
    {
        //
    }

    void OnChoiceMade(Choice choice)
    {
        //TODO resolve the choice
    }
}
