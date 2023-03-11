using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAPIKeyManager : MonoBehaviour
{
    /// <summary>
    /// APIPath��ۑ�����
    /// </summary>
    /// <param name="aPIKey"></param>
    public void SaveAPIKey(string aPIKey)
    {
        string savePath = Application.persistentDataPath + "/APIKey.json";
        KeyData data = new();
        data.APIKey = aPIKey;

        string jsonData = JsonUtility.ToJson(data);

        using (StreamWriter  streamWriter = new(savePath))
        {
            streamWriter.Write(jsonData);
        }
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
