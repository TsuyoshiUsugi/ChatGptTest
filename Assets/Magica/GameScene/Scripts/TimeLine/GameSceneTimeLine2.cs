using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneTimeLine2 : MonoBehaviour
{
    [SerializeField] Target _target;
    // Start is called before the first frame update
    void Start()
    {
        _target.OnTargetDestroy += () => Active();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Active()
    {
        this.gameObject.SetActive(true);
    }
}
