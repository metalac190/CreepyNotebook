using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Button))]
public class ChoiceButton : MonoBehaviour, IDisplayable
{
    public event Action<Choice> ChoiceClicked;
    public event Action ShowCompleted;
    public event Action HideCompleted;

    [SerializeField] TextMeshProUGUI _textView = null;
    public string Text => _textView.text;

    [Header("Animation Settings")]
    [SerializeField] float _xAnimOffset = 100f;
    [SerializeField] float _startDelayInSeconds = .2f;
    [SerializeField] float _showSpeedInSeconds = .2f;
    [SerializeField] float _hideDelayInSeconds = .1f;
    [SerializeField] float _hideSpeedInSeconds = .2f;

    float _startXPos = 0;
    Button _buttonUI = null;
    Image _imageUI = null;
    CanvasGroup _canvasGroup = null;
    Coroutine _animationCoroutine = null;

    public Choice Choice { get; private set; }
    public bool IsRevealed { get; private set; } = false;

    private void Awake()
    {
        // get references
        _buttonUI = GetComponent<Button>();
        _imageUI = _buttonUI.image;
        _canvasGroup = GetComponent<CanvasGroup>();
        // set field defaults
        _startXPos = transform.localPosition.x;
        _imageUI.raycastTarget = false;
        // clear by default
        Clear();
    }

    void OnEnable()
    {
        _buttonUI.onClick.AddListener(OnButtonClicked);
    }

    void OnDisable()
    {
        _buttonUI.onClick.RemoveListener(OnButtonClicked);
    }

    public void SetChoice(Choice choice)
    {
        Choice = choice;
    }

    public void Display(string buttonText)
    {
        _textView.text = buttonText;
    }

    public void Clear()
    {
        _textView.text = string.Empty;
        Choice = null;
        _canvasGroup.alpha = 0;
        _imageUI.raycastTarget = false;
    }

    public void CompleteReveal()
    {
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);
        // ensure we hit our final value
        _canvasGroup.alpha = 1;
        transform.localPosition = new Vector2(_startXPos, transform.localPosition.y);
        // only allow button to be clickable if animation has finished
        _imageUI.raycastTarget = true;

        IsRevealed = true;
        ShowCompleted?.Invoke();
    }

    public void Reveal()
    {
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);
        _animationCoroutine = StartCoroutine(AnimateRevealRoutine(_startDelayInSeconds, _showSpeedInSeconds));
    }

    public void Hide()
    {
        // if we're currently animating, kill it early to start our new one
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);
        _animationCoroutine = StartCoroutine(AnimateHideRoutine(_hideDelayInSeconds, _hideSpeedInSeconds));
    }

    IEnumerator AnimateRevealRoutine(float startDelay, float showSpeedInSeconds)
    {
        // flag button state to be revealed, at first
        IsRevealed = false;

        float currentAlpha = 0;
        _canvasGroup.alpha = 0;
        // x offset data
        float animStartXPos = _startXPos - _xAnimOffset;
        float animEndXPos = _startXPos;
        float currentXPos = animStartXPos;

        yield return new WaitForSeconds(startDelay);

        for (float t = 0f; t < 1.0f; t += Time.deltaTime / showSpeedInSeconds)
        {
            // opacity animate
            currentAlpha = Mathf.Lerp(0f, 1f, t);
            _canvasGroup.alpha = currentAlpha;
            // position animate
            currentXPos = Mathf.Lerp(animStartXPos, animEndXPos, t);
            transform.localPosition = new Vector2(currentXPos, transform.localPosition.y);
            yield return null;
        }
        // ensure we hit our final value
        _canvasGroup.alpha = 1;
        transform.localPosition = new Vector2(animEndXPos, transform.localPosition.y);
        // only allow button to be clickable if animation has finished
        _imageUI.raycastTarget = true;
        // set reveal state, now that we're completed
        IsRevealed = true;

        ShowCompleted?.Invoke();
    }

    IEnumerator AnimateHideRoutine(float startDelay, float hideSpeedInSeconds)
    {

        IsRevealed = true;

        float currentAlpha = 1;
        _canvasGroup.alpha = 1;
        _imageUI.raycastTarget = false;

        //yield return new WaitForSeconds(startDelay);

        for (float t = 1f; t > 0; t -= Time.deltaTime / hideSpeedInSeconds)
        {
            currentAlpha = Mathf.Lerp(0, 1, t);
            _canvasGroup.alpha = currentAlpha;
            yield return null;
        }
        // ensure we hit our final value
        _canvasGroup.alpha = 0;
        // set reveal state, now that we're completed
        IsRevealed = false;

        HideCompleted?.Invoke();
    }

    void OnButtonClicked()
    {
        ChoiceClicked?.Invoke(Choice);
    }
}

