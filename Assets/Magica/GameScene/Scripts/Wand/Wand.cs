using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 呪文を詠唱するワンドクラス
/// マウス左クリック中は音声認識をする。
/// </summary>
public class Wand : MonoBehaviour
{
    [Header("参照")]
    [SerializeField] RecordVoice _recordVoice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while(Input.GetButtonDown("CastSpell"))
        {
            Debug.Log("詠唱中");
        }
    }
}
