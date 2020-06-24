using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class StoryChoice
{
    public event Action<Choice> ChoicePicked;

    [SerializeField] string _decisionPrompt = null;
    public string DecisionPrompt => _decisionPrompt;

    [SerializeField] float _textSpeedModifier = 0;
    public float TextSpeedModifier => _textSpeedModifier;

    [SerializeField] Choice _calmChoice = null;
    public Choice CalmChoice => _calmChoice;

    [SerializeField] Choice _survivalChoice = null;
    public Choice SurvivalChoice => _survivalChoice;

    [SerializeField] Choice _tenacityChoice = null;
    public Choice TenacityChoice => _tenacityChoice;
}
