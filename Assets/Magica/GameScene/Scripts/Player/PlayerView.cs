using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PlayerView : MonoBehaviour
{
    [SerializeField] PlayerManager _playerManager;

    [SerializeField] Text _floorText;
    [SerializeField] Text _hpText;

    // Start is called before the first frame update
    void Start()
    {
        _playerManager.ObserveEveryValueChanged(hp => _playerManager.HP)
            .Subscribe(hp => _hpText.text = hp.ToString());
    }
}
