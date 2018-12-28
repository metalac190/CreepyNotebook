using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryRequirement
{
    [Header("Requirements")]
    [SerializeField] StatIdentifier requiredStat = StatIdentifier.None;
    [SerializeField] int requiredStatLevel = 0;
    [SerializeField] string requiredKey;

    public bool RequirementsMet()
    {
        //TODO placeholder
        return true;
    }
}
