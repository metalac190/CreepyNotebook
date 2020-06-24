using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StoryContentImageView : MonoBehaviour
{
    public event Action CompletedShowAnimation;

    [SerializeField] Canvas _imageCanvas = null;
    [SerializeField] Image _imageUI = null;
    [SerializeField] TextMeshProUGUI _imageTextUI = null;

    private void Awake()
    {
        _imageCanvas.gameObject.SetActive(false);
        Clear();
    }

    void Clear()
    {
        _imageUI.sprite = null;
        _imageTextUI.text = string.Empty;
    }

    public void Display(StoryPage storyImage)
    {
        _imageUI.sprite = null;
        _imageTextUI.text = string.Empty;
    }

    public void Show()
    {
        _imageCanvas.gameObject.SetActive(true);
        CompletedShowAnimation?.Invoke();
    }

    public void Complete()
    {
        _imageCanvas.gameObject.SetActive(true);
        CompletedShowAnimation?.Invoke();
    }

    public void Hide()
    {
        _imageCanvas.gameObject.SetActive(false);
    }
}
