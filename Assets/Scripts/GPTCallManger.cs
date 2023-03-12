using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using AAA.OpenAI;
using Cysharp.Threading.Tasks;

public class GPTCallManger : MonoBehaviour
{
    [SerializeField] SaveAPIKeyManager _saveAPIKeyManager;
    [SerializeField] string _requestCommand = "こんにちは";

    List<ChatGPTMessageModel> _messageList = new();

    readonly ChatGPTMessageModel _assumption = new()
    {
        role = "system",
        content =
                $"あなたは通常の会話に加え、オブジェクトの操作ができます。" +
                $"オブジェクトは0,0,0の場所にあると仮定します" +
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

    private void Start()
    {
        _messageList.Add(new ChatGPTMessageModel { role = _assumption.role, content = _assumption.content });
    }

    public async void Request(string request)
    {
        Debug.Log(_messageList.Count);
        if (_messageList.Count == 2)
        {
            _messageList.RemoveAt(1);
        }
        Debug.Log(request);
        var chatGPTConnection = new ChatGPTConnection(_saveAPIKeyManager.LoadApiPath());
        _messageList.Add(new ChatGPTMessageModel { role = "user", content = request });

        var result = await chatGPTConnection.RequestAsync(_messageList);
        ReadCommand(result.ToString());
    }

    void ReadCommand(string com)
    {
        var command = com.Split("[会話部分]");  //ここで[コマンド]と[会話部分に分かれる]

        if (command[0].StartsWith("[コマンド] SetPos")) //SetPosコマンドなら
        {
            var normalizedCommand = command[0].Split(" "); //ここで[コマンド], SetPos, x, y, zに分かれる

            float x = float.Parse(normalizedCommand[2]);
            float y = float.Parse(normalizedCommand[3]);
            float z = float.Parse(normalizedCommand[4]);

            GameObject.FindObjectOfType<RequestObj>().DoMoveRequest(new(x, y, z));

        }
    }


}
