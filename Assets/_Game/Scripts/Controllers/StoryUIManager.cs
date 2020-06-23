using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryUIManager : MonoBehaviour
{
    [SerializeField] StoryContentController _storyTextController = null;
    public StoryContentController StoryTextController => _storyTextController;

    [SerializeField] StoryDecisionController _storyDecisionController = null;
    public StoryDecisionController StoryDecisionController => _storyDecisionController;

    [SerializeField] HUDController _HUDController = null;
    public HUDController HUDController => _HUDController;
}
