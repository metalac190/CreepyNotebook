using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RevealText : MonoBehaviour
{
    public event Action RevealStarted = delegate { };
    public event Action RevealCompleted = delegate { };
    public event Action HideStarted = delegate { };
    public event Action HideCompleted = delegate { };

    bool _isPaused = false;

    TextMeshProUGUI _textUI = null;
    Coroutine _animationRoutine = null;

    private void Awake()
    {
        _textUI = GetComponent<TextMeshProUGUI>();
    }

    public void Reveal(float delayBetweenCharacters)
    {
        RevealStarted?.Invoke();

        if (_animationRoutine != null)
            StopCoroutine(_animationRoutine);
        _animationRoutine = StartCoroutine(RevealRoutine(delayBetweenCharacters));
    }

    public void CompleteReveal()
    {
        if (_animationRoutine != null)
            StopCoroutine(_animationRoutine);
        // make all characters visible
        _textUI.maxVisibleCharacters = _textUI.textInfo.characterCount;

        RevealCompleted?.Invoke();
    }

    public void Hide(float hideSpeedInSeconds)
    {
        HideStarted?.Invoke();

        if (_animationRoutine != null)
            StopCoroutine(_animationRoutine);
        _animationRoutine = StartCoroutine(HideRoutine(hideSpeedInSeconds));
    }

    public void PauseText(bool shouldPause)
    {
        _isPaused = shouldPause;
    }

    IEnumerator RevealRoutine(float delayBetweenCharacters)
    {
        _textUI.ForceMeshUpdate();

        _textUI.alpha = 1;
        int totalVisibleCharacters = _textUI.textInfo.characterCount;
        int counter = 0;

        while (counter <= totalVisibleCharacters && !_isPaused)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _textUI.maxVisibleCharacters = visibleCount;
            counter += 1;

            yield return new WaitForSeconds(delayBetweenCharacters);
        }

        RevealCompleted?.Invoke();
    }

    IEnumerator HideRoutine(float hideSpeedInSeconds)
    {
        _textUI.ForceMeshUpdate();

        float currentAlpha = 1;

        for (float t = 0f; t < 1.0f; t += Time.deltaTime / hideSpeedInSeconds)
        {
            // opacity animate
            currentAlpha = Mathf.Lerp(1, 0, t);
            _textUI.alpha = currentAlpha;
            yield return null;
        }
        _textUI.alpha = 0;

        HideCompleted?.Invoke();
    }
}

