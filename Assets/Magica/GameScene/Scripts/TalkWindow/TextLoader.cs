using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextLoader : MonoBehaviour
{
    [Header("éQè∆")]
    [SerializeField] TalkWindowManager _talkWindowManager;
    [SerializeField] List<TextAsset> _textAssetList;

    public void ShowText(int index)
    {
        _talkWindowManager.DoTalkWindowTask(_textAssetList[index].Text);
    }
}
