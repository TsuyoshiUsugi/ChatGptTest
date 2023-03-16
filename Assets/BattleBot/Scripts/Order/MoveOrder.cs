using System;
using System.Threading;
using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

/// <summary>
/// �ړ��Ɋւ���R�}���h�̃N���X
/// </summary>
public class MoveOrder : MonoBehaviour, ICommand
{
    GameObject _targetObj;
    float _speed = 1;

    public void Command(string[] arguments, GameObject bot)
    {
        float x = float.Parse(arguments[0]);
        float y = float.Parse(arguments[1]);
        float z = float.Parse(arguments[2]);
        float t = float.Parse(arguments[3]);
        int intT = (int)(t * 1000);

        Vector3 dir = new(x, y, z);

        Move(dir, t);

        Debug.Log("MoveOrder���s");
    }

    /// <summary>
    /// �w�肳�ꂽ�����Ɏw�肳�ꂽ���Ԉړ�����B���Ԃ�-1�̂Ƃ��A���̎w��������܂ł����Ɠ���������
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="time"></param>
    void Move(Vector3 dir, float time)
    {
        if(time == -1)
        {
            this.UpdateAsObservable()
                .Subscribe(_ => this.transform.position += _speed * Time.deltaTime * dir);
        }
        else
        {
            this.UpdateAsObservable()
                .Take(System.TimeSpan.FromSeconds(time))
                .Subscribe(_ => this.transform.position += _speed * Time.deltaTime * dir);
        }

    }
}
