using System;
using Models.WhisperAPI;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;


public class WhisperAPIConnection
{
    private readonly string _apiKey;
    private const string ApiUrl = "https://api.openai.com/v1/audio/transcriptions";
    private const string FilePath = "Assets/Audio/Record1.wav";
    private const string ModelName = "whisper-1";

    public WhisperAPIConnection(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async UniTask<WhisperAPIResponseModel> RequestAsync(CancellationToken token, string filePath)
    {
        Debug.Log(filePath);

        var headers = new Dictionary<string, string>
        {
            { "Authorization", "Bearer " + _apiKey }
        };

        byte[] fileBytes = await File.ReadAllBytesAsync(filePath, cancellationToken: token);

        //â∫ÇÃÅöÇ∆Ç«ÇøÇÁÇ≈Ç‡ê≥ÇµÇ≠èàóùÇ≥ÇÍÇÈ
        WWWForm form = new();
        form.AddField("model", ModelName);
        form.AddBinaryData("file", fileBytes, Path.GetFileName(filePath), "multipart/form-data");

        //// è„ÇÃÅöÇ∆Ç«ÇøÇÁÇ≈Ç‡ê≥ÇµÇ≠èàóùÇ≥ÇÍÇÈ
        //List<IMultipartFormSection> form = new();
        //form.Add(new MultipartFormDataSection("model", ModelName));
        //form.Add(new MultipartFormFileSection("file", fileBytes, Path.GetFileName(FilePath), "multipart/form-data"));

        using UnityWebRequest request = UnityWebRequest.Post(ApiUrl, form);

        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        await request.SendWebRequest().ToUniTask(cancellationToken: token);

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            throw new Exception();
        }
        else
        {
            var responseString = request.downloadHandler.text;
            var responseObject = JsonUtility.FromJson<WhisperAPIResponseModel>(responseString);
            Debug.Log("WhisperAPI: " + responseObject.text);
            return responseObject;
        }
    }
}

namespace Models.WhisperAPI
{
    [Serializable]
    public class WhisperAPIResponseModel
    {
        public string text;
    }
}