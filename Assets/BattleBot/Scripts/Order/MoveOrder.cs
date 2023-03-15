using System;
using System.Threading;
using System.Collections;
using UnityEngine;

/// <summary>
/// �ړ��Ɋւ���R�}���h�̃N���X
/// </summary>
[System.Serializable]
public class MoveOrder : ICommand
{
    GameObject _targetObj;
    float _speed = 10;

    public void Command(string[] arguments, GameObject bot)
    {
        float[] normalizedArguments = Array.ConvertAll(arguments, arg => float.Parse(arg)); //�����ň�����x, y, z�ɕ������

        float x = normalizedArguments[0];
        float y = normalizedArguments[1];
        float z = normalizedArguments[2];
        float t = normalizedArguments[3];
        int intT = (int)(t * 1000);

        Vector3 dir = new(x, y, z);

        Timer timer = new(_ => Move(dir), null, 0, intT);
        Thread.Sleep(intT);
        timer.Dispose();

        Debug.Log("MoveOrder���s");
    }

    /// <summary>
    /// �w�肳�ꂽ�����Ɏw�肳�ꂽ���Ԉړ�����B���Ԃ�-1�̂Ƃ��A���̎w��������܂ł����Ɠ���������
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="time"></param>
    void Move(Vector3 dir)
    {
        _targetObj.transform.position += _speed * dir;
    }
}
