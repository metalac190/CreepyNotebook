using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryPage
{
    [TextArea(8, 12)]
    [SerializeField] string storyText = "...";
    public string StoryText { get { return storyText; } }

    [SerializeField] float textSpeedModifier = 0;
    public float TextSpeedModifier { get { return textSpeedModifier; } }
}
