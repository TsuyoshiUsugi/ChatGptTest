using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneView : MonoBehaviour
{
    [Header("リクエスト")]
    [SerializeField] InputField _orderInputField;
    [SerializeField] Button _sendRequestButton;

    [Header("APIキー入力")]
    [SerializeField] InputField _apiKeyInputField;
    [SerializeField] Button _saveAPIKeyButton;

    public event Action<string> OnSendRequestButtonClicked;
    public event Action<string> OnSaveAPIKeyButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        _sendRequestButton.onClick.AddListener(() => OnSendRequestButtonClicked(_orderInputField.text));
        _saveAPIKeyButton.onClick.AddListener(() => OnSaveAPIKeyButtonClicked(_apiKeyInputField.text));

    }
}
