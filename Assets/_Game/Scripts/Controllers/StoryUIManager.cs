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

    [SerializeField] StoryDecisionController _storyDecisionController = null;
    public StoryDecisionController StoryDecisionController => _storyDecisionController;
}
