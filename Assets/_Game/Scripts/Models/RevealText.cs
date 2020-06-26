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

    bool _isPaused = false;

    TextMeshProUGUI _textUI = null;
    Coroutine _revealRoutine = null;

    private void Awake()
    {
        _textUI = GetComponent<TextMeshProUGUI>();
    }

    public void Reveal(float delayBetweenCharacters)
    {
        RevealStarted?.Invoke();

        if (_revealRoutine != null)
            StopCoroutine(_revealRoutine);
        _revealRoutine = StartCoroutine(AnimateTextCoroutine(delayBetweenCharacters));
    }

    public void CompleteReveal()
    {
        if (_revealRoutine != null)
            StopCoroutine(_revealRoutine);
        // make all characters visible
        _textUI.maxVisibleCharacters = _textUI.textInfo.characterCount;

        RevealCompleted?.Invoke();
    }

    public void PauseText(bool shouldPause)
    {
        _isPaused = shouldPause;
    }

    IEnumerator AnimateTextCoroutine(float delayBetweenCharacters)
    {
        _textUI.ForceMeshUpdate();

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
}

