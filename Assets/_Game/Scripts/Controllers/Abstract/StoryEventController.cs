using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StoryEventController : MonoBehaviour
{
    public abstract event Action<StoryEventData> EventEnded;

    public abstract void Progress();
}
