using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �e�R�}���h�̒P�̃e�X�g�p
/// </summary>
[RequireComponent(typeof(SearchEnemyOrder))]
public class CommandTest : MonoBehaviour
{
    ICommand _commands;

    // Start is called before the first frame update
    void Start()
    {
        _commands = GetComponent<SearchEnemyOrder>();

        string[] dummyString = { };
        GameObject dummyObj = this.gameObject;

        _commands.Command(dummyString, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
