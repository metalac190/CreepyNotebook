using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryPage
{
    [SerializeField] float _textSpeedModifier = 0;
    public float TextSpeedModifier => _textSpeedModifier;
}
