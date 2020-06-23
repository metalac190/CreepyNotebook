using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryImage
{
    [SerializeField] Sprite _graphic = null;
    public Sprite Graphic => _graphic;

    [TextArea(8, 12)]
    [SerializeField] string _storyText = "...";
    public string StoryText => _storyText;
}
