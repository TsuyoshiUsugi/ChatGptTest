using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������r�����郏���h�N���X
/// �}�E�X���N���b�N���͉����F��������B
/// </summary>
public class Wand : MonoBehaviour
{
    [Header("�Q��")]
    [SerializeField] RecordVoice _recordVoice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while(Input.GetButtonDown("CastSpell"))
        {
            Debug.Log("�r����");
        }
    }
}
