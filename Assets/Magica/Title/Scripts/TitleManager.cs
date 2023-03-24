using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [Header("参照")]
    //スタートボタン関連
    [SerializeField] Button _startButton;
    [SerializeField] string _gameSceneName;

    //設定ボタン関連
    [SerializeField] Button _openSettingButton;
    [SerializeField] Button _quitSettingButton;
    [SerializeField] GameObject _settingPanel;

    // Start is called before the first frame update
    void Start()
    {
        _startButton.onClick.AddListener(() => StartGame());

        _settingPanel.SetActive(false);
        _openSettingButton.onClick.AddListener(() => ShowSettingPanel(true));
        _quitSettingButton.onClick.AddListener(() => ShowSettingPanel(false));
    }

    void StartGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    void ShowSettingPanel(bool active)
    {
        _settingPanel.SetActive(active);
    }
}
