using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTextView : MonoBehaviour
{
    StoryText _storyText = null;
    int _currentTextBlockIndex = 0;

    public bool CanProgress()
    {
        int targetIndex = _currentTextBlockIndex + 1;
        if(_storyText.TextBlock[targetIndex] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Display(StoryText storyText)
    {
        Debug.Log("Display Story Text");
    }

    public void Progress()
    {
        throw new System.NotImplementedException();
    }
}
