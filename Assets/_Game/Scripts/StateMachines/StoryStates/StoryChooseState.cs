using UnityEngine;

/// <summary>
/// This is the Story State that gives the Player multiple options on buttons to choose from.
/// Once they choose an option, it resolves the story pages and then starts a new story.
/// </summary>
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
        Debug.Log("CHOOSE STATE");
        _input.Clicked += OnClicked;
        _decisionController.ChoiceMade += OnChoiceMade;
        // display
        DisplayStoryChoices();
    }

    private void DisplayStoryChoices()
    {
        StoryChoice newStoryChoice = _stateMachine.CurrentStoryEvent.StoryChoice;
        if (newStoryChoice != null)
        {
            _decisionController.Begin(newStoryChoice);
        }
    }

    public void Exit()
    {
        //TODO STOP listening for button clicks
        _input.Clicked -= OnClicked;
        _decisionController.ChoiceMade -= OnChoiceMade;
        // Hide display
        _decisionController.Hide();
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
        Debug.Log("Choice made: " + choice.ButtonText);

        _stateMachine.SetChoiceOutcome(choice.ChoiceOutcome);
        _stateMachine.ChangeState(_stateMachine.ChosenPagesState);
    }
}
