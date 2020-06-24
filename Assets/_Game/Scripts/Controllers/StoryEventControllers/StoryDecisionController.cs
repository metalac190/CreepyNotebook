using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDecisionController : MonoBehaviour
{
    public event Action<Choice> ChoiceMade;

    [Header("Prompts")]
    [SerializeField] string _continuePromptText = "Choose...";
    public string ContinuePromptText => _continuePromptText;

    StoryChoice _currentStoryDecision = null;
    StoryDecisionView _decisionView = null;

    bool _finishedShowAnimation = false;

    #region MonoBehaviour
    private void Awake()
    {
        _decisionView = GetComponent<StoryDecisionView>();
    }

    private void OnEnable()
    {
        _decisionView.ShowCompleted += OnShowCompleted;
    }

    private void OnDisable()
    {
        _decisionView.ShowCompleted -= OnShowCompleted;
    }
    #endregion

    #region Public
    public void Begin(StoryChoice storyDecision)
    {
        // store data
        _currentStoryDecision = storyDecision;
        // progression state
        _finishedShowAnimation = false;
        // display it
        _decisionView.Display(storyDecision);
        _decisionView.Show();
    }
    // on progress attempt, auto finish animation
    public void Progress()
    {
        //TODO auto finish display
        _decisionView.ShowImmediate();
    }

    public void Choose(Choice choice)
    {
        ChoiceMade.Invoke(choice);
    }
    #endregion



    #region Callbacks
    void OnShowCompleted()
    {
        _finishedShowAnimation = true;
    }
    #endregion
}
