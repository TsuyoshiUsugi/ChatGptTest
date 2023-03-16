using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    Animator _animator;
    
    [Header("ê›íËíl")]
    [SerializeField] float _hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_hp <= 0)
        {
            _animator.SetBool("Death", true);
        }
    }

    public void Hit(float dmg)
    {
        _hp -= dmg;
    }
}
