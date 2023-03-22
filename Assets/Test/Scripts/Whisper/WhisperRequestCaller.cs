using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using Models.WhisperAPI;

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

    /// <summary>
    /// WhisperAPIConnection���ĂсA��������ǂݎ�����������Ԃ�
    /// </summary>
    /// <param name="audioFilePath"></param>
    /// <returns></returns>
    public async UniTask<string> WhisperRequestCall(string audioFilePath)
    {
        _token = _cts.Token;
        _whisperConnection = new(_saveAPIKeyManager.LoadApiPath());

        WhisperAPIResponseModel responseModel = await _whisperConnection.RequestAsync(_token, audioFilePath);

        //�^�������I�[�f�B�I�t�@�C�����폜����
        System.IO.File.Delete(Application.dataPath + $"/{audioFilePath}.wav");

        return responseModel.text;
    }
}