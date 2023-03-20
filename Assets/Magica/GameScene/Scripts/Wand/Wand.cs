using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioRecord;

/// <summary>
/// �������r�����郏���h�N���X
/// �}�E�X���N���b�N���͉����F��������B
/// </summary>
public class Wand : MonoBehaviour
{
    [Header("�Q��")]
    [SerializeField] VoiceRecorder _recordVoice;

    // Update is called once per frame
    void Update()
    {
        CastSpell();
    }

    private void CastSpell()
    {
        if (Input.GetButtonDown("CastSpell"))
        {
            //record��true�ɂ���
            _recordVoice.OnRecordButtonClicked();
        }
        else if(Input.GetButtonUp("CastSpell"))
        {
            //record��false�ɂ���
            _recordVoice.OnRecordButtonClicked();
        }
    }
}
