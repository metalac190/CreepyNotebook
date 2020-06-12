using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dev 
{
    public abstract class Progression : ScriptableObject
    {
        protected GameController gameController;

        public abstract void Initialize(GameController gameController);
        public abstract void Begin();
        public abstract void Continue();
        public abstract void Finish();

        public void SendPageTextToController(StoryPage storyPage)
        {
            string storyText = storyPage.StoryText;
            gameController.RevealStory(storyText);
        }

        public StoryOutcome ChooseStoryOutcome(List<StoryOutcome> storyOutcomes)
        {
            List<StoryOutcome> eligibleOutcomes = new List<StoryOutcome>();

            foreach (StoryOutcome outcome in storyOutcomes)
            {
                if (outcome.IsEligible())
                {
                    eligibleOutcomes.Add(outcome);
                }
            }

            //TODO make more robust. for now choose a random one from our eligible
            int randomIndex = UnityEngine.Random.Range(0, eligibleOutcomes.Count);
            return eligibleOutcomes[randomIndex];
        }

        public StoryOutcome ChooseStoryOutcome(List<StoryOutcomeGated> storyOutcomes)
        {
            List<StoryOutcome> eligibleOutcomes = new List<StoryOutcome>();

            foreach (StoryOutcomeGated outcome in storyOutcomes)
            {
                if (outcome.IsEligible())
                {
                    eligibleOutcomes.Add(outcome);
                }
            }

            //TODO make more robust. for now choose a random one from our eligible
            int randomIndex = UnityEngine.Random.Range(0, eligibleOutcomes.Count);
            return eligibleOutcomes[randomIndex];
        }
    }
}

