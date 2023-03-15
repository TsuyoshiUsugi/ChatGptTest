using UnityEngine;
/// <summary>
/// コマンドのインターフェース
/// </summary>
public interface ICommand
{
    public void Command(string[] arguments);
}
