using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Dev
{
    public class RevealChoices : MonoBehaviour
    {
        [SerializeField] List<ChoiceButton> choiceButtons = new List<ChoiceButton>();

        [Header("Animation Settings")]
        [SerializeField] float revealSpeed = .5f;
        [SerializeField] float hideSpeed = .5f;
        [SerializeField] float showDelayIncrement = .2f;
        [SerializeField] float hideDelayIncrement = .2f;

        GameController gameController;

        public void Initialize(GameController gameController)
        {
            // inject
            this.gameController = gameController;
            // pass it on
            InitializeChoiceButtons();

            Validate();
        }

        private void OnEnable()
        {
            gameController.OnRevealChoices += OnRevealChoicesCallback;
        }

        private void OnDisable()
        {
            gameController.OnRevealChoices -= OnRevealChoicesCallback;
        }

        void InitializeChoiceButtons()
        {
            for (int i = 0; i < choiceButtons.Count; i++)
            {
                //choiceButtons[i].Initialize(gameController, this);
            }
        }

        private void Validate()
        {
            //TODO validate choice buttons here
        }

        void OnRevealChoicesCallback(List<ChoiceData> choices)
        {
            List<ChoiceData> activeChoices = choices;

            ShuffleChoices(activeChoices);
            PrepareButtons(activeChoices, choiceButtons);
            DisplayChoices(choiceButtons);
        }

        void PrepareButtons(List<ChoiceData> choices, List<ChoiceButton> choiceButtons)
        {
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i] == null)
                {
                    //choiceButtons[i].SetChoiceActive(false);
                    continue;
                }

                // add our new "On Press" call to send it back to gameController when pressed
                //choiceButtons[i].PrepareNewChoice(gameController, choices[i]);
            }
        }

        public void DisplayChoices(List<ChoiceButton> choiceButtons)
        {
            // slightly increase start delay for each iteration to create animation overlap
            float startDelay = 0;
            // show each choice, with the increment start delay
            for (int i = 0; i < choiceButtons.Count; i++)
            {
                if (choiceButtons[i].IsChoiceActive)
                {
                    //choiceButtons[i].Show(startDelay, revealSpeed);
                    startDelay += showDelayIncrement;
                }
            }
        }

        public void DisableChoices()
        {
            // slightly increase start delay for each iteration to create animation overlap
            float startDelay = 0;
            // hide each choice, with the increment start delay
            for (int i = 0; i < choiceButtons.Count; i++)
            {
                if (choiceButtons[i].IsChoiceActive)
                {
                    //choiceButtons[i].Hide(startDelay, hideSpeed);
                    startDelay += hideDelayIncrement;
                }
                // disable all choices in preparation of a future choice
                //choiceButtons[i].SetChoiceActive(false);
            }
        }

        public void ShuffleChoices(List<ChoiceData> choices)
        {
            for (int i = 0; i < choices.Count; i++)
            {
                ChoiceData temp = choices[i];
                int randomIndex = UnityEngine.Random.Range(i, choices.Count);
                choices[i] = choices[randomIndex];
                choices[randomIndex] = temp;
            }

            //Debugging
            for (int i = 0; i < choiceButtons.Count; i++)
            {
                Debug.Log("choice button index: " + choiceButtons[i].name);
            }
        }
    }
}

