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
    [SerializeField] WhisperRequestCaller _whisperRequestCaller;
    [SerializeField] 

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
            var fileName = _recordVoice.StopRecording();
            var spell = _whisperRequestCaller.WhisperRequestCall(fileName);
        }
    }
}
