using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryContentTextView : MonoBehaviour
{
    [SerializeField] Canvas _textCanvas = null;
    [SerializeField] TextMeshProUGUI _textUI = null;

    private void Awake()
    {
        _textCanvas.gameObject.SetActive(false);
        Clear();
    }

    public void Display(StoryPage storyPage)
    {
        
    }

    void Clear()
    {
        _textUI.text = string.Empty;
    }

    public void Show()
    {
        //TODO replace with Animations
        _textCanvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        //TODO replace with Animations
        _textCanvas.gameObject.SetActive(false);
    }
}
