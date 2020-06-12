using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    public bool IsRevealing { get; private set; } = false;

    public void ChooseStartingEvent()
    {

    }

    public void ChooseNextEvent()
    {

    }

    public void Progress()
    {
        Debug.Log("...Progress story...");
        IsRevealing = true;
        // send to UI
    }

    public void CompleteReveal()
    {
        Debug.Log("Finish Reveal");
    }
}
