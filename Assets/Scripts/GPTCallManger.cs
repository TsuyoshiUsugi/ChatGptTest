using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using AAA.OpenAI;
using Cysharp.Threading.Tasks;

public class GPTCallManger : MonoBehaviour
{
    [SerializeField] SaveAPIKeyManager _saveAPIKeyManager;
    [SerializeField] string _requestCommand = "����ɂ���";

    List<ChatGPTMessageModel> _messageList = new();

    readonly ChatGPTMessageModel _assumption = new()
    {
        role = "system",
        content =
                $"���Ȃ��͒ʏ�̉�b�ɉ����A�I�u�W�F�N�g�̑��삪�ł��܂��B" +
                $"�I�u�W�F�N�g��0,0,0�̏ꏊ�ɂ���Ɖ��肵�܂�" +
                $"������s���ꍇ�̓R�}���h���o�͂��Ă��������B" +
                $"���삪�Ȃ��ꍇ�̓R�}���h���󔒂Ƃ��Ă�������" +
                $"���p�\�ȃR�}���h�͈ȉ��̒ʂ�ł�" +
                $"- SetPos x y z" +
                $"�I�u�W�F�N�g�̈ʒu��ύX���܂��Bx, y, z �͂��ꂼ��X���W�AY���W�AZ���W���w�肵�܂��B" +
                $"����̉�b�ł͂��Ȃ��͏�ɃR�}���h�� [�R�}���h] �ȉ��ɋL�ڂ��A��b���e�ɂ��Ă� [��b����] �ɋL�ڂ��ĕԓ����Ă�������" +
                $"�Ⴆ�Έȉ��̂悤�ȓ��e�ɂȂ�܂�" +
                $"[�R�}���h]" + " " +
                $"SetPos 2 3 4" +
                $"[��b����]" +
                $"�������܂����B����ł͎n�߂܂��傤�B"
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
        var command = com.Split("[��b����]");  //������[�R�}���h]��[��b�����ɕ������]

        if (command[0].StartsWith("[�R�}���h] SetPos")) //SetPos�R�}���h�Ȃ�
        {
            var normalizedCommand = command[0].Split(" "); //������[�R�}���h], SetPos, x, y, z�ɕ������

            float x = float.Parse(normalizedCommand[2]);
            float y = float.Parse(normalizedCommand[3]);
            float z = float.Parse(normalizedCommand[4]);

            GameObject.FindObjectOfType<RequestObj>().DoMoveRequest(new(x, y, z));

        }
    }


}
