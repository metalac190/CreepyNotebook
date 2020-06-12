using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEnd : StoryEvent
{
    [SerializeField] TextBlock _textBlock = null;
    public TextBlock TextBlock => _textBlock;
}
