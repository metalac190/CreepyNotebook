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
    public event Action EnterCompleted;
    public event Action ExitCompleted;

    [SerializeField] TextMeshProUGUI _textView = null;
    public string Text => _textView.text;

    [Header("Animation Settings")]
    [SerializeField] float xAnimOffset = 100f;
    [SerializeField] float _startDelayInSeconds = .2f;
    [SerializeField] float _showSpeedInSeconds = .2f;
    [SerializeField] float _hideSpeedInSeconds = .2f;

    float _startXPos = 0;
    Button _buttonUI = null;
    Image _imageUI = null;
    CanvasGroup _canvasGroup = null;
    Coroutine _animationCoroutine = null;

    private void Awake()
    {
        // get references
        _buttonUI = GetComponent<Button>();
        _imageUI = _buttonUI.image;
        _canvasGroup = GetComponent<CanvasGroup>();
        // set field defaults
        _startXPos = transform.localPosition.x;
        _imageUI.raycastTarget = false;
    }

    public void SetupButton(Action actionOnClick)
    {
        // make sure we've disposed of our previous listeners
        _buttonUI.onClick.RemoveAllListeners();
        _buttonUI.onClick.AddListener(delegate { actionOnClick.Invoke(); } );
    }

    public void Display(string buttonText)
    {
        _textView.text = buttonText;
    }

    public void Clear()
    {
        _textView.text = string.Empty;
    }

    IEnumerator AnimateEnter(float startDelay, float showSpeedInSeconds)
    {
        float currentAlpha = 0;
        _canvasGroup.alpha = 0;
        // x offset data
        float animStartXPos = _startXPos - xAnimOffset;
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
    }

    IEnumerator AnimateExit(float startDelay, float hideSpeedInSeconds)
    {
        float currentAlpha = 1;
        _canvasGroup.alpha = 1;
        _imageUI.raycastTarget = false;

        yield return new WaitForSeconds(startDelay);

        for (float t = 1f; t > 0; t -= Time.deltaTime / hideSpeedInSeconds)
        {
            currentAlpha = Mathf.Lerp(0, 1f, t);
            _canvasGroup.alpha = currentAlpha;
            yield return null;
        }
        // ensure we hit our final value
        _canvasGroup.alpha = 0;

        gameObject.SetActive(false);
    }

    public void Show()
    {
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);
        _animationCoroutine = StartCoroutine(AnimateEnter(_startDelayInSeconds, _showSpeedInSeconds));
    }

    public void Hide()
    {
        // if we're currently animating, kill it early to start our new one
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);
        _animationCoroutine = StartCoroutine(AnimateExit(_startDelayInSeconds, _hideSpeedInSeconds));
    }
}

