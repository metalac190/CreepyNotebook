using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(StoryContentTextView))]
[RequireComponent(typeof(StoryContentImageView))]
public class StoryPageController : MonoBehaviour
{
    public event Action OutOfPages;

    [Header("Prompts")]
    [SerializeField] string _continuePromptText = "Continue...";
    public string ContinuePromptText => _continuePromptText;

    StoryContentTextView _storyTextView = null;
    StoryContentImageView _storyImageView = null;

    public StoryPage CurrentStoryPage => _storyPages[_pageProgressionIndex];

    int _pageProgressionIndex = 0;
    List<StoryPage> _storyPages = new List<StoryPage>();
    bool _isRevealingText = false;

    #region MonoBehaviour
    private void Awake()
    {
        _storyTextView = GetComponent<StoryContentTextView>();
        _storyImageView = GetComponent<StoryContentImageView>();
    }

    private void OnEnable()
    {
        _storyTextView.RevealAnimationStarted += OnStartedShowAnimation;
        _storyTextView.RevealAnimationCompleted += OnCompletedShowAnimation;
        _storyImageView.RevealAnimationStarted += OnStartedShowAnimation;
        _storyImageView.RevealAnimationCompleted += OnCompletedShowAnimation;
    }

    private void OnDisable()
    {
        _storyTextView.RevealAnimationStarted -= OnStartedShowAnimation;
        _storyTextView.RevealAnimationCompleted -= OnCompletedShowAnimation;
        _storyImageView.RevealAnimationStarted -= OnStartedShowAnimation;
        _storyImageView.RevealAnimationCompleted -= OnCompletedShowAnimation;
    }
    #endregion

    #region Public
    public void Begin(List<StoryPage> storyPages)
    {
        _storyPages = storyPages;

        // reset progress state
        _pageProgressionIndex = 0;
        _isRevealingText = false;
        // display first page
        RevealContent();
    }

    public void RevealContent()
    {
        // determine display type
        if (CurrentStoryPage.PageType == PageType.Text)
        {
            RevealStoryText(CurrentStoryPage);
        }
        else if (CurrentStoryPage.PageType == PageType.Image)
        {
            RevealStoryImage(CurrentStoryPage);
        }
    }

    public void CompleteRevealContent()
    {
        // determine display type
        if (CurrentStoryPage.PageType == PageType.Text)
        {
            CompleteRevealStoryText(CurrentStoryPage);
        }
        else if (CurrentStoryPage.PageType == PageType.Image)
        {
            CompleteRevealStoryImage(CurrentStoryPage);
        }
    }

    public void HideContent()
    {
        // determine which display to hide
        if (CurrentStoryPage.PageType == PageType.Text)
        {
            HideStoryText();
        }
        else if (CurrentStoryPage.PageType == PageType.Image)
        {
            HideStoryImage();
        }
    }

    public void Progress()
    {
        // if we're not finished animating, immediately reveal
        if (_isRevealingText == true)
        {
            //Debug.Log("Complete Content Reveal");
            CompleteRevealContent();
        }
        // if we have more pages and are finished Animating, continue
        else if (IsMorePages() && !_isRevealingText)
        {
            //Debug.Log("Progress Next Page");
            HideContent();
            _pageProgressionIndex++;
            RevealContent();
        }
        // if we don't have any more pages, send the 'out of content' event
        else if (IsMorePages() == false && !_isRevealingText)
        {
            //Debug.Log("Progress Next Story");
            OutOfPages?.Invoke();
        }
    }
    #endregion

    #region Private
    void RevealStoryText(StoryPage page)
    {
        _storyTextView.Display(page);
        _storyTextView.Reveal();
    }

    void CompleteRevealStoryText(StoryPage page)
    {
        //_storyTextView.Display(page);
        _storyTextView.CompleteReveal();
    }

    void HideStoryText()
    {
        _storyTextView.Hide();
    }

    void RevealStoryImage(StoryPage storyImage)
    {
        _storyImageView.Display(storyImage);
        _storyImageView.Reveal();
    }

    void CompleteRevealStoryImage(StoryPage page)
    {
        _storyImageView.Display(page);
        _storyImageView.CompleteReveal();
    }

    void HideStoryImage()
    {
        _storyImageView.Hide();
    }

    bool IsMorePages()
    {
        // if we can progress our index by 1 and it's still valid, we can progress
        if (ArrayHelper.IsValidIndex(_pageProgressionIndex + 1, _storyPages.Count))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnStartedShowAnimation()
    {
        _isRevealingText = true;
    }

    void OnCompletedShowAnimation()
    {
        _isRevealingText = false;
    }
    #endregion
}
