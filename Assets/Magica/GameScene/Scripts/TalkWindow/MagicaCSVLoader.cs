using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicaCSVLoader : MonoBehaviour
{
    [SerializeField] TalkWindowManager _talkWindowManager;
    TextAsset _csvFile;
    List<string[]> _csvData = new ();
    string _fileName = "Magica_Text_Utf8";

    // Start is called before the first frame update
    void Start()
    {
        _csvFile = Resources.Load(_fileName) as TextAsset;
        StringReader reader = new StringReader(_csvFile.text);

        while (reader.Peek() != -1)
        {
            string[] line = reader.ReadLine().Split(',');
            _csvData.Add(line);
        }

        _talkWindowManager.DoTalkWindowTask(_csvData);
    }
}
