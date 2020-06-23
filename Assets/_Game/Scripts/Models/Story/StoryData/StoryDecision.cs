using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryDecision
{
    [Header("Story Decision Settings")]
    [SerializeField] string _decisionPrompt = null;
    public string DecisionPrompt => _decisionPrompt;

    [SerializeField] float _textSpeedModifier = 0;
    public float TextSpeedModifier => _textSpeedModifier;

    [SerializeField] Choice _calmChoice = null;
    public Choice CalmChoice => _calmChoice;

    [SerializeField] Choice _tenacityChoice = null;
    public Choice TenacityChoice => _tenacityChoice;

    [SerializeField] Choice _survivalChoice = null;
    public Choice SurvivalChoice => _survivalChoice;
}
