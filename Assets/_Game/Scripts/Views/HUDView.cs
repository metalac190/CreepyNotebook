using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDView : MonoBehaviour, IDisplayable
{
    [SerializeField] Canvas _HUDCanvas = null;
    [SerializeField] TextMeshProUGUI _promptTextUI = null;

    public void Display()
    {
        _HUDCanvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _HUDCanvas.gameObject.SetActive(false);
    }

    public void DisplayPrompt(string promptText)
    {
        _promptTextUI.text = promptText;
    }

    public void ClearPrompt()
    {
        _promptTextUI.text = string.Empty;
    }
}
