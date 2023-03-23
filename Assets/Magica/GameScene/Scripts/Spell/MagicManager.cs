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

    public void Start()
    {
        GetComponents<ISpell>().ToList().ForEach(spell => _spells.Add(spell)) ;

        _spells.ForEach(spell => Debug.Log(spell));
    }

    /// <summary>
    /// �o�^����Ă��閂�@���特�����͂��ꂽ���̂�������
    /// </summary>
    public void SearchSpell(string voiceInput)
    {
        Debug.Log(voiceInput);
        var spell = _spells.FirstOrDefault(x => x.SpellName == voiceInput);
        spell?.CastSpell();
    }
}
