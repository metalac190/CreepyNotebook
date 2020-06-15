using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryTextView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textView = null;

    public void Display(TextBlock textBlock)
    {
        Debug.Log("Display Story Text");
        _textView.text = textBlock.StoryText;
    }
}
