using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class TalkWindowManager : MonoBehaviour
{
    [Header("éQè∆")]
    [SerializeField] GameObject _talkWindowPanel;
    [SerializeField] Text _talkText;
    [SerializeField] float _showTextSec = 2;

    private void Start()
    {
        //_talkWindowPanel.SetActive(false);
    }

    public void DoTalkWindowTask(List<string> sentences)
    {
        DoTask(sentences);
    }

    async void DoTask(List<string> sentence)
    {
        _talkWindowPanel.SetActive(true);

        for (int i = 0; i < sentence.Count; i++)
        {
            _talkText.text = sentence[i];
            await UniTask.Delay(System.TimeSpan.FromSeconds(_showTextSec));
        }

        _talkWindowPanel.SetActive(false);
    }
}
