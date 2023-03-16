using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// �o�g���{�b�g�̎w�����s���R���|�[�l���g
/// </summary>
public class BattleBotBrain : MonoBehaviour
{
    /// <summary> ���߈ꗗ </summary>
    List<ICommand> _commandList = new();

    /// <summary> ���݂̖��� </summary>
    ReactiveProperty<string[]> _orderCommand = new();
    public ReactiveProperty<string[]> OrderCommand { get => _orderCommand; set => _orderCommand = value; }

    // Start is called before the first frame update
    void Start()
    {
        AddOrder();
        _orderCommand.Subscribe(com => SelectCommand(com)).AddTo(this.gameObject);
    }

    /// <summary>
    /// �����ɕt�^����Ă��閽�߂����s����
    /// </summary>
    void AddOrder()
    {
        _commandList = GetComponents<ICommand>().ToList();

    }

    /// <summary>
    /// �����Ă����R�}���h�̖��O��
    /// �R�}���h���X�g�ɓo�^����Ă���R�}���h�ƈ�v���Ă�������s����
    /// </summary>
    /// <param name="stringCommand"></param>
    void SelectCommand(string[] stringCommand)
    {
        if(stringCommand == null || stringCommand.Length == 1)
        {
            if (stringCommand == null)
            {
                Debug.Log($"�`�F�b�N:null");
            }
            else
            {
                Debug.Log($"�`�F�b�N:{stringCommand[1]}");
            }

            NoticeException();
            return;
        }

        foreach (var command in _commandList)
        {
            if(stringCommand[1].TrimEnd() == command.GetType().Name)
            {

                if (stringCommand.Length >= 3)
                {
                    //������̃R�}���h����[�R�}���h]�Ɗ֐�����""�������������݂̂̔z���p�ӂ���
                    string[] arg = stringCommand.Skip(2).Where(x => x != "").Take(stringCommand.Length - 1).ToArray();

                    foreach (var item in arg)
                    {
                        Debug.Log(item) ;
                    }
                    Debug.Log("���s�P");
                    command.Command(arg, this.gameObject);
                    return;
                }
                //�������Ȃ��֐������s����Ƃ�
                else
                {
                    Debug.Log("���s�Q");
                    string[] dummy = { };
                    command.Command(dummy, this.gameObject);
                    return;
                }
            }
        }

        NoticeException();
    }

    void NoticeException()
    {
        Debug.Log("���Ă͂܂閽�߂�����܂���");
        OpenJTalk.Speak("���Ă͂܂閽�߂�����܂���", "takumi_normal");
        return;
    }
}
