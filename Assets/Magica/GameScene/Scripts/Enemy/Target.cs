using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弾に当たったときの的のクラス
/// </summary>
public class Target : MonoBehaviour, IHit
{
    [SerializeField] GameObject _fireEffect;

    void Start()
    {
        _fireEffect.SetActive(false);
    }

    public void Hit()
    {
        _fireEffect.SetActive(true);
    }
}
