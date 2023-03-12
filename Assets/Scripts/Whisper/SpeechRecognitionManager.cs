using System;
using System.IO;
using System.Collections;
using UnityEngine;

/// <summary>
/// 音声を認識する
/// </summary>
public class SpeechRecognitionManager : MonoBehaviour
{
    private bool isRecording;

    public IEnumerator Record()
    {
        var audioSource = GetComponent<AudioSource>();

        // マイクからオーディオを録音する
        audioSource.clip = Microphone.Start(null, true, 10, 44100);
        audioSource.loop = true;

        while (Microphone.GetPosition(null) <= 0)
        {
            yield return null;
        }

        audioSource.Play();

        isRecording = true;

        yield return new WaitForSeconds(10);

        isRecording = false;

        // オーディオを録音する
        Microphone.End(null);

        // 録音したオーディオをwavファイルに保存する
        var filePath = Application.dataPath + "/recording.wav";
        SavWav.Save(filePath, audioSource.clip);

		//ここでwhisper呼び出す


		// 録音したオーディオファイルを削除する
		System.IO.File.Delete(Application.dataPath + "/recording.wav");

	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (!isRecording)
			{
				StartCoroutine(Record());
			}
		}
	}
}

public static class SavWav
{
	const int HEADER_SIZE = 44;

	public static bool Save(string filename, AudioClip clip)
	{

		if (!filename.ToLower().EndsWith(".wav"))
		{
			filename += ".wav";
		}

		var filepath = Path.Combine(Application.persistentDataPath, filename);

		Debug.Log("Saving to " + filepath);

		// Make sure directory exists if user is saving to sub dir.
		Directory.CreateDirectory(Path.GetDirectoryName(filepath));

		using (var fileStream = CreateEmpty(filepath))
		{

			ConvertAndWrite(fileStream, clip);

			WriteHeader(fileStream, clip);
		}

		return true; // TODO: return false if there's a failure saving the file
	}

	private static FileStream CreateEmpty(string filepath)
	{

		var fileStream = new FileStream(filepath, FileMode.Create);
		byte emptyByte = new byte();

		for (int i = 0; i < HEADER_SIZE; i++) //preparing the header
		{
			fileStream.WriteByte(emptyByte);
		}

		return fileStream;
	}

	private static void ConvertAndWrite(FileStream fileStream, AudioClip clip)
	{

		var samples = new float[clip.samples];

		clip.GetData(samples, 0);

		Int16[] intData = new Int16[samples.Length];

		//converting in 2 float[] steps to Int16[], //then Int16[] to Byte[]

		const float rescaleFactor = 32767; //to convert float to Int16

		for (int i = 0; i < samples.Length; i++)
		{
			intData[i] = (short)(samples[i] * rescaleFactor);
		}

		var byteData = new byte[intData.Length * 2];
		Buffer.BlockCopy(intData, 0, byteData, 0, byteData.Length);

		fileStream.Write(byteData, 0, byteData.Length);
	}

	private static void WriteHeader(FileStream fileStream, AudioClip clip)
	{

		var hz = clip.frequency;
		var channels = clip.channels;
		var samples = clip.samples;

		fileStream.Seek(0, SeekOrigin.Begin);

		var riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
		fileStream.Write(riff, 0, 4);

		var chunkSize = BitConverter.GetBytes(fileStream.Length - 8);
		fileStream.Write(chunkSize, 0, 4);

		var wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
		fileStream.Write(wave, 0, 4);

		var fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
		fileStream.Write(fmt, 0, 4);

		var subChunk1 = BitConverter.GetBytes(16);
		fileStream.Write(subChunk1, 0, 4);

		var two = BitConverter.GetBytes((ushort)2);
		fileStream.Write(two, 0, 2);

		var byteRate = BitConverter.GetBytes(hz * channels * 2);
		fileStream.Write(byteRate, 0, 4);

		var blockAlign = (ushort)(channels * 2);
		fileStream.Write(BitConverter.GetBytes(blockAlign), 0, 2);

		var bitsPerSample = BitConverter.GetBytes(16);
		fileStream.Write(bitsPerSample, 0, 2);

		var datastring = System.Text.Encoding.UTF8.GetBytes("data");
		fileStream.Write(datastring, 0, 4);

		var subChunk2 = BitConverter.GetBytes(samples * channels * 2);
		fileStream.Write(subChunk2, 0, 4);

		fileStream.Close();
	}
}
