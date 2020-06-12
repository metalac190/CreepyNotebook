using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryImage", menuName = "Story/Image")]
public class StoryImage : StoryEvent
{
    [SerializeField] Sprite _image = null;
    public Sprite Image => _image;

    [SerializeField] TextBlock _imageText = null;
    public TextBlock ImageText => _imageText;
}
