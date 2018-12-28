using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOG_Location_###_Description", menuName = "Story/StoryOutcomeGated")]
public class StoryOutcomeGated : StoryOutcome
{
    [Header("Requirements")]
    [SerializeField] StoryRequirement storyRequirement = new StoryRequirement();

    public override bool IsEligible()
    {
        //TODO in the future test gate requirements
        return base.IsEligible();
    }
}
