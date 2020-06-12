using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RevealText : MonoBehaviour
{
    public event Action OnAnimationFinish = delegate { };

    bool _isPaused = false;

    TextMeshProUGUI _textView = null;
    Coroutine _revealRoutine = null;

    public void AnimateText(string text, float characterRevealTime)
    {
        if (_revealRoutine != null)
            StopCoroutine(_revealRoutine);
        _revealRoutine = StartCoroutine(AnimateTextCoroutine(text, characterRevealTime));
    }

    public void PauseText(bool shouldPause)
    {
        _isPaused = shouldPause;
    }

    IEnumerator AnimateTextCoroutine(string textToReveal, float characterRevealTime)
    {
        _textView.ForceMeshUpdate();

        _textView.text = textToReveal;
        int totalVisibleCharacters = _textView.textInfo.characterCount;
        int counter = 0;

        while (counter <= totalVisibleCharacters && !_isPaused)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);

            _textView.maxVisibleCharacters = visibleCount;

            counter += 1;

            yield return new WaitForSeconds(characterRevealTime);
        }

        OnAnimationFinish.Invoke();
    }
}

