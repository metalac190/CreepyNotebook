using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StoryDecisionView : MonoBehaviour
{
    public event Action ShowStarted;
    public event Action ShowCompleted;

    [Header("Decision Canvas")]
    [SerializeField] Canvas _decisionCanvas = null;
    [SerializeField] TextMeshProUGUI _textUI = null;
    [SerializeField] RectTransform _choicePanel = null;

    [Header("Choice Buttons")]
    [SerializeField] ChoiceButton _calmButton = null;
    [SerializeField] ChoiceButton _survivalButton = null;
    [SerializeField] ChoiceButton _tenacityButton = null;

    private void Awake()
    {
        _decisionCanvas.gameObject.SetActive(false);
        _choicePanel.gameObject.SetActive(false);
    }

    public void Show()
    {
        //TODO replace with Animation
        _decisionCanvas.gameObject.SetActive(true);
        _choicePanel.gameObject.SetActive(true);
        // animate buttons
        _calmButton.Show();
        _survivalButton.Show();
        _tenacityButton.Show();

        ShowStarted?.Invoke();
    }

    public void ShowImmediate()
    {
        //TODO replace with Animation
        _decisionCanvas.gameObject.SetActive(true);
        _choicePanel.gameObject.SetActive(true);
        // animate buttons
        _calmButton.Show();
        _survivalButton.Show();
        _tenacityButton.Show();
        Debug.Log("TODO: Complete Animation");
        ShowCompleted?.Invoke();
    }

    public void Display(StoryChoice storyDecision)
    {
        _textUI.text = storyDecision.DecisionPrompt;
        // choices
        _calmButton.Show();
        _survivalButton.Show();
        _tenacityButton.Show();
    }

    public void Clear()
    {
        _textUI.text = string.Empty;
        // clear buttons
        _calmButton.Clear();
        _survivalButton.Clear();
        _tenacityButton.Clear();
    }

    public void Hide()
    {
        //TODO replace with Animation
        _decisionCanvas.gameObject.SetActive(false);
        _choicePanel.gameObject.SetActive(false);
        // animate buttons
        _calmButton.Hide();
        _survivalButton.Hide();
        _tenacityButton.Hide();
    }
}
