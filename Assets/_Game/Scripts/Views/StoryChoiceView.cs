using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StoryChoiceView : MonoBehaviour
{
    public event Action TextRevealStarted;
    public event Action TextRevealCompleted;
    public event Action ButtonRevealCompleted;

    [Header("Decision Canvas")]
    [SerializeField] Canvas _choiceCanvas = null;
    [SerializeField] TextMeshProUGUI _textUI = null;

    [Header("Text Animation")]
    [SerializeField] RevealText _textAnimation = null;

    [Header("Choice Buttons")]
    [SerializeField] ChoiceButton _calmButton = null;
    [SerializeField] ChoiceButton _survivalButton = null;
    [SerializeField] ChoiceButton _tenacityButton = null;

    [Header("Panel Animation")]
    [SerializeField] float _delayBetweenShowButtons = .15f;
    [SerializeField] float _delayBetweenHideButtons = .1f;

    bool _calmButtonRevealed = false;
    bool _survivalButtonRevealed = false;
    bool _tenacityButtonRevealed = false;

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

        _calmButton.EnterCompleted += OnCalmRevealCompleted;
        _survivalButton.EnterCompleted += OnSurvivalRevealCompleted;
        _tenacityButton.EnterCompleted += OnTenacityRevealCompleted;
    }

    private void OnDisable()
    {
        _textAnimation.RevealCompleted -= OnTextRevealCompleted;

        _calmButton.EnterCompleted -= OnCalmRevealCompleted;
        _survivalButton.EnterCompleted -= OnSurvivalRevealCompleted;
        _tenacityButton.EnterCompleted -= OnTenacityRevealCompleted;
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

    public void Hide()
    {
        // animate buttons
        if (_viewAnimatorRoutine != null)
            StopCoroutine(_viewAnimatorRoutine);
        _viewAnimatorRoutine = StartCoroutine(ViewHideRoutine());
    }

    IEnumerator ViewHideRoutine()
    {
        // animate buttons
        _calmButton.Hide();
        yield return new WaitForSeconds(_delayBetweenHideButtons);
        _survivalButton.Hide();
        yield return new WaitForSeconds(_delayBetweenHideButtons);
        _tenacityButton.Hide();
        yield return new WaitForSeconds(_delayBetweenHideButtons);

        DisableCanvas();
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

        // reset button states, to track when all 3 have finished animating
        ResetButtonRevealStates();
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
            ButtonRevealCompleted?.Invoke();
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
