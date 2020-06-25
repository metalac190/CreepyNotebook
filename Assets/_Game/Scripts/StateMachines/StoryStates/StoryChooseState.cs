using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryChooseState : IState
{
    private StorySM _stateMachine = null;
    private InputManager _input = null;
    private StoryChoiceController _decisionController = null;

    public StoryChooseState(StorySM stateMachine, InputManager input, StoryChoiceController decisionController)
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
        // display
        DisplayStoryChoice();
    }

    private void DisplayStoryChoice()
    {
        StoryChoice newStoryChoice = _stateMachine.CurrentStoryEvent.StoryChoice;
        if (newStoryChoice != null)
        {
            _decisionController.Begin(newStoryChoice);
        }
    }

    void HideStoryChoice()
    {
        StoryChoice newStoryChoice = _stateMachine.CurrentStoryEvent.StoryChoice;
        if (newStoryChoice != null)
        {
            _decisionController.Hide();
        }
    }

    public void Exit()
    {
        //TODO STOP listening for button clicks
        _input.Clicked -= OnClicked;
        _decisionController.ChoiceMade -= OnChoiceMade;
        // Hide display
        HideStoryChoice();
    }

    public void Tick()
    {
        
    }

    void OnClicked()
    {
        _decisionController.Progress();
    }

    void OnChoiceMade(Choice choice)
    {
        //TODO resolve the choice
        _stateMachine.ChosenStoryResult = choice.ChoiceOutcome.ChosenStoryPages;
    }
}
