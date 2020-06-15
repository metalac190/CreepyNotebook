using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDecisionEvent", menuName = "StoryEvent/Decision")]
public class StoryDecision : StoryEvent
{
    [Header("Story Decision Settings")]
    [SerializeField] TextBlock _decisionPrompt = null;
    public TextBlock DecisionPrompt => _decisionPrompt;

    [SerializeField] Choice[] _possibleChoices = new Choice[1];
    public Choice[] PossibleChoices => _possibleChoices;

    public StoryEvent ExitChoice { get; set; }

    /*
    public override StoryEvent Exit(PlayerStats stats, Inventory inventory)
    {
        return ExitChoice;
    }
    */
}
