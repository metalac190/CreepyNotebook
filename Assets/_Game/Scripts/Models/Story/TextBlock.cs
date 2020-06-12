using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextBlock
{
    [TextArea(8, 12)]
    [SerializeField] string _storyText = "...";
    public string StoryText => _storyText;

    [SerializeField] float _textSpeedModifier = 0;
    public float TextSpeedModifier => _textSpeedModifier;
}
