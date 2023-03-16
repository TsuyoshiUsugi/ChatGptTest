using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 各コマンドの単体テスト用
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
