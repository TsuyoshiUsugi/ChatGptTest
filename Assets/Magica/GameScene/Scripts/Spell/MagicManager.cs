using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���@��o�^���Ă���N���X
/// �������Q�Ƃ��Ė��@���Ăяo��
/// </summary>
public class MagicManager : MonoBehaviour
{
    [Header("�Q��")]
    [SerializeField] List<ISpell> _spells = new();

    /// <summary>
    /// �o�^����Ă��閂�@���特�����͂��ꂽ���̂�������
    /// </summary>
    public void SearchSpell(string voiceInput)
    {
        Debug.Log("SearchSpell");

        var spell = _spells.FirstOrDefault(x => x.SpellName == voiceInput);
        spell?.CastSpell();
    }
}
