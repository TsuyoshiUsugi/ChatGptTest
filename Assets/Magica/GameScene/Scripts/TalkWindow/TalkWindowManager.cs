using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class TalkWindowManager : MonoBehaviour
{
    [Header("QÆ")]
    [SerializeField] GameObject _talkWindowPanel;
    [SerializeField] Text _talkText;
    [SerializeField] float _showTextSec = 2;

    private void Start()
    {
        //Às‡‚ğ‚©‚ñ‚ª‚¦‚éB
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

        
    }

    public void CloseUI()
    {
        _talkWindowPanel.SetActive(false);
    }
}
