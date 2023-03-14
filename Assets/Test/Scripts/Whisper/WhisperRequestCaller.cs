using UnityEngine;
using System.Threading;
using Models.WhisperAPI;
using UnityEngine.UI;

/// <summary>
/// WhisperAPI���Ăяo���N���X
/// </summary>
public class WhisperRequestCaller : MonoBehaviour
{
    [SerializeField] SaveAPIKeyManager _saveAPIKeyManager;
    [SerializeField] GPTCallManger _gPTCallManger;

    private CancellationTokenSource _cts = new();
    private CancellationToken _token;

    private WhisperAPIConnection _whisperConnection;

    public async void WhisperRequestCall(string audioFilePath)
    {
        _token = _cts.Token;
        _whisperConnection = new(_saveAPIKeyManager.LoadApiPath());

        WhisperAPIResponseModel responseModel = await _whisperConnection.RequestAsync(_token, audioFilePath);
        _gPTCallManger.Request(responseModel.text);
        // �^�������I�[�f�B�I�t�@�C�����폜����
        System.IO.File.Delete(Application.dataPath + "/recording.wav");
    }
}