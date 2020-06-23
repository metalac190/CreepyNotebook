using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryDecisionView : MonoBehaviour
{
    [Header("Decision Canvas")]
    [SerializeField] Canvas _decisionCanvas = null;
    [SerializeField] TextMeshProUGUI _textUI = null;
    [SerializeField] RectTransform _choicePanel = null;

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
    }

    public void Display(StoryText storyText, StoryDecision storyDecision)
    {
        _textUI.text = storyText.Text;
        // TODO add choices here
    }

    public void Clear()
    {
        _textUI.text = string.Empty;
    }

    public void Hide()
    {
        //TODO replace with Animation
        _decisionCanvas.gameObject.SetActive(false);
        _choicePanel.gameObject.SetActive(false);
    }
}
