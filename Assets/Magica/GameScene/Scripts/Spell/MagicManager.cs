using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法を登録しているクラス
/// ここを参照して魔法を呼び出す
/// </summary>
public class MagicManager : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] List<ISpell> _spells = new();

    /// <summary>
    /// 
    /// </summary>
    public void Search(string voiceInput)
    { 
        
    }
}
