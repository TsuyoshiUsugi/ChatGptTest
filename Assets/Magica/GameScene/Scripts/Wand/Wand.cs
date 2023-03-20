using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioRecord;

/// <summary>
/// 呪文を詠唱するワンドクラス
/// マウス左クリック中は音声認識をする。
/// </summary>
public class Wand : MonoBehaviour
{
    [Header("参照")]
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
            //recordをtrueにする
            _recordVoice.OnRecordButtonClicked();
        }
        else if(Input.GetButtonUp("CastSpell"))
        {
            //recordをfalseにする
            _recordVoice.OnRecordButtonClicked();
        }
    }
}
