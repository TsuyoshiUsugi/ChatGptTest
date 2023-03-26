using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerManager : MonoBehaviour, IHit
{
    int _hp = 100;
    public int HP => _hp;

    public void Hit(int damage)
    {
        _hp -= damage;

        if(_hp < 0)
        {
            _hp = 0;
        }
    }
}
