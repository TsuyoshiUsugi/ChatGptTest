using UnityEngine;
using AudioRecord;
using Cysharp.Threading.Tasks;

/// <summary>
/// �������r�����郏���h�N���X
/// �}�E�X���N���b�N���͉����F��������B
/// </summary>
public class Wand : MonoBehaviour
{
    [Header("�Q��")]
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
