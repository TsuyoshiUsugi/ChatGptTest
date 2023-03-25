using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager01 : MonoBehaviour
{
    [SerializeField] Target _target;
    [SerializeField] GameObject _timeline2;
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
}
