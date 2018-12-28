using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CD_Location_###_Descriptor_CE###", menuName = "Story/ChoiceData")]
public class ChoiceData : ScriptableObject
{
    [SerializeField] StoryRequirement storyRequirement = new StoryRequirement();

    [Header("General")]
    [SerializeField] string choiceText = "...";
    public string ChoiceText { get { return choiceText; } }
    [SerializeField] float difficulty = 0;

    [Header("Success")]
    [SerializeField] StoryOutcome sucessOutcome;

    [Header("Failure")]
    [SerializeField] StoryOutcome failureOutcome;

    public bool IsEligible()
    {
        return true;
    }

    // for now, give it 50/50 chance
    public StoryOutcome RollOutcome()
    {
        int result = Random.RandomRange(0, 100);
        if(result <= 50)
        {
            return sucessOutcome;
        }
        else
        {
            return failureOutcome;
        }
    }
}
