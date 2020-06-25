using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryUIManager : MonoBehaviour
{
    [SerializeField] HUDController _HUDController = null;
    public HUDController HUDController => _HUDController;

    [SerializeField] StoryPageController _storyPageController = null;
    public StoryPageController StoryPageController => _storyPageController;

    [SerializeField] StoryChoiceController _storyDecisionController = null;
    public StoryChoiceController StoryDecisionController => _storyDecisionController;
}
