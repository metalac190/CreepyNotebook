using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StoryHelper
{
    public static bool IsStoryPagesValid(List<StoryPage> storyPages)
    {
        if (storyPages.Count == 0)
        {
            return false;
        }
        else if (storyPages == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
