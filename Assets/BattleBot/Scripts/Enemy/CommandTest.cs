using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e�R�}���h�̒P�̃e�X�g�p
/// </summary>
public class CommandTest : MonoBehaviour
{
    ICommand _commands;

    // Start is called before the first frame update
    void Start()
    {
        _commands = GetComponent<MoveOrder>();

        string[] dummyString = { "0", "0", "1", "-1"};
        GameObject dummyObj = this.gameObject;

        _commands.Command(dummyString, this.gameObject);
    }
}
