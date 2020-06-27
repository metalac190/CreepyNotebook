using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StoryChoiceView : MonoBehaviour
{
    public event Action TextRevealStarted = delegate { };
    public event Action TextRevealCompleted = delegate { };
    public event Action ButtonRevealCompleted = delegate { };
    public event Action ButtonHideCompleted = delegate { };

    [Header("Decision Canvas")]
    [SerializeField] Canvas _choiceCanvas = null;
    [SerializeField] TextMeshProUGUI _textUI = null;

    [Header("Text Animation")]
    [SerializeField] RevealText _textAnimation = null;
    [SerializeField] float _hideSpeedInSeconds = .1f;

    [Header("Choice Buttons")]
    [SerializeField] ChoiceButton _calmButton = null;
    [SerializeField] ChoiceButton _survivalButton = null;
    [SerializeField] ChoiceButton _tenacityButton = null;

    [Header("Panel Animation")]
    [SerializeField] float _delayBetweenShowButtons = .15f;
    [SerializeField] float _delayBetweenHideButtons = .1f;

    const float DELAY_BETWEEN_CHARACTERS = .03f;    // base delay amount
    float _textDelayModifier = 0;   // modified by particular story content
    public float TextRevealDelay => DELAY_BETWEEN_CHARACTERS + _textDelayModifier;  // combine it for the total

    Coroutine _viewAnimatorRoutine = null;

    private void Awake()
    {
        // ensure that if designer left canvas on that it's handled
        DisableCanvas();
    }

    private void OnEnable()
    {
        _textAnimation.RevealCompleted += OnTextRevealCompleted;
        // intro animation events
        _calmButton.ShowCompleted += OnButtonRevealCompleted;
        _survivalButton.ShowCompleted += OnButtonRevealCompleted;
        _tenacityButton.ShowCompleted += OnButtonRevealCompleted;
        // exit animation events
        _calmButton.HideCompleted += OnButtonHideCompleted;
        _survivalButton.HideCompleted += OnButtonHideCompleted;
        _tenacityButton.HideCompleted += OnButtonHideCompleted;
        // canvas
        ButtonHideCompleted += DisableCanvas;
    }

    private void OnDisable()
    {
        _textAnimation.RevealCompleted -= OnTextRevealCompleted;
        // intro animation events
        _calmButton.ShowCompleted -= OnButtonRevealCompleted;
        _survivalButton.ShowCompleted -= OnButtonRevealCompleted;
        _tenacityButton.ShowCompleted -= OnButtonRevealCompleted;
        // exit animation events
        _calmButton.HideCompleted -= OnButtonHideCompleted;
        _survivalButton.HideCompleted -= OnButtonHideCompleted;
        _tenacityButton.HideCompleted -= OnButtonHideCompleted;
        // canvas
        ButtonHideCompleted -= DisableCanvas;
    }

    public void Reveal()
    {
        EnableCanvas();

        TextRevealStarted?.Invoke();
        _textAnimation.Reveal(TextRevealDelay);
    }

    public void CompleteReveal()
    {
        _textAnimation.CompleteReveal();
    }

    public void Display(StoryChoice storyDecision)
    {
        _textUI.text = storyDecision.DecisionPrompt;

        _textDelayModifier = storyDecision.TextSpeedModifier;
        // choices
        _calmButton.Display(storyDecision.CalmChoice.ButtonText);
        _survivalButton.Display(storyDecision.SurvivalChoice.ButtonText);
        _tenacityButton.Display(storyDecision.TenacityChoice.ButtonText);
    }

    public void Clear()
    {
        _textUI.text = string.Empty;
        // clear buttons
        _calmButton.Clear();
        _survivalButton.Clear();
        _tenacityButton.Clear();

        _textDelayModifier = 0;
    }

    void OnTextRevealCompleted()
    {
        TextRevealCompleted?.Invoke();
        RevealButtons();
    }

    private void RevealButtons()
    {
        // animate buttons
        if (_viewAnimatorRoutine != null)
            StopCoroutine(_viewAnimatorRoutine);
        _viewAnimatorRoutine = StartCoroutine(ViewRevealRoutine());
    }

    IEnumerator ViewRevealRoutine()
    {
        //TODO panel animation here
        _calmButton.Reveal();
        yield return new WaitForSeconds(_delayBetweenShowButtons);
        _survivalButton.Reveal();
        yield return new WaitForSeconds(_delayBetweenShowButtons);
        _tenacityButton.Reveal();
    }

    public void Hide()
    {
        // animate buttons
        if (_viewAnimatorRoutine != null)
            StopCoroutine(_viewAnimatorRoutine);

        _viewAnimatorRoutine = StartCoroutine(ViewHideRoutine());
    }

    IEnumerator ViewHideRoutine()
    {

        _textAnimation.Hide(_hideSpeedInSeconds);
        // animate buttons
        _calmButton.Hide();
        yield return new WaitForSeconds(_delayBetweenHideButtons);
        _survivalButton.Hide();
        yield return new WaitForSeconds(_delayBetweenHideButtons);
        _tenacityButton.Hide();
        yield return new WaitForSeconds(_delayBetweenHideButtons);
    }

    void OnButtonRevealCompleted()
    {
        CheckAllButtonsRevealed();
    }

    void OnButtonHideCompleted()
    {
        CheckAllButtonsHidden();
    }

    void CheckAllButtonsRevealed()
    {
        // if all 3 buttons are revealed, send out a notification
        if (_calmButton.IsRevealed 
            && _survivalButton.IsRevealed 
            && _tenacityButton.IsRevealed)
        {
            ButtonRevealCompleted?.Invoke();
        }
    }

    void CheckAllButtonsHidden()
    {
        // if all 3 buttons are hidden, send notification
        if (_calmButton.IsRevealed == false
            && _survivalButton.IsRevealed == false
            && _tenacityButton.IsRevealed == false)
        {
            ButtonHideCompleted?.Invoke();
        }
    }

    void EnableCanvas()
    {
        _choiceCanvas.gameObject.SetActive(true);
    }

    void DisableCanvas()
    {
        Clear();
        _choiceCanvas.gameObject.SetActive(false);
    }
}
