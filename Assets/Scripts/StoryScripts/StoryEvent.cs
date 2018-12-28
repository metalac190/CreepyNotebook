using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SE_Location_###_Description", menuName = "Story/StoryEvent")]
public class StoryEvent : Progression
{
    [SerializeField] List<StoryOutcome> possibleStoryOutcomes = new List<StoryOutcome>();

    public Story ActiveStory { get; private set; }
    public Progression ExitEvent { get; private set; }

    void Awake()
    {
        ValidateStoryOutcomes();
    }

    public override void Initialize(GameController gameController)
    {
        // inject
        this.gameController = gameController;
        // local state
        StoryOutcome storyOutcome = ChooseStoryOutcome(possibleStoryOutcomes);

        ActiveStory = storyOutcome.ChooseStory();
        ActiveStory.Initialize();

        ExitEvent = storyOutcome.ExitEvent;
    }

    public override void Begin()
    {
        // verify and continue
        if (!IsStoryValid())
        {
            Finish();
        }
        else
        {
            // reset the current story index, so that we can progress through each story
            SendPageTextToController(ActiveStory.CurrentPage);
        }
    }

    public override void Continue()
    {
        // if there are more pages, get the next one and send it
        if (!ActiveStory.IsLastPage())
        {
            ActiveStory.TurnPage();
            SendPageTextToController(ActiveStory.CurrentPage);
        }
        else
        {
            Finish();
        }
    }

    public override void Finish()
    {
        if(ExitEvent == null)
        {
            Debug.LogWarning("NO EXIT TRANSITION ON Story EXIT" + name);
        }
        Debug.Log("Finish: " + name);
        gameController.StartNewEvent(ExitEvent);
    }

    bool IsStoryValid()
    {
        if(ActiveStory == null)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    void ValidateStoryOutcomes()
    {
        if(possibleStoryOutcomes.Count == 0)
        {
            Debug.LogError("No Story outcomes in StoryEvent. Nowhere to go: " + GetInstanceID());
        }
        foreach (StoryOutcome storyOutcome in possibleStoryOutcomes)
        {
            if (storyOutcome == null)
            {
                Debug.LogError("Story event missing StoryData: " + GetInstanceID());
            }
        }
    }
}
