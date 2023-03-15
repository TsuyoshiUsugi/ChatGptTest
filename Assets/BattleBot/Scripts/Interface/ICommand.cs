using System;
using UnityEngine;
/// <summary>
/// コマンドのインターフェース
/// </summary>
public interface ICommand
{
    void Command(string[] arguments);
}
