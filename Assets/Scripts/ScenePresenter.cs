using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePresenter : MonoBehaviour
{
    [SerializeField] SceneView _sceneView;
    [SerializeField] SaveAPIKeyManager _saveAPIKeyManager;
    [SerializeField] GPTCallManger _gPTCallManger;

    // Start is called before the first frame update
    void Start()
    {
        _sceneView.OnSaveAPIKeyButtonClicked += (x) => _saveAPIKeyManager.SaveAPIKey(x);
        _sceneView.OnSendRequestButtonClicked += (x) => _gPTCallManger.Request(x);
    }

}
