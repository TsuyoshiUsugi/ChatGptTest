using System.Linq;
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
        _commandList.Add(new MoveOrder());


        foreach (var command in _commandList)
        {
            Debug.Log(nameof());
        }



        _orderCommand.Subscribe(com => SelectCommand(com)).AddTo(this.gameObject);
    }

    /// <summary>
    /// �����Ă����R�}���h�̖��O��
    /// �R�}���h���X�g�ɓo�^����Ă���R�}���h�ƈ�v���Ă�������s����
    /// </summary>
    /// <param name="stringCommand"></param>
    void SelectCommand(string[] stringCommand)
    {
        if(stringCommand == null)
        {
            CheckException();
            return;
        }

        foreach (var command in _commandList)
        {

            if(stringCommand[1] == nameof(command))
            {
                if (stringCommand.Length >= 3)
                {
                    //������̃R�}���h����֐����������������݂̂̔z���p�ӂ���
                    string[] arg = stringCommand.Skip(1).Take(stringCommand.Length - 1).ToArray();
                    command.Command(arg, this.gameObject);
                }
                //�������Ȃ��֐������s����Ƃ�
                else
                {
                    string[] dummy = { };
                    command.Command(dummy, this.gameObject);
                    return;
                }
            }
        }

        CheckException();
    }

    void CheckException()
    {
        Debug.Log("���Ă͂܂閽�߂�����܂���");
        OpenJTalk.Speak("���Ă͂܂閽�߂�����܂���", "takumi_normal");
        return;
    }
}
