using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager01 : MonoBehaviour
{
    [SerializeField] Target _target;
    [SerializeField] GameObject _timeline2;
    [SerializeField] string _nextScene;
    // Start is called before the first frame update
    void Start()
    {
        _target.OnTargetDestroy += () => Active();
        _timeline2.SetActive(false);
    }

    void Active()
    {
        _timeline2.SetActive(true);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(_nextScene);
    }
}
