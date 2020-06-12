using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class TMProHelper
{
    public static Coroutine RevealText(MonoBehaviour monoBehaviour, Coroutine coroutine, 
        TextMeshProUGUI tmproText, string textToReveal, float characterRevealTime)
    {
        if (coroutine != null)
            monoBehaviour.StopCoroutine(coroutine);
        return monoBehaviour.StartCoroutine(RevealTextCoroutine
            (tmproText, textToReveal, characterRevealTime));
    }

    private static IEnumerator RevealTextCoroutine(TextMeshProUGUI tmproText, 
        string textToReveal, float characterRevealTime)
    {
        tmproText.ForceMeshUpdate();

        tmproText.text = textToReveal;
        int totalVisibleCharacters = tmproText.textInfo.characterCount;
        int counter = 0;
        // reveal text one character at a time loop
        while (counter <= totalVisibleCharacters)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);

            tmproText.maxVisibleCharacters = visibleCount;

            counter += 1;

            yield return new WaitForSeconds(characterRevealTime);
        }
    }
}
