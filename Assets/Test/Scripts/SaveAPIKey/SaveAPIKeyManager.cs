using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAPIKeyManager : MonoBehaviour
{
    public event Action<string> OnSaveAPIKey;

    void Start()
    {
        OnSaveAPIKey += key =>  SaveAPIKey(key);
    }

    /// <summary>
    /// APIPath��ۑ�����
    /// </summary>
    /// <param name="aPIKey"></param>
    public void SaveAPIKey(string aPIKey)
    {
        string savePath = Application.persistentDataPath + "/APIKey.json";
        Debug.Log(savePath);
        KeyData data = new();
        data.APIKey = aPIKey;

        string jsonData = JsonUtility.ToJson(data);

        using (StreamWriter  streamWriter = new(savePath))
        {
            streamWriter.Write(jsonData);
        }

        Debug.Log("Key���Z�[�u���܂���");
    }

    /// <summary>
    /// �ۑ�����Ă���APIPath��Ԃ�
    /// </summary>
    /// <returns></returns>
    public string LoadApiPath()
    {
        string savePath = Application.persistentDataPath + "/APIKey.json";

        using (StreamReader streamReader = new(savePath))
        {
            var loadJsonData = streamReader.ReadToEnd();
            var keyData = JsonUtility.FromJson<KeyData>(loadJsonData);
            return keyData.APIKey;
        }
        
    }
}

[System.Serializable]
public class KeyData
{
    public string APIKey;
}
