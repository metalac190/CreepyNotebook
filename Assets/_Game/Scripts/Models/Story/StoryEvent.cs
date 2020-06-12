using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEvent : ScriptableObject
{
    [SerializeField] string _name = "...";
    public string Name => _name;
}
