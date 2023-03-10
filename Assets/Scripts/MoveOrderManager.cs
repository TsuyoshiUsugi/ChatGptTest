using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrderManager : MonoBehaviour
{
    [SerializeField] Transform _moveObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DoMoveRequest(Vector3 endPos)
    {
        _moveObj.position = endPos;
    }
}
