using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(StoryContentTextView))]
[RequireComponent(typeof(StoryContentImageView))]
public class StoryPageController : MonoBehaviour
{
    public event Action CompletedShowAnimation;
    public event Action OutOfPages;

    [Header("Prompts")]
    [SerializeField] string _continuePromptText = "Continue...";
    public string ContinuePromptText => _continuePromptText;

    StoryContentTextView _storyTextView = null;
    StoryContentImageView _storyImageView = null;

    public StoryPage CurrentStoryPage => _storyPages[_pageProgressionIndex];

    int _pageProgressionIndex = 0;
    StoryPage[] _storyPages;
    bool _finishedShowAnimation = false;

    #region MonoBehaviour
    private void Awake()
    {
        _storyTextView = GetComponent<StoryContentTextView>();
        _storyImageView = GetComponent<StoryContentImageView>();
    }

    private void OnEnable()
    {
        _storyTextView.CompletedShowAnimation += OnCompletedShowAnimation;
        _storyImageView.CompletedShowAnimation += OnCompletedShowAnimation;
    }

    private void OnDisable()
    {
        
    }
    #endregion

    #region Public
    public void Begin(StoryPage[] storyPages)
    {
        _storyPages = storyPages;

        // reset progress state
        _pageProgressionIndex = 0;
        _finishedShowAnimation = false;
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
            HideStoryPage();
        }
        else if (CurrentStoryPage.PageType == PageType.Image)
        {
            HideStoryImage();
        }
    }

    public void Progress()
    {
        // if we're not finished animating, immediately reveal
        if (_finishedShowAnimation == false)
        {
            CompleteRevealContent();
        }
        // if we have more pages and are finished Animating, continue
        else if (IsMorePages())
        {
            _pageProgressionIndex++;
            RevealContent();
        }
        // if we don't have any more pages, send the 'out of content' event
        else if (IsMorePages() == false)
        {
            OutOfPages?.Invoke();
        }
    }
    #endregion

    #region Private
    void RevealStoryText(StoryPage page)
    {
        _storyTextView.Display(page);
        _storyTextView.Show();
    }

    void CompleteRevealStoryText(StoryPage page)
    {
        _storyTextView.Display(page);
        _storyTextView.Complete();
    }

    void HideStoryPage()
    {
        _storyTextView.Hide();
    }

    void RevealStoryImage(StoryPage storyImage)
    {
        _storyImageView.Display(storyImage);
        _storyImageView.Show();
    }

    void CompleteRevealStoryImage(StoryPage page)
    {
        _storyImageView.Display(page);
        _storyTextView.Complete();
    }

    void HideStoryImage()
    {
        _storyImageView.Hide();
    }

    bool IsMorePages()
    {
        // if we can progress our index by 1 and it's still valid, we can progress
        if (ArrayHelper.IsValidIndex(_pageProgressionIndex + 1, _storyPages.Length))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnCompletedShowAnimation()
    {
        _finishedShowAnimation = true;
        CompletedShowAnimation?.Invoke();
    }
    #endregion
}
