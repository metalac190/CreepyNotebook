using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Dev
{
    [RequireComponent(typeof(RevealText))]
    [RequireComponent(typeof(RevealChoices))]
    public class TextDisplay : MonoBehaviour
    {
        //[SerializeField] int maxCharactersOnScreen = 200;
        [SerializeField] TextMeshProUGUI textUI = null;

        public string CurrentText { get; private set; }

        RevealText revealText;
        RevealChoices revealChoices;
        GameController gameController;

        public void Initialize(GameController gameController)
        {
            // inject
            this.gameController = gameController;
            // get local components 
            FillReferences();
            //revealText.Initialize(textUI);
            revealChoices.Initialize(gameController);
            // local setup
            //textUI.maxVisibleCharacters = maxCharactersOnScreen;
        }

        private void OnEnable()
        {
            gameController.OnRevealStory += OnProgressStoryTextCallback;
        }

        private void OnDisable()
        {
            gameController.OnRevealStory -= OnProgressStoryTextCallback;
        }

        void FillReferences()
        {
            if (textUI == null)
            {
                Debug.LogWarning("TextDisplay missing textUI reference");
            }

            revealText = GetComponent<RevealText>();
            revealChoices = GetComponent<RevealChoices>();
        }

        void OnProgressStoryTextCallback(string newStoryText)
        {
            DisplayText(newStoryText, .02f);
        }

        public void DisplayText(string displayText, float revealSpeed)
        {
            //TODO store previous text in general

            // save our new text, for future reference in journal
            CurrentText = displayText;

            textUI.text = displayText;

            //revealText.AnimateText(revealSpeed);
        }
    }
}

