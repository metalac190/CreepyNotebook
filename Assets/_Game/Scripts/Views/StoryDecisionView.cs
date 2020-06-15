using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDecisionView : MonoBehaviour, IProgressable
{
    public void ProgressDisplay(StoryDecision storyDecision)
    {
        Debug.Log("Display Story Decision");
    }

    public void Progress()
    {
        
    }
}
