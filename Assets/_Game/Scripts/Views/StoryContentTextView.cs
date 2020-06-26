using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StoryContentTextView : MonoBehaviour
{
    public event Action RevealAnimationStarted;
    public event Action RevealAnimationCompleted;

    [SerializeField] Canvas _textCanvas = null;
    [SerializeField] TextMeshProUGUI _textUI = null;
    [SerializeField] RevealText _textAnimation = null;

    const float DELAY_BETWEEN_CHARACTERS = .03f;
    float _delayModifier = 0;
    public float TextRevealDelay => DELAY_BETWEEN_CHARACTERS + _delayModifier;

    private void Awake()
    {
        _textCanvas.gameObject.SetActive(false);
        Clear();
    }

    private void OnEnable()
    {
        _textAnimation.RevealCompleted += OnRevealCompleted;
    }

    private void OnDisable()
    {
        _textAnimation.RevealCompleted -= OnRevealCompleted;
    }

    public void Display(StoryPage storyPage)
    {
        _textUI.text = storyPage.Text;
        _delayModifier = storyPage.TextSpeedModifier;
    }

    void Clear()
    {
        _textUI.text = string.Empty;

        _delayModifier = 0;
    }

    public void Reveal()
    {
        //TODO replace with Animations
        _textCanvas.gameObject.SetActive(true);
        RevealAnimationStarted?.Invoke();

        _textAnimation.Reveal(TextRevealDelay);
    }

    public void CompleteReveal()
    {
        _textCanvas.gameObject.SetActive(true);

        _textAnimation.CompleteReveal();
        // complete the animation here
        // send out the event that it has completed
    }

    public void Hide()
    {
        //TODO replace with Animations
        _textCanvas.gameObject.SetActive(false);
    }

    void OnRevealCompleted()
    {
        RevealAnimationCompleted?.Invoke();
    }
}
