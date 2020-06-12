using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0168 // variable declared but not used
#pragma warning disable 0219 // variable assign but not used
#pragma warning disable 0414 // private field assigned but no used

namespace Dev 
{
    [CreateAssetMenu(fileName = "SO_Location_###_Description", menuName = "StoryDEV/StoryOutcome")]
    public class StoryOutcome : ScriptableObject
    {
        [Header("General")]
        [SerializeField] Story defaultStory = null;
        public Story ChosenStory { get; private set; }
        public Progression ExitEvent { get { return exitEvent; } }
        [SerializeField] Progression exitEvent = null;

        [Header("Variations")]
        [SerializeField] Story[] storyVariations = null;

        GameController gameController;

        #region Public functions

        public virtual bool IsEligible()
        {
            //TODO test requirements in the future. Gates, etc.
            return true;
        }

        public Story ChooseStory()
        {
            ValidateDefaultStory();

            // if we don't have any story variations, just return the default text
            if (storyVariations.Length == 0)
            {
                ChosenStory = defaultStory;
            }
            else
            {
                // return variation
                ChosenStory = ChooseStoryVariation();
            }

            ChosenStory.Initialize();

            return ChosenStory;
        }

        #endregion

        void ValidateDefaultStory()
        {
            if (defaultStory == null)
            {
                //TODO find a way to log the instance of this script to console
                Debug.LogError("No default story specified on StoryOutcome");
            }
        }

        Story ChooseStoryVariation()
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
    }
}


