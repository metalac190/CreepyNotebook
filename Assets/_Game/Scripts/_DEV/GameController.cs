using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Dev
{
    public class GameController : MonoBehaviour
    {
        [Header("Game Start")]
        [SerializeField] List<Progression> startingEvents = new List<Progression>();

        public event Action<string> OnRevealStory = delegate { };
        public event Action<List<ChoiceData>> OnRevealChoices = delegate { };

        public Progression CurrentEvent { get; private set; }
        public ChoiceEvent CurrentChoiceEvent { get; private set; }

        [Header("Game References")]
        [SerializeField] TextDisplay textDisplay = null;

        [SerializeField] InputController inputHandler;

        #region Setup
        void Awake()
        {
            Debug.Log("GameController entry");

            // fill local references
            FillReferences();
            // pass it on
            textDisplay.Initialize(this);
        }

        void FillReferences()
        {
            if (textDisplay == null)
            {
                Debug.LogError("GameController missing textDisplay reference");
                textDisplay = FindObjectOfType<TextDisplay>();
            }
            inputHandler = GetComponent<InputController>();
        }

        private void OnEnable()
        {
            inputHandler.OnClicked += OnClickReceivedCallback;
        }

        private void OnDisable()
        {
            inputHandler.OnClicked -= OnClickReceivedCallback;
        }

        void Start()
        {
            ChooseRandomStartingEvent();
        }

        void ChooseRandomStartingEvent()
        {
            int randomIndex = UnityEngine.Random.Range(0, startingEvents.Count);
            StartNewEvent(startingEvents[randomIndex]);
        }
        #endregion

        public void StartNewEvent(Progression newEvent)
        {
            Debug.Log("Begin: " + newEvent.name);

            CurrentEvent = newEvent;
            newEvent.Initialize(this);
            newEvent.Begin();
        }

        public void RevealStory(string storyText)
        {
            OnRevealStory.Invoke(storyText);
        }

        public void RevealChoices(List<ChoiceData> choices, ChoiceEvent choiceEvent)
        {
            CurrentChoiceEvent = choiceEvent;
            // TODO reveal Choice Blocks
            OnRevealChoices.Invoke(choices);
            // we don't want to accept clicks that aren't button presses during Choice State

        }

        public void ApplyChoice(ChoiceData choice)
        {
            CurrentChoiceEvent.ActivateResult(choice);
        }

        void OnClickReceivedCallback()
        {
            CurrentEvent.Continue();
        }
    }
}

