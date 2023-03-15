using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// バトルボットの指示を行うコンポーネント
/// </summary>
public class BattleBotBrain : MonoBehaviour
{
    /// <summary> 命令一覧 </summary>
    List<ICommand> _commandList = new();

    /// <summary> 現在の命令 </summary>
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
    /// 送られてきたコマンドの名前が
    /// コマンドリストに登録されているコマンドと一致していたら実行する
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
                    //文字列のコマンドから関数名を除いた引数のみの配列を用意する
                    string[] arg = stringCommand.Skip(1).Take(stringCommand.Length - 1).ToArray();
                    command.Command(arg, this.gameObject);
                }
                //引数がない関数を実行するとき
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
        Debug.Log("当てはまる命令がありません");
        OpenJTalk.Speak("当てはまる命令がありません", "takumi_normal");
        return;
    }
}
