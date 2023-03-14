using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestObj : MonoBehaviour
{
    [SerializeField] Transform _moveObj;

    public void DoMoveRequest(Vector3 addPos)
    {
        _moveObj.position = addPos;
    }
}
