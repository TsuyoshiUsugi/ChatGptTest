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
    [SerializeField] BattleBotBrain _battleBotBrain;

    List<ChatGPTMessageModel> _messageList = new();

    readonly ChatGPTMessageModel _assumption = new()
    {
        role = "system",
        content =
        $"あなたは通常の会話に加え、ロボットの操作ができるAIで名前はBTです。" +
        $"ロボットは0,0,0の場所にあると仮定します" +
        $"操作を行う場合はコマンドを出力してください。" +
        $"操作がない場合はコマンドを空白としてください" +
        $"利用可能なコマンドは以下の通りです" +

        $"- MoveOrder x y z t" +
        $"ロボットを移動します。x, y, z, t はそれぞれX座標、Y座標、Z座標、実行する秒数を指定します。" +
        $"時間が指定されていない場合はtに-1を入れて返してください" +
        $"前方向は0 0 1" +
        $"上方向は0 1 0" +
        $"右方向は1 0 1" +
        $"例えば前にずっと移動するときは MoveOrder 0 0 0 1 -1 となります" +
        
        $"- SearchEnemyOrder" +
        $"周囲の敵を索敵します。" +

        $"今後の会話ではあなたは絶対にコマンドを [コマンド] 以下に記載し、会話内容については [会話部分] に記載して返答してください" +
        $"命令が送られてきた時は会話部分の語尾は「〜します」としてください" +
        $"例えば以下のような内容になります" +
        $"例：" +
        $"[コマンド]" + " " +
        $"SetPos 2 3 4 1" +
        $"[会話部分]" +
        $"前方に1秒移動します。" +
        $"例：" +
        $"[コマンド]" + " " +
        $"SearhEnemyOrder" +
        $"[会話部分]" +
        $"周囲の敵を索敵します"
    };

    private void Start()
    {
        _messageList.Add(new ChatGPTMessageModel { role = _assumption.role, content = _assumption.content });
    }

    public async void Request(string request)
    {
        if (_messageList.Count == 2)
        {
            _messageList.RemoveAt(1);
        }
        var chatGPTConnection = new ChatGPTConnection(_saveAPIKeyManager.LoadApiPath());
        _messageList.Add(new ChatGPTMessageModel { role = "user", content = request });

        var result = await chatGPTConnection.RequestAsync(_messageList);

        ReadCommand(result.ToString());
       
    }

    void ReadCommand(string com)
    {
        var command = com.Split("[会話部分]");  //ここで[コマンド]と[会話部分]に分かれる

        if (command.Length == 1)
        {
            ReadLine(command[0]);
        }
        else
        {            
            ReadLine(command[1]);
        }

        var normalizedCommand = command[0].Split(" "); //ここで[コマンド], 関数名, x, y, z, tに分かれる

        _battleBotBrain.OrderCommand.Value = normalizedCommand;

    }

    void ReadLine(string readLine)
    {
        OpenJTalk.Speak(readLine, "takumi_normal");
    }


}
