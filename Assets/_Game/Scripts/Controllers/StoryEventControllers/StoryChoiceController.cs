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

    bool _finishedShowAnimation = false;

    #region MonoBehaviour
    private void Awake()
    {
        _decisionView = GetComponent<StoryChoiceView>();
    }

    private void OnEnable()
    {
        // listen for when Intro Animation has completed
        _decisionView.RevealCompleted += OnShowCompleted;
        // listen for button choices
        _calmChoice.ChoiceClicked += OnChoiceClicked;
        _survivalChoice.ChoiceClicked += OnChoiceClicked;
        _tenacityChoice.ChoiceClicked += OnChoiceClicked;
    }

    private void OnDisable()
    {
        _decisionView.RevealCompleted -= OnShowCompleted;
        
        _calmChoice.ChoiceClicked -= OnChoiceClicked;
        _survivalChoice.ChoiceClicked -= OnChoiceClicked;
        _tenacityChoice.ChoiceClicked -= OnChoiceClicked;
    }
    #endregion

    #region Public
    public void Begin(StoryChoice storyDecision)
    {
        // progression state
        _finishedShowAnimation = false;
        // set up choice buttons

        // display it
        _decisionView.Display(storyDecision);
        _decisionView.Reveal();

    }
    // on progress attempt, auto finish animation
    public void Progress()
    {
        if(_finishedShowAnimation == false)
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
        _decisionView.Hide();
        _decisionView.Clear();
    }
    #endregion

    #region Callbacks
    void OnShowCompleted()
    {
        _finishedShowAnimation = true;
    }

    void OnChoiceClicked(Choice choice)
    {
        ChoiceMade?.Invoke(choice);
    }
    #endregion
}
