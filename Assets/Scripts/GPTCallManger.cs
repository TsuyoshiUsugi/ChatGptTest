using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AAA.OpenAI;
using UniRx;

public class GPTCallManger : MonoBehaviour
{
    [SerializeField] string _openAIApiKey = "";
    [SerializeField] string _requestCommand = "こんにちは";

    readonly ChatGPTMessageModel _assumption = new ChatGPTMessageModel()
    {
        role = "system",
        content =
                $"あなたは通常の会話に加え、オブジェクトの操作ができます。" +
                $"オブジェクトがあると仮定してください" +
                $"操作を行う場合はコマンドを出力してください。" +
                $"操作がない場合はコマンドを空白としてください" +
                $"利用可能なコマンドは以下の通りです" +
                $"- SetPos x y z" +
                $"オブジェクトの位置を変更します。x, y, z はそれぞれX座標、Y座標、Z座標を指定します。" +
                $"今後の会話ではあなたは常にコマンドを [コマンド] 以下に記載し、会話内容については [会話部分] に記載して返答してください" +
                $"例えば以下のような内容になります" +
                $"[コマンド]" + " " +
                $"SetPos 2 3 4" +
                $"[会話部分]" +
                $"了解しました。それでは始めましょう。"
    };

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
        chatGPTConnection.MessageList.Add(_assumption);
    
        chatGPTConnection.RequestAsync(_requestCommand);
    }


}
