using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HUDView))]
public class HUDController : MonoBehaviour
{
    HUDView _HUDView = null;

    private void Awake()
    {
        _HUDView = GetComponent<HUDView>();
    }

    public void ShowPrompt(string prompt)
    {
        _HUDView.DisplayPrompt(prompt);
    }

    public void HidePrompt()
    {
        _HUDView.ClearPrompt();
    }
}
