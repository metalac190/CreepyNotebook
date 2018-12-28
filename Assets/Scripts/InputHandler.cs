using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputHandler : MonoBehaviour
{
    public bool IsContinueClickActive { get; private set; }

    public event Action OnClickReceived = delegate { };

    GameController gameController;

    public void Initialize(GameController gameController)
    {
        this.gameController = gameController;
    }

    private void OnEnable()
    {
        gameController.OnRevealStory += OnRevealStoryCallback;
        gameController.OnRevealChoices += OnRevealChoicesCallback;
    }

    private void OnDisable()
    {
        gameController.OnRevealStory -= OnRevealStoryCallback;
        gameController.OnRevealChoices -= OnRevealChoicesCallback;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsContinueClickActive)
        {
            OnClickReceived.Invoke();
        }
    }

    public void SetContinueClickActive(bool isContinueClickActive)
    {
        IsContinueClickActive = isContinueClickActive;
    }

    void OnRevealChoicesCallback(List<ChoiceData> choices)
    {
        SetContinueClickActive(false);
    }

    void OnRevealStoryCallback(string newStory)
    {
        SetContinueClickActive(true);
    }
}
