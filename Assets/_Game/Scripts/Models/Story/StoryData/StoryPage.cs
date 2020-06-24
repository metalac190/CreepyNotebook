using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryPage
{
    [SerializeField] PageType _pageType = PageType.Text;
    public PageType PageType => _pageType;

    [TextArea(8, 12)]
    [SerializeField] string _text = "...";
    public string Text => _text;

    [SerializeField] float _textSpeedModifier = 0;
    public float TextSpeedModifier => _textSpeedModifier;

    [SerializeField] Sprite _graphic = null;
    public Sprite Graphic => _graphic;
}
