using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マウスの動きでカメラを動かす
/// </summary>
public class FPSPlayerCamController : MonoBehaviour
{
    Camera _cam;
    Rigidbody _rb;
    float _h;
    float _v;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ReadKey();

        RotateCam();
    }

    void ReadKey()
    {
        _h = Input.GetAxisRaw("Mouse X");
        _v = Input.GetAxisRaw("Mouse Y");
    }

    void RotateCam()
    {
        var hInputAngle = Quaternion.AngleAxis(_h, Vector3.up);

        _rb.rotation *= hInputAngle;
    }
}
