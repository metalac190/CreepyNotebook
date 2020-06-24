using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StoryContentTextView : MonoBehaviour
{
    public event Action CompletedShowAnimation;

    [SerializeField] Canvas _textCanvas = null;
    [SerializeField] TextMeshProUGUI _textUI = null;

    private void Awake()
    {
        _textCanvas.gameObject.SetActive(false);
        Clear();
    }

    public void Display(StoryPage storyPage)
    {
        _textUI.text = storyPage.Text;
    }

    void Clear()
    {
        _textUI.text = string.Empty;
    }

    public void Show()
    {
        //TODO replace with Animations
        _textCanvas.gameObject.SetActive(true);
        CompletedShowAnimation?.Invoke();
    }

    public void Complete()
    {
        _textCanvas.gameObject.SetActive(true);
        CompletedShowAnimation?.Invoke();
        // complete the animation here
        // send out the event that it has completed
    }

    public void Hide()
    {
        //TODO replace with Animations
        _textCanvas.gameObject.SetActive(false);
    }
}
