using UnityEngine;
using System.Threading;
using Models.WhisperAPI;
using UnityEngine.UI;

/// <summary>
/// WhisperAPIÇåƒÇ—èoÇ∑ÉNÉâÉX
/// </summary>
public class WhisperRequestCaller : MonoBehaviour
{
    [SerializeField] private Text displayText;
    [SerializeField] SaveAPIKeyManager _saveAPIKeyManager;

    private CancellationTokenSource _cts = new();
    private CancellationToken _token;

    private WhisperAPIConnection _whisperConnection;

    public async void WhisperRequestCall(string audioFilePath)
    {
        _token = _cts.Token;
        _whisperConnection = new(_saveAPIKeyManager.LoadApiPath());

        WhisperAPIResponseModel responseModel = await _whisperConnection.RequestAsync(_token, audioFilePath);
        displayText.text = responseModel.text;
    }
}