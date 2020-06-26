using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StoryContentImageView : MonoBehaviour
{
    public event Action RevealAnimationStarted;
    public event Action RevealAnimationCompleted;

    [SerializeField] Canvas _imageCanvas = null;
    [SerializeField] Image _imageUI = null;
    [SerializeField] TextMeshProUGUI _imageTextUI = null;
    [SerializeField] RevealText _textAnimation = null;

    const float DELAY_BETWEEN_CHARACTERS = .03f;    // base delay amount
    float _delayModifier = 0;   // modified by particular story content
    public float TextRevealDelay => DELAY_BETWEEN_CHARACTERS + _delayModifier;  // combine it for the total

    private void Awake()
    {
        DisableCanvas();
    }

    private void OnEnable()
    {
        _textAnimation.RevealStarted += OnRevealStarted;
        _textAnimation.RevealCompleted += OnRevealCompleted;
    }

    private void OnDisable()
    {
        _textAnimation.RevealStarted -= OnRevealStarted;
        _textAnimation.RevealCompleted -= OnRevealCompleted;
    }

    void Clear()
    {
        _imageUI.sprite = null;
        _imageTextUI.text = string.Empty;

        _delayModifier = 0;
    }

    public void Display(StoryPage storyImage)
    {
        _imageUI.sprite = storyImage.Graphic;
        _imageTextUI.text = storyImage.Text;

        _delayModifier = storyImage.TextSpeedModifier;
    }

    public void Reveal()
    {
        _imageCanvas.gameObject.SetActive(true);
        //RevealAnimationStarted?.Invoke();

        _textAnimation.Reveal(TextRevealDelay);
    }

    public void CompleteReveal()
    {
        Debug.Log("Reveal Text immediate!");
        _imageCanvas.gameObject.SetActive(true);

        _textAnimation.CompleteReveal();
    }

    public void Hide()
    {
        DisableCanvas();
    }

    public void EnableCanvas()
    {
        _imageCanvas.gameObject.SetActive(true);
    }

    public void DisableCanvas()
    {
        Clear();
        _imageCanvas.gameObject.SetActive(false);
    }

    void OnRevealStarted()
    {
        RevealAnimationStarted?.Invoke();
    }

    void OnRevealCompleted()
    {
        RevealAnimationCompleted?.Invoke();
    }
}
