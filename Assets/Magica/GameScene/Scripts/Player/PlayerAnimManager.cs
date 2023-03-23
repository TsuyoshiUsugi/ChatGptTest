using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �q�I�u�W�F�N�g��Animator���擾���đ��삷��
/// </summary>
public class PlayerAnimManager : MonoBehaviour
{
    Animator _animator;
    Vector3 _presentPos = new();
    [SerializeField] Vector3 _dir;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        CheckMovementAmout();
    }

    void CheckMovementAmout()
    {

        DoMoveAnimation();
    }

    void DoMoveAnimation()
    {

        if(Input.GetKey(KeyCode.W))
        {
            MoveForwardAnim(true);
        }
        else
        {
            MoveForwardAnim(false);
        }

    }

    void MoveForwardAnim(bool isActive)
    {
        _animator.SetBool("RunForward", isActive);
    }
}
