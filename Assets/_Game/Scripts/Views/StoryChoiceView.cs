using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StoryChoiceView : MonoBehaviour
{
    public event Action RevealCompleted;

    [Header("Decision Canvas")]
    [SerializeField] Canvas _choiceCanvas = null;
    [SerializeField] TextMeshProUGUI _textUI = null;
    [SerializeField] RectTransform _choicePanel = null;

    [Header("Choice Buttons")]
    [SerializeField] ChoiceButton _calmButton = null;
    [SerializeField] ChoiceButton _survivalButton = null;
    [SerializeField] ChoiceButton _tenacityButton = null;

    bool _calmButtonRevealed = false;
    bool _survivalButtonRevealed = false;
    bool _tenacityButtonRevealed = false;

    private void Awake()
    {
        // ensure that if designer left canvas on that it's handled
        _choiceCanvas.gameObject.SetActive(false);
        _choicePanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _calmButton.EnterCompleted += OnCalmRevealCompleted;
        _survivalButton.EnterCompleted += OnSurvivalRevealCompleted;
        _tenacityButton.EnterCompleted += OnTenacityRevealCompleted;
    }

    private void OnDisable()
    {
        _calmButton.EnterCompleted -= OnCalmRevealCompleted;
        _survivalButton.EnterCompleted -= OnSurvivalRevealCompleted;
        _tenacityButton.EnterCompleted -= OnTenacityRevealCompleted;
    }

    public void Reveal()
    {
        //TODO replace with Animation
        Debug.Log("Enable Buttons");
        _choiceCanvas.gameObject.SetActive(true);
        _choicePanel.gameObject.SetActive(true);
        Debug.Log("Reveal Buttons");
        // animate buttons
        _calmButton.Reveal();
        _survivalButton.Reveal();
        _tenacityButton.Reveal();
        // reset button states, to track when all 3 have finished animating
        ResetButtonRevealStates();
    }

    public void CompleteReveal()
    {
        Debug.Log("Completed Reveal");
        //TODO replace with Animation
        _choiceCanvas.gameObject.SetActive(true);
        _choicePanel.gameObject.SetActive(true);
        // animate buttons
        _calmButton.Reveal();
        _survivalButton.Reveal();
        _tenacityButton.Reveal();
        Debug.Log("TODO: Complete Animation");
        RevealCompleted?.Invoke();
    }

    public void Display(StoryChoice storyDecision)
    {
        _textUI.text = storyDecision.DecisionPrompt;
        // choices
        _calmButton.SetChoice(storyDecision.CalmChoice);
        _survivalButton.SetChoice(storyDecision.SurvivalChoice);
        _tenacityButton.SetChoice(storyDecision.TenacityChoice);
    }

    public void Clear()
    {
        _textUI.text = string.Empty;
        // clear buttons
        _calmButton.Clear();
        _survivalButton.Clear();
        _tenacityButton.Clear();
    }

    public void Hide()
    {
        //TODO replace with Animation
        _choiceCanvas.gameObject.SetActive(false);
        _choicePanel.gameObject.SetActive(false);
        // animate buttons
        _calmButton.Hide();
        _survivalButton.Hide();
        _tenacityButton.Hide();
    }

    void ResetButtonRevealStates()
    {
        _calmButtonRevealed = false;
        _survivalButtonRevealed = false;
        _tenacityButtonRevealed = false;
    }

    void OnCalmRevealCompleted()
    {
        _calmButtonRevealed = true;
        CheckAnimationFinished();
    }

    void OnSurvivalRevealCompleted()
    {
        _survivalButtonRevealed = true;
        CheckAnimationFinished();
    }

    void OnTenacityRevealCompleted()
    {
        _tenacityButtonRevealed = true;
        CheckAnimationFinished();
    }

    // if all 3 buttons are revealed, send out a notification
    void CheckAnimationFinished()
    {
        if(_calmButtonRevealed && _survivalButtonRevealed && _tenacityButtonRevealed)
        {
            RevealCompleted?.Invoke();
        }
    }
}
