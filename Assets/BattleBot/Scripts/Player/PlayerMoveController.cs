using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WASDÇ≈ÉvÉåÉCÉÑÅ[ÇìÆÇ©Ç∑
/// </summary>
public class PlayerMoveController : MonoBehaviour
{
    [SerializeField] float _sp;
    Rigidbody _rb;
    float _h;
    float _v;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadKey();

        MovePlayer();
    }

    void ReadKey()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        Vector3 dir = new(_h, _rb.velocity.y, _v);
        var moveDir = transform.TransformDirection(dir);
        moveDir *= _sp;

        _rb.velocity = moveDir * Time.deltaTime;
    }
}
