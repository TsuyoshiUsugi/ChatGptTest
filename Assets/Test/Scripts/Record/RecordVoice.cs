using System;
using System.Collections;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 音声を録音する
/// </summary>
public class RecordVoice : MonoBehaviour
{
    [SerializeField] Button _recordButton;
    [SerializeField] Text _isRecordingText;
    [SerializeField] WhisperRequestCaller _whisperRequestCaller;
    AudioClip _voiceClip;

    string _micName = "";
    bool _isRecording = false;

    //サンプリング周波数
    const int _samplingFrequency = 44100;

    //最大録音時間
    const int _maxRecordSecTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        
        _micName = Microphone.devices[0];

        //_recordButton.onClick.AddListener(() => OnRecordButtonClicked());
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            OnRecordButtonClicked();
        }
        else if(Input.GetKeyUp(KeyCode.F))
        {
            OnRecordButtonClicked();
        }
    }

    void OnRecordButtonClicked()
    {
        if(!_isRecording)
        {
            _isRecording = true;

            _isRecordingText.text = "音声認識中";

            _voiceClip = Microphone.Start(
                deviceName: _micName,
                loop: false,
                lengthSec: _maxRecordSecTime,
                frequency: _samplingFrequency);
        }
        else
        {
            _isRecording = false;

            _isRecordingText.text = "音声認識を始める(Fキー)";
            Microphone.End(deviceName: _micName);

            //Wavファイル生成
            Wav.ExportWav(_voiceClip, Application.dataPath + "/recording.wav");

            //WhisperAPIにリクエスト送信
            _whisperRequestCaller.WhisperRequestCall(Application.dataPath + "/recording.wav");
            
        }
    }

}

public static class Wav
{
    private const int BitsPerSample = 16;
    private const int AudioFormat = 1;

    /// <summary>
    /// Wavファイルのデータ構造に変換する
    /// </summary>
    /// <param name="audioClip"></param>
    /// <returns></returns>
    public static byte[] ToWav(this AudioClip audioClip)
    {
        using var stream = new MemoryStream();

        WriteRiffChunk(audioClip, stream);
        WriteFmtChunk(audioClip, stream);
        WriteDataChunk(audioClip, stream);

        return stream.ToArray();
    }

    /// <summary>
    /// Wavファイルをpathに出力する
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="path"></param>
    public static void ExportWav(this AudioClip audioClip, string path)
    {
        using var stream = new FileStream(path, FileMode.Create);

        WriteRiffChunk(audioClip, stream);
        WriteFmtChunk(audioClip, stream);
        WriteDataChunk(audioClip, stream);
    }

    private static void WriteRiffChunk(AudioClip audioClip, Stream stream)
    {
        // ChunkID RIFF
        stream.Write(Encoding.ASCII.GetBytes("RIFF"));

        // ChunkSize
        const int headerByteSize = 44;
        var chunkSize = BitConverter.GetBytes((UInt32)(headerByteSize + audioClip.samples * audioClip.channels * BitsPerSample / 8));
        stream.Write(chunkSize);

        // Format WAVE
        stream.Write(Encoding.ASCII.GetBytes("WAVE"));
    }

    private static void WriteFmtChunk(AudioClip audioClip, Stream stream)
    {
        // Subchunk1ID fmt
        stream.Write(Encoding.ASCII.GetBytes("fmt "));

        // Subchunk1Size (16 for PCM)
        stream.Write(BitConverter.GetBytes((UInt32)16));

        // AudioFormat (PCM=1)
        stream.Write(BitConverter.GetBytes((UInt16)AudioFormat));

        // NumChannels (Mono = 1, Stereo = 2, etc.)
        stream.Write(BitConverter.GetBytes((UInt16)audioClip.channels));

        // SampleRate (audioClip.sampleではなくaudioClip.frequencyのはず)
        stream.Write(BitConverter.GetBytes((UInt32)audioClip.frequency));

        // ByteRate (=SampleRate * NumChannels * BitsPerSample/8)
        stream.Write(BitConverter.GetBytes((UInt32)(audioClip.samples * audioClip.channels * BitsPerSample / 8)));

        // BlockAlign (=NumChannels * BitsPerSample/8)
        stream.Write(BitConverter.GetBytes((UInt16)(audioClip.channels * BitsPerSample / 8)));

        // BitsPerSample
        stream.Write(BitConverter.GetBytes((UInt16)BitsPerSample));
    }

    private static void WriteDataChunk(AudioClip audioClip, Stream stream)
    {
        // Subchunk2ID data
        stream.Write(Encoding.ASCII.GetBytes("data"));

        // Subchuk2Size
        stream.Write(BitConverter.GetBytes((UInt32)(audioClip.samples * audioClip.channels * BitsPerSample / 8)));

        // Data
        var floatData = new float[audioClip.samples * audioClip.channels];
        audioClip.GetData(floatData, 0);

        switch (BitsPerSample)
        {
            case 8:
                foreach (var f in floatData) stream.Write(BitConverter.GetBytes((sbyte)(f * sbyte.MaxValue)));
                break;
            case 16:
                foreach (var f in floatData) stream.Write(BitConverter.GetBytes((short)(f * short.MaxValue)));
                break;
            case 32:
                foreach (var f in floatData) stream.Write(BitConverter.GetBytes((int)(f * int.MaxValue)));
                break;
            case 64:
                foreach (var f in floatData) stream.Write(BitConverter.GetBytes((float)(f * float.MaxValue)));
            default:
                throw new NotSupportedException(nameof(BitsPerSample));
        }
    }
}
