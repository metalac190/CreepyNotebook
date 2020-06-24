using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class StoryEventController : MonoBehaviour
{
    public abstract event Action CompletedShowAnimation;
    public abstract event Action OutOfContent;
    public abstract void Progress();
}
