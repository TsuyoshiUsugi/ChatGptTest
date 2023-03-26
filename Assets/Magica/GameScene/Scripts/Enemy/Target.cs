using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ’e‚É“–‚½‚Á‚½‚Æ‚«‚Ì“I‚ÌƒNƒ‰ƒX
/// </summary>
public class Target : MonoBehaviour, IHit
{
    [SerializeField] GameObject _fireEffect;
    [SerializeField] GameObject _mark;

    public event Action OnTargetDestroy;

    void Start()
    {
        _fireEffect.SetActive(false);
        _mark.SetActive(true);
    }

    public void Hit(int damage)
    {
        _fireEffect.SetActive(true);
        _mark.SetActive(false);

        OnTargetDestroy();
    }
}
