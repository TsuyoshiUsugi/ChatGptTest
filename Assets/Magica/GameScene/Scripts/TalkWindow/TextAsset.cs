using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TextData", menuName = "ScriptableObjects/TextAsset")]
public class TextAsset : ScriptableObject
{
    [SerializeField, Multiline(3)] public List<string> Text;
}
