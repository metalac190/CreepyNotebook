using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryText : StoryPage
{
    [TextArea(8, 12)]
    [SerializeField] string _text = "...";
    public string Text => _text;
}
