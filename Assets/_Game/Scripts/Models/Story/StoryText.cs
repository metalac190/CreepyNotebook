using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryText : StoryEvent
{
    [TextArea]
    [SerializeField] TextBlock[] _textBlock = null;
    public TextBlock[] TextBlock => _textBlock;
}
