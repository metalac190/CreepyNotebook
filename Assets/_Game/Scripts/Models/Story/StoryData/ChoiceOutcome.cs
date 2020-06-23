using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoiceOutcome
{
    [SerializeField] StoryPage _successText = null;
    public StoryPage SuccessText => _successText;

    [SerializeField] StoryExit _successExit = null;
    public StoryExit SuccessExit => _successExit;

    [SerializeField] StoryPage _failureText = null;
    public StoryPage FailureText => _failureText;

    [SerializeField] StoryExit _failureExit = null;
    public StoryExit FailureExit => _failureExit;

    public bool DetermineSuccess()
    {
        //TODO make this driven by stats
        int randomNumber = UnityEngine.Random.Range(0, 100);
        if (randomNumber < 50)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
