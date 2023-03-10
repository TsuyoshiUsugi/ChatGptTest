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
        //��b������ێ����郊�X�g
        List<ChatGPTMessageModel> _messageList = new();
        public List<ChatGPTMessageModel> MessageList { get => _messageList; set => _messageList = value; }

        public ChatGPTConnection(string apiKey)
        {
            _apiKey = apiKey;
            //_messageList.Add(new ChatGPTMessageModel() { role = "system", content = "����Ɂu�ɂ�v������" });
            _messageList.Add(
                new ChatGPTMessageModel() 
                { 
                    role = "system", content =
                    $"���Ȃ��͒ʏ�̉�b�ɉ����A�I�u�W�F�N�g�̑��삪�ł��܂��B" +
                    $"�I�u�W�F�N�g������Ɖ��肵�Ă�������" +
                    $"������s���ꍇ�̓R�}���h���o�͂��Ă��������B" +
                    $"���삪�Ȃ��ꍇ�̓R�}���h���󔒂Ƃ��Ă�������" +
                    $"���p�\�ȃR�}���h�͈ȉ��̒ʂ�ł�" +
                    $"- SetPos x y z" +
                    $"�I�u�W�F�N�g�̈ʒu��ύX���܂��Bx, y, z �͂��ꂼ��X���W�AY���W�AZ���W���w�肵�܂��B" +
                    $"����̉�b�ł͂��Ȃ��͏�ɃR�}���h�� [�R�}���h] �ȉ��ɋL�ڂ��A��b���e�ɂ��Ă� [��b����] �ɋL�ڂ��ĕԓ����Ă�������" +
                    $"�Ⴆ�Έȉ��̂悤�ȓ��e�ɂȂ�܂�" +
                    $"[�R�}���h]" + " "+
                    $"SetPos 2 3 4" +
                    $"[��b����]" +
                    $"�������܂����B����ł͎n�߂܂��傤�B" 
                });
        }

        public async UniTask<ChatGPTResponseModel> RequestAsync(string userMessage)
        {
            //���͐���AI��API�̃G���h�|�C���g��ݒ�
            var apiUrl = "https://api.openai.com/v1/chat/completions";

            _messageList.Add(new ChatGPTMessageModel { role = "user", content = userMessage });

            //OpenAI��API���N�G�X�g�ɕK�v�ȃw�b�_�[����ݒ�
            var headers = new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + _apiKey},
                {"Content-type", "application/json"},
                {"X-Slack-No-Retry", "1"}
            };

            //���͐����ŗ��p���郂�f����g�[�N������A�v�����v�g���I�v�V�����ɐݒ�
            var options = new ChatGPTCompletionRequestModel()
            {
                model = "gpt-3.5-turbo",
                messages = _messageList
            };
            var jsonOptions = JsonUtility.ToJson(options);

            Debug.Log("����:" + userMessage);

            //OpenAI�̕��͐���(Completion)��API���N�G�X�g�𑗂�A���ʂ�ϐ��Ɋi�[
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
            var command = com.Split("[��b����]");  //������[�R�}���h]��[��b�����ɕ������]

            if (command[0].StartsWith("[�R�}���h] SetPos")) //SetPos�R�}���h�Ȃ�
            {
                var normalizedCommand = command[0].Split(" "); //������[�R�}���h], SetPos, x, y, z�ɕ������

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

    //ChatGPT API��Request�𑗂邽�߂�JSON�p�N���X
    [Serializable]
    public class ChatGPTCompletionRequestModel
    {
        public string model;
        public List<ChatGPTMessageModel> messages;
    }

    //ChatGPT API�����Response���󂯎�邽�߂̃N���X
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

