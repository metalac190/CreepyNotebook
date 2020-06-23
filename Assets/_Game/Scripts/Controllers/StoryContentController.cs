using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(StoryContentTextView))]
[RequireComponent(typeof(StoryContentImageView))]
public class StoryContentController : StoryEventController
{
    public override event Action<StoryEventData> EventEnded;

    [SerializeField] string _promptText = "Continue";

    StoryContentTextView _storyTextView = null;
    StoryContentImageView _storyImageView = null;

    HUDController _HUD = null;
    PlayerStats _stats = null;
    Inventory _inventory = null;

    int _currentProgressionIndex = 0;
    public StoryEventData CurrentStoryEvent { get; private set; }
    public StoryPage CurrentStoryPage 
        => CurrentStoryEvent.StoryPages[_currentProgressionIndex];

    private void Awake()
    {
        _storyTextView = GetComponent<StoryContentTextView>();
        _storyImageView = GetComponent<StoryContentImageView>();
    }

    public void Init(HUDController HUD, PlayerStats stats, Inventory inventory)
    {
        _HUD = HUD;
        _stats = stats;
        _inventory = inventory;
    }

    public void Begin(StoryEventData storyEvent)
    {
        CurrentStoryEvent = storyEvent;
        _HUD.ShowPrompt(_promptText);
        // reset progress state
        _currentProgressionIndex = 0;
        // display first page
        DisplayPage(CurrentStoryPage);
    }

    public bool CanProgress()
    {
        // if we can progress our index by 1 and it's still valid, we can progress
        if (ArrayHelper.IsValidIndex(_currentProgressionIndex + 1, CurrentStoryEvent.StoryPages.Length))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Progress()
    {
        if (CanProgress())
        {
            _currentProgressionIndex++;
            DisplayPage(CurrentStoryPage);
        }
        else
        {
            End();
        }
    }

    void End()
    {
        // hide the visual elements
        _HUD.HidePrompt();
        HideDisplay();
        // determine exit event
        CurrentStoryEvent.StoryExit.GetExit(_stats, _inventory);
        EventEnded.Invoke(CurrentStoryEvent);
    }

    void DisplayPage(StoryPage page)
    {
        _storyTextView.Display(page);
        _storyTextView.Show();
    }

    void DisplayImage(StoryImage storyImage)
    {
        _storyImageView.Display(storyImage);
        _storyImageView.Show();
    }

    void HideDisplay()
    {
        _storyTextView.Hide();
    }
}
