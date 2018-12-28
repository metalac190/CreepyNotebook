using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RevealText : MonoBehaviour
{
    public event Action OnAnimationFinish = delegate { };

    TextMeshProUGUI textUI;
    Coroutine animationInstance;

    public void Initialize(TextMeshProUGUI textUI)
    {
        this.textUI = textUI;
    }

    public void AnimateText(float characterRevealTime)
    {
        // stop the old one, if there's already one playing
        if (animationInstance != null)
        {
            StopCoroutine(animationInstance);
        }
        // start a new animation
        animationInstance = StartCoroutine(IEAnimateText(characterRevealTime));
    }

    IEnumerator IEAnimateText(float characterRevealTime)
    {
        textUI.ForceMeshUpdate();

        int totalVisibleCharacters = textUI.textInfo.characterCount;
        int counter = 0;

        while (counter <= totalVisibleCharacters)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);

            textUI.maxVisibleCharacters = visibleCount;

            counter += 1;

            yield return new WaitForSeconds(characterRevealTime);
        }

        OnAnimationFinish.Invoke();
    }
}
