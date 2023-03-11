using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AAA.OpenAI;
using UniRx;

public class GPTCallManger : MonoBehaviour
{
    [SerializeField] string _openAIApiKey = "";
    [SerializeField] string _requestCommand = "����ɂ���";

    readonly ChatGPTMessageModel _assumption = new ChatGPTMessageModel()
    {
        role = "system",
        content =
                $"���Ȃ��͒ʏ�̉�b�ɉ����A�I�u�W�F�N�g�̑��삪�ł��܂��B" +
                $"�I�u�W�F�N�g������Ɖ��肵�Ă�������" +
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
        Debug.Log("���N�G�X�g");
        var chatGPTConnection = new ChatGPTConnection(_openAIApiKey);
        chatGPTConnection.MessageList.Add(_assumption);
    
        chatGPTConnection.RequestAsync(_requestCommand);
    }


}
