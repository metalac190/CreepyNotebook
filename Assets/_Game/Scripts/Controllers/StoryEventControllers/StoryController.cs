using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoryController : MonoBehaviour
{
    //public event Action StoryContentBegin;
    //public event Action DecisionBegin;
    //public event Action ChoiceStoryBegin;

    [Header("Story")]
    [SerializeField] StoryEventData _startingStory = null;

    public StoryEventData CurrentStoryEvent { get; private set; }
    public StoryEventData NextStoryEvent { get; private set; }

    public StoryEventController _activeEventController { get; private set; }

    /*
    public void Init(PlayerStats stats, Inventory inventory)
    {
        _stats = stats;
        _inventory = inventory;
        // set the starting story
        NextStoryEvent = _startingStory;
        //_hudController.ShowPrompt(_continuePromptText);
    }

    public void Begin()
    {
        // get our story
        CurrentStoryEvent = NextStoryEvent;
        NextStoryEvent = null;  // clear for testing
        // if there's pages, display them
        if(CurrentStoryEvent.StoryPages.Length > 0)
        {
            _pageController.Begin(CurrentStoryEvent.StoryPages);
        }
        // if not, handle a decision event
        else if(CurrentStoryEvent.ExitType == ExitType.Decision)
        {
            _decisionController.Begin(CurrentStoryEvent);
        }
        else if(CurrentStoryEvent.ExitType == ExitType.Story)
        {
            CurrentStoryEvent.StoryExit.GetExit(_stats, _inventory);
        }
    }

    public void Progress()
    {
        _activeEventController.Progress();



        if (IsDecision() && !_decisionActivated)
        {
            _decisionActivated = true;
            ProgressStoryDecision(CurrentStoryEvent.StoryDecision);
        }
        else if (IsDecision() && _decisionActivated)
        {
            HideStoryDecision();
            End();
        }
        else
        {
            End();
        }
    }

    public override void End()
    {
        // hide the visual elements
        _HUD.HidePrompt();
        HideContent();
        // determine exit event
        NextStoryEvent = CurrentStoryEvent.StoryExit.GetExit(_stats, _inventory);
        DisplayCompleted.Invoke();
    }

    bool IsDecision()
    {
        // check to make sure a decision is assigned
        if (CurrentStoryEvent.ExitType == ExitType.Decision)
        {
            // make sure that we actually assigned something
            if (CurrentStoryEvent.StoryDecision == null)
            {
                Debug.LogWarning("No Story Decision assigned");
                return false;
            }
            // we've assigned it! success!
            else
            {
                return true;
            }
        }
        // exit type not assigned as decision
        else
        {
            return false;
        }
    }
    */
}
