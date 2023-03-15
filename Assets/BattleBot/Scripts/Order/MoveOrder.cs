using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveOrder : MonoBehaviour, ICommand
{
    public void Command(string[] arguments)
    {
        throw new System.NotImplementedException();
    }

    public void Move(Vector3 dir)
    {

    }
}
