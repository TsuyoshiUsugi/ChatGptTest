using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVLoader : MonoBehaviour
{
    [SerializeField] TalkWindowManager _talkWindowManager;
    TextAsset _csvFile;
    List<string> _csvData = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        _csvFile = Resources.Load("Magica_Text_Utf8") as TextAsset;
        StringReader reader = new StringReader(_csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            _csvData.Add(line);
        }

        
        _talkWindowManager.DoTalkWindowTask(_csvData);
        
    }
}
