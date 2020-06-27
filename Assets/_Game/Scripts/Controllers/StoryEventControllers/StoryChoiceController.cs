using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoryChoiceController : MonoBehaviour
{
    public event Action<Choice> ChoiceMade;

    [Header("Prompts")]
    [SerializeField] string _continuePromptText = "Choose...";
    public string ContinuePromptText => _continuePromptText;

    [Header("Buttons")]
    [SerializeField] ChoiceButton _calmChoice = null;
    [SerializeField] ChoiceButton _survivalChoice = null;
    [SerializeField] ChoiceButton _tenacityChoice = null;

    StoryChoiceView _decisionView = null;

    bool _isRevealingText = false;

    #region MonoBehaviour
    private void Awake()
    {
        _decisionView = GetComponent<StoryChoiceView>();
    }

    private void OnEnable()
    {
        // listen for when Intro Animation has completed
        _decisionView.TextRevealStarted += OnRevealTextStarted;
        _decisionView.TextRevealCompleted += OnRevealTextCompleted;
        // listen for button choices
        _calmChoice.ChoiceClicked += OnChoiceClicked;
        _survivalChoice.ChoiceClicked += OnChoiceClicked;
        _tenacityChoice.ChoiceClicked += OnChoiceClicked;
    }

    private void OnDisable()
    {
        _decisionView.TextRevealStarted -= OnRevealTextStarted;
        _decisionView.TextRevealCompleted -= OnRevealTextCompleted;

        _calmChoice.ChoiceClicked -= OnChoiceClicked;
        _survivalChoice.ChoiceClicked -= OnChoiceClicked;
        _tenacityChoice.ChoiceClicked -= OnChoiceClicked;
    }
    #endregion

    #region Public
    public void Begin(StoryChoice storyDecision)
    {
        // progression state
        _isRevealingText = false;
        // set up choice buttons
        _calmChoice.SetChoice(storyDecision.CalmChoice);
        _survivalChoice.SetChoice(storyDecision.SurvivalChoice);
        _tenacityChoice.SetChoice(storyDecision.TenacityChoice);
        // display it
        _decisionView.Display(storyDecision);
        _decisionView.Reveal();
    }
    // on progress attempt, auto finish animation
    public void Progress()
    {
        if(_isRevealingText == true)
        {
            _decisionView.CompleteReveal();
        }
    }

    public void Choose(Choice choice)
    {
        ChoiceMade.Invoke(choice);
    }

    public void Hide()
    {
        Debug.Log("Story Choice Controller: Hide");
        _decisionView.Hide();
    }
    #endregion

    #region Callbacks
    void OnRevealTextStarted()
    {
        _isRevealingText = true;
    }

    void OnRevealTextCompleted()
    {
        _isRevealingText = false;
    }

    void OnChoiceClicked(Choice choice)
    {
        
        ChoiceMade?.Invoke(choice);
    }
    #endregion
}
