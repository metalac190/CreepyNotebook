using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Choice
{
    // choice that gets assigned to the button
    [SerializeField] string _buttonText = null;
    public string ButtonText => _buttonText;

    [SerializeField] int _statRequirement = 0;
    public int StatRequirement => _statRequirement;

    [SerializeField] ChoiceOutcome _choiceOutcome = null;
    public ChoiceOutcome ChoiceOutcome => _choiceOutcome;


}
