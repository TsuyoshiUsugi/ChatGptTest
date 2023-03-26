using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����@�𐶐�����
/// </summary>
public class FireMagic : MonoBehaviour, ISpell
{
    [SerializeField] GameObject _fire;
    [SerializeField] GameObject _magicCircle;
    [SerializeField] GameObject _magicGeneratePoint;
    float _speed = 1;
    float _magicCircleTime = 2;

    public string SpellName => "�t�@�C�A�{���g";

    public void CastSpell()
    {
        StartCoroutine(nameof(GenerateSpellCircle));
    }

    IEnumerator GenerateSpellCircle()
    {
        GameObject magicCircle = Instantiate(_magicCircle);
        magicCircle.transform.position = _magicGeneratePoint.transform.position;
        magicCircle.transform.rotation = _magicGeneratePoint.transform.rotation;

        FireBolt(magicCircle);

        yield return new WaitForSeconds(_magicCircleTime);

        Destroy(magicCircle);
    }

    private void FireBolt(GameObject magicCircle)
    {
        GameObject fire = Instantiate(_fire);
        fire.transform.position = magicCircle.transform.position;

        var dir = Camera.main.transform.forward;
        fire.GetComponent<Rigidbody>().velocity = dir.normalized * _speed;
    }
}
