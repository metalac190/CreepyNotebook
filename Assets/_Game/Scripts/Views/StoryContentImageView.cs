using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryContentImageView : MonoBehaviour
{
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

    public void Display(StoryImage storyImage)
    {
        _imageUI.sprite = null;
        _imageTextUI.text = string.Empty;
    }

    public void Show()
    {
        _imageCanvas.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _imageCanvas.gameObject.SetActive(false);
    }
}
