using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炎魔法を生成する
/// </summary>
public class FireMagic : MonoBehaviour, ISpell
{
    [SerializeField] GameObject _fire;
    [SerializeField] GameObject _magicCircle;
    [SerializeField] GameObject _magicGeneratePoint;
    float _speed = 10;
    float _magicCircleTime = 2;

    public string SpellName => "ファイアボルト";

    public void CastSpell()
    {
        StartCoroutine(nameof(GenerateSpellCircle));

        GameObject fire = Instantiate(_fire);
        fire.transform.position = _magicCircle.transform.position;

        Vector3 dir = _magicCircle.transform.forward;
        fire.GetComponent<Rigidbody>().velocity = dir * 10;
    }

    IEnumerator GenerateSpellCircle()
    {
        GameObject magicCircle = Instantiate(_magicCircle);
        magicCircle.transform.position = _magicGeneratePoint.transform.position;

        yield return new WaitForSeconds(_magicCircleTime);

        Destroy(magicCircle);
    }
}
