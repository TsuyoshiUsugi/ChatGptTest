using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// 移動に関するコマンドのクラス
/// </summary>
[System.Serializable]
public class MoveOrder : MonoBehaviour, ICommand
{
    GameObject _targetObj;
    float _speed = 10;

    public void Command(string[] arguments, GameObject bot)
    {
        var normalizedArguments = Array.ConvertAll(arguments, arg => float.Parse(arg)); //ここで引数をx, y, zに分かれる

        float x = normalizedArguments[0];
        float y = normalizedArguments[1];
        float z = normalizedArguments[2];
        float t = normalizedArguments[3];

        Vector3 dir = new(x, y, z);

        Move(dir, t);
    }

    /// <summary>
    /// 指定された方向に指定された時間移動する。時間が-1のとき、次の指示が来るまでずっと動き続ける
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="time"></param>
    public void Move(Vector3 dir, float time)
    {
        this.UpdateAsObservable().Subscribe(_ => _targetObj.transform.position += dir * _speed * Time.deltaTime);
    }

    IEnumerator CountTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
