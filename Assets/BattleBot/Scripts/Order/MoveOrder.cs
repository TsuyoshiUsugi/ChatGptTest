using System;
using System.Threading;
using System.Collections;
using UnityEngine;

/// <summary>
/// 移動に関するコマンドのクラス
/// </summary>
[System.Serializable]
public class MoveOrder : ICommand
{
    GameObject _targetObj;
    float _speed = 10;

    public void Command(string[] arguments, GameObject bot)
    {
        float[] normalizedArguments = Array.ConvertAll(arguments, arg => float.Parse(arg)); //ここで引数をx, y, zに分かれる

        float x = normalizedArguments[0];
        float y = normalizedArguments[1];
        float z = normalizedArguments[2];
        float t = normalizedArguments[3];
        int intT = (int)(t * 1000);

        Vector3 dir = new(x, y, z);

        Timer timer = new(_ => Move(dir), null, 0, intT);
        Thread.Sleep(intT);
        timer.Dispose();

        Debug.Log("MoveOrder実行");
    }

    /// <summary>
    /// 指定された方向に指定された時間移動する。時間が-1のとき、次の指示が来るまでずっと動き続ける
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="time"></param>
    void Move(Vector3 dir)
    {
        _targetObj.transform.position += _speed * dir;
    }
}
