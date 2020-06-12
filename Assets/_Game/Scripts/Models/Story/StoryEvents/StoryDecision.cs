using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDecisionEvent", menuName = "StoryEvents/Decision")]
public class StoryDecision : StoryEvent
{
    [SerializeField] TextBlock _decisionPrompt = null;
    public TextBlock DecisionPrompt => _decisionPrompt;

    [SerializeField] Choice[] _possibleChoices = new Choice[1];
    public Choice[] PossibleChoices => _possibleChoices;
}
