using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using Models.WhisperAPI;

/// <summary>
/// WhisperAPIを呼び出すクラス
/// </summary>
public class WhisperRequestCaller : MonoBehaviour
{
    [SerializeField] SaveAPIKeyManager _saveAPIKeyManager;
    [SerializeField] GPTCallManger _gPTCallManger;

    private CancellationTokenSource _cts = new();
    private CancellationToken _token;

    private WhisperAPIConnection _whisperConnection;

    /// <summary>
    /// WhisperAPIConnectionを呼び、音声から読み取った文字列を返す
    /// </summary>
    /// <param name="audioFilePath"></param>
    /// <returns></returns>
    public async UniTask<string> WhisperRequestCall(string audioFilePath)
    {
        _token = _cts.Token;
        _whisperConnection = new(_saveAPIKeyManager.LoadApiPath());

        WhisperAPIResponseModel responseModel = await _whisperConnection.RequestAsync(_token, audioFilePath);

        //録音したオーディオファイルを削除する
        System.IO.File.Delete(Application.dataPath + $"/{audioFilePath}.wav");

        return responseModel.text;
    }
}