using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChoice", menuName = "Story/Choice")]
public class Choice : ScriptableObject
{
    [SerializeField] string _choiceText = "...";
    public string ChoiceText => _choiceText;

    [SerializeField] Gate _gate = null;
    public Gate Gate => _gate;

    [SerializeField] StoryEvent _successOutcome = null;
    public StoryEvent SuccessOutcome => _successOutcome;

    [SerializeField] StoryEvent _failureOutcome = null;
    public StoryEvent FailureOutcome => _failureOutcome;
}
