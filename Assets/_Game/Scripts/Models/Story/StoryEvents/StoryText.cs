using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryText", menuName = "StoryEvents/Text")]
public class StoryText : StoryEvent
{
    [SerializeField] TextBlock[] _textBlock = null;
    public TextBlock[] TextBlock => _textBlock;
}
