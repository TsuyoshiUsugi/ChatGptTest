using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AAA.OpenAI;
using UniRx;

public class GPTCall : MonoBehaviour
{
    [SerializeField] string _openAIApiKey = "";
    [SerializeField] string _requestCommand = "こんにちは";

    private readonly ReactiveProperty<bool> _request = new BoolReactiveProperty();

    private void Start()
    {
        _request.Where(x => x == true).Subscribe(_ => Request()).AddTo(this);
    }

    private void Update()
    {
        _request.Value = Input.GetKeyDown(KeyCode.R);
    }

    void Request()
    {
        Debug.Log("リクエスト");
        var chatGPTConnection = new ChatGPTConnection(_openAIApiKey);
        chatGPTConnection.RequestAsync(_requestCommand);
    }


}
