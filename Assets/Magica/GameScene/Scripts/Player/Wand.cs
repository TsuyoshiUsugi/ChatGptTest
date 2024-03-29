using UnityEngine;
using AudioRecord;
using Cysharp.Threading.Tasks;

/// <summary>
/// 呪文を詠唱するワンドクラス
/// マウス左クリック中は音声認識をする。
/// </summary>
public class Wand : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] VoiceRecorder _recordVoice;
    [SerializeField] WhisperRequestCaller _whisperRequestCaller;
    [SerializeField] MagicManager _magicManager;

    // Update is called once per frame
    void Update()
    {
        CastSpell();
    }

    private void CastSpell()
    {
        if (Input.GetButtonDown("CastSpell"))
        {
            _recordVoice.StartRecording();
        }
        else if(Input.GetButtonUp("CastSpell"))
        {
            Cast();
        }
    }

    private async void Cast()
    {
        var fileName = _recordVoice.StopRecording();
        var spell = await _whisperRequestCaller.WhisperRequestCall(fileName);
        _magicManager.SearchSpell(spell.ToString());
    }
}
