using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChoiceOutcome
{
    public StoryPage[] ChosenStoryPages => _wasSuccessful ? _successText : _failureText;
    public StoryExit[] ChosenStoryExits => _wasSuccessful ? _successExits : _failureExit;

    [SerializeField] int _difficulty = 0;

    [Header("Success")]
    [SerializeField] StoryPage[] _successText = null;
    [SerializeField] StoryExit[] _successExits = null;

    [Header("Failure")]
    [SerializeField] StoryPage[] _failureText = null;
    [SerializeField] StoryExit[] _failureExit = null;

    bool _wasSuccessful = false;

    public bool DetermineSuccess()
    {
        //TODO make this driven by stats
        int randomNumber = UnityEngine.Random.Range(0, 100);
        if (randomNumber < 50)
        {
            _wasSuccessful = true;
            return true;
        }
        else
        {
            _wasSuccessful = false;
            return false;
        }
    }
}
