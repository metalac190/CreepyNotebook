using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Dev 
{
    [System.Serializable]
    public class Story
    {
        public event Action OnNewPage = delegate { };

        [SerializeField] StoryPage[] pages = null;

        public StoryPage CurrentPage { get; private set; }

        int currentPageIndex = 0;

        public void Initialize()
        {
            ValidateStory();
            currentPageIndex = 0;
            AssignNewPage();
        }

        void ValidateStory()
        {
            if (pages.Length <= 0)
            {
                Debug.LogError("No pages defined on story");
            }
        }

        public int GetPageCount()
        {
            return pages.Length;
        }

        public void TurnPage()
        {
            // BOUNCER //
            if (currentPageIndex < 0)
            {
                Debug.LogError("Invalid page index on Story");
            }

            // if there's more pages, go to the next
            if (!IsLastPage())
            {
                currentPageIndex++;
                AssignNewPage();
            }
            else
            {
                Debug.LogWarning("Trying to turn page, but we're out of pages. No page exists on Story");
            }
        }

        void AssignNewPage()
        {
            CurrentPage = pages[currentPageIndex];
            OnNewPage.Invoke();
        }

        public bool IsLastPage()
        {
            if (currentPageIndex >= 0 && currentPageIndex == GetPageCount() - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}


