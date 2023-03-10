using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace AAA.OpenAI
{
    public class ChatGPTConnection
    {
        private readonly string _apiKey;
        //会話履歴を保持するリスト
        List<ChatGPTMessageModel> _messageList = new();
        public List<ChatGPTMessageModel> MessageList { get => _messageList; set => _messageList = value; }

        public ChatGPTConnection(string apiKey)
        {
            _apiKey = apiKey;
            //_messageList.Add(new ChatGPTMessageModel() { role = "system", content = "語尾に「にゃ」をつけて" });
            _messageList.Add(
                new ChatGPTMessageModel() 
                { 
                    role = "system", content =
                    $"あなたは通常の会話に加え、オブジェクトの操作ができます。" +
                    $"オブジェクトがあると仮定してください" +
                    $"操作を行う場合はコマンドを出力してください。" +
                    $"操作がない場合はコマンドを空白としてください" +
                    $"利用可能なコマンドは以下の通りです" +
                    $"- SetPos x y z" +
                    $"オブジェクトの位置を変更します。x, y, z はそれぞれX座標、Y座標、Z座標を指定します。" +
                    $"今後の会話ではあなたは常にコマンドを [コマンド] 以下に記載し、会話内容については [会話部分] に記載して返答してください" +
                    $"例えば以下のような内容になります" +
                    $"[コマンド]" + " "+
                    $"SetPos 2 3 4" +
                    $"[会話部分]" +
                    $"了解しました。それでは始めましょう。" 
                });
        }

        public async UniTask<ChatGPTResponseModel> RequestAsync(string userMessage)
        {
            //文章生成AIのAPIのエンドポイントを設定
            var apiUrl = "https://api.openai.com/v1/chat/completions";

            _messageList.Add(new ChatGPTMessageModel { role = "user", content = userMessage });

            //OpenAIのAPIリクエストに必要なヘッダー情報を設定
            var headers = new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + _apiKey},
                {"Content-type", "application/json"},
                {"X-Slack-No-Retry", "1"}
            };

            //文章生成で利用するモデルやトークン上限、プロンプトをオプションに設定
            var options = new ChatGPTCompletionRequestModel()
            {
                model = "gpt-3.5-turbo",
                messages = _messageList
            };
            var jsonOptions = JsonUtility.ToJson(options);

            Debug.Log("自分:" + userMessage);

            //OpenAIの文章生成(Completion)にAPIリクエストを送り、結果を変数に格納
            using var request = new UnityWebRequest(apiUrl, "POST")
            {
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOptions)),
                downloadHandler = new DownloadHandlerBuffer()
            };

            foreach (var header in headers)
            {
                request.SetRequestHeader(header.Key, header.Value);
            }

            await request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                throw new Exception();
            }
            else
            {
                var responseString = request.downloadHandler.text;
                var responseObject = JsonUtility.FromJson<ChatGPTResponseModel>(responseString);
                Debug.Log("ChatGPT:" + responseObject.choices[0].message.content);
                ReadCommand(responseObject.choices[0].message.content);
                _messageList.Add(responseObject.choices[0].message);
                return responseObject;
            }
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

                GameObject.FindObjectOfType<MoveOrderManager>().DoMoveRequest(new(x, y, z));
                
            }
        }
    }

    [Serializable]
    public class ChatGPTMessageModel
    {
        public string role;
        public string content;
    }

    //ChatGPT APIにRequestを送るためのJSON用クラス
    [Serializable]
    public class ChatGPTCompletionRequestModel
    {
        public string model;
        public List<ChatGPTMessageModel> messages;
    }

    //ChatGPT APIからのResponseを受け取るためのクラス
    [System.Serializable]
    public class ChatGPTResponseModel
    {
        public string id;
        public string @object;
        public int created;
        public Choice[] choices;
        public Usage usage;

        [System.Serializable]
        public class Choice
        {
            public int index;
            public ChatGPTMessageModel message;
            public string finish_reason;
        }

        [System.Serializable]
        public class Usage
        {
            public int prompt_tokens;
            public int completion_tokens;
            public int total_tokens;
        }
    }
}

