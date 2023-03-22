using System.Linq;
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
    /// 登録されている魔法から音声入力されたものを見つける
    /// </summary>
    public void SearchSpell(string voiceInput)
    {
        Debug.Log("SearchSpell");

        var spell = _spells.FirstOrDefault(x => x.SpellName == voiceInput);
        spell?.CastSpell();
    }
}
