using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マウスの動きでカメラを動かす
/// </summary>
public class FPSPlayerCamController : MonoBehaviour
{
    Camera _cam;
    float _h;
    float _v;

    // Start is called before the first frame update
    void Start()
    {
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
        _h = Input.GetAxis("Mouse X");
        _v = -1 * Input.GetAxis("Mouse Y");
    }

    void RotateCam()
    {
        var hInputAngle = Quaternion.AngleAxis(_h, Vector3.up);
        var vInputAngle = Quaternion.AngleAxis(_v, Vector3.right);

        this.transform.rotation *= hInputAngle;
        _cam.transform.rotation *= vInputAngle;
    }
}
