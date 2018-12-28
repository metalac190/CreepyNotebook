using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CE_Location_###_Description", menuName = "Story/ChoiceEvent")]
public class ChoiceEvent : Progression
{
    public enum ChoiceState { Prompt, Choice, Result }
    ChoiceState choiceState = ChoiceState.Prompt;

    [Header("Story Prompt")]
    [SerializeField] Story storyPrompt;
    [SerializeField] Story[] storyVariations;

    [Header("Choices")]
    [SerializeField] List<ChoiceData> tenacityChoices = new List<ChoiceData>();
    [SerializeField] List<ChoiceData> perceptionChoices = new List<ChoiceData>();
    [SerializeField] List<ChoiceData> survivalChoices = new List<ChoiceData>();

    public Story ActiveStory { get; private set; }
    public ChoiceData ActiveChoice { get; private set; }
    public StoryOutcome ActiveOutcome { get; private set; }

    List<ChoiceData> finalChoices = new List<ChoiceData>();

    #region Public functions

    public override void Initialize(GameController gameController)
    {
        // inject
        this.gameController = gameController;
        storyPrompt.Initialize();

        ValidatePrompt();
        ValidateChoices();
    }

    public override void Begin()
    {
        ActivatePrompt();
    }

    public override void Continue()
    {
        switch (choiceState)
        {
            case ChoiceState.Prompt:
                ProgressPrompt();
                break;
            case ChoiceState.Choice:
                // wait until choice button is pressed before changing state
                break;
            case ChoiceState.Result:
                ProgressResult();
                break;
            default:
                Debug.LogError("Not a valid choice state: " + name);
                break;
        }

    }

    public override void Finish()
    {
        Debug.Log("Choice Event finished");
        if (ActiveOutcome.ExitEvent == null)
        {
            Debug.LogWarning("NO EXIT TRANSITION ON Story EXIT" + name);
        }

        Debug.Log("Finish: " + name);
        gameController.StartNewEvent(ActiveOutcome.ExitEvent);
    }

    public void ActivatePrompt()
    {
        // BOUNCER //
        if (!IsStoryValid())
        {
            Debug.LogError("No story prompt assigned: " + name);
            ActivateChoice();
        }

        ChangeState(ChoiceState.Prompt);
        
        ChooseStoryPrompt();

        SendPageTextToController(ActiveStory.CurrentPage);
    }

    public void ActivateChoice()
    {
        ChangeState(ChoiceState.Choice);
        finalChoices = DetermineChoices();

        if (!IsChoiceListValid())
        {
            Debug.LogError("Insufficient choices assigned: " + name);
            Finish();
        }

        gameController.RevealChoices(finalChoices, this);
    }

    public void ActivateResult(ChoiceData choice)
    {
        ChangeState(ChoiceState.Result);
        // set a new story based on the outcome
        ActiveOutcome = choice.RollOutcome();
        ActiveOutcome.ChooseStory();
        ActiveOutcome.ChosenStory.Initialize();

        // pick a choice
        SendPageTextToController(ActiveOutcome.ChosenStory.CurrentPage);
    }
    #endregion

    #region Private functions

    private void ProgressPrompt()
    {
        if (!ActiveStory.IsLastPage())
        {
            ActiveStory.TurnPage();
            SendPageTextToController(ActiveStory.CurrentPage);
        }
        else
        {
            ActivateChoice();
        }
    }

    private void ProgressResult()
    {
        if (!ActiveOutcome.ChosenStory.IsLastPage())
        {
            ActiveOutcome.ChosenStory.TurnPage();
            SendPageTextToController(ActiveOutcome.ChosenStory.CurrentPage);
        }
        else
        {
            Finish();
        }
    }

    private void ChooseStoryPrompt()
    {
        if (storyVariations.Length == 0)
        {
            ActiveStory = storyPrompt;
        }
        else
        {
            ActiveStory = ChooseStoryVariation(storyPrompt);
            Debug.Log("No variations, assigning storyPrompt: " + storyPrompt.CurrentPage.StoryText);
            ActiveStory = storyPrompt;
        }
    }

    Story ChooseStoryVariation(Story defaultStory)
    {
        // assigning the default text at an index 1 past the length of the array
        int randomIndex = ChooseVariationIndex();
        int defaultTextIndex = storyVariations.Length + 1;
        // if we happened to hit the default index, return that instead of a variation
        if (randomIndex == defaultTextIndex)
        {
            return defaultStory;
        }
        else
        {
            try
            {
                // otherwise, return our variation
                return storyVariations[randomIndex];
            }
            catch
            {
                Debug.LogWarning("StoryVariation trying to return an empty index: " + name);
                return null;
            }
        }
    }

    int ChooseVariationIndex()
    {
        int randomIndex = UnityEngine.Random.Range(0, storyVariations.Length + 1);

        return randomIndex;
    }

    bool IsStoryValid()
    {
        if (storyPrompt == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool IsChoiceListValid()
    {
        foreach(ChoiceData choice in finalChoices)
        {
            if(choice == null)
            {
                return false;
            }
        }
        // we've made it through all final choices, validated
        return true;
    }

    List<ChoiceData> DetermineChoices()
    {
        List<ChoiceData> finalChoices = new List<ChoiceData>();
        // determine a choice from each out our choice categories (related to stat)
        finalChoices.Add(ChooseChoiceOption(tenacityChoices));
        finalChoices.Add(ChooseChoiceOption(perceptionChoices));
        finalChoices.Add(ChooseChoiceOption(survivalChoices));

        return finalChoices;
    }

    ChoiceData ChooseChoiceOption(List<ChoiceData> choiceOptions)
    {
        List<ChoiceData> eligibleChoices = new List<ChoiceData>();

        foreach (ChoiceData choice in choiceOptions)
        {
            if (choice.IsEligible())
            {
                eligibleChoices.Add(choice);
            }
        }

        //TODO make more robust. for now choose a random one from our eligible
        //List<int> chosenIndices = new List<int>();
        int randomIndex = UnityEngine.Random.Range(0, eligibleChoices.Count);

        return eligibleChoices[randomIndex];
    }

    void ChangeState(ChoiceState newState)
    {
        choiceState = newState;
    }

    void ValidatePrompt()
    {
        if (storyPrompt == null)
        {
            Debug.LogError("No story specified in Choice Event: " + name);
        }
    }

    void ValidateChoices()
    {
        // if there aren't any choices in any of the choice lists, it blocks progress
        if (tenacityChoices.Count == 0 && perceptionChoices.Count == 0 && survivalChoices.Count == 0)
        {
            Debug.LogError("No choices specified in Choice Event: " + name);
        }
    }
    #endregion
}
