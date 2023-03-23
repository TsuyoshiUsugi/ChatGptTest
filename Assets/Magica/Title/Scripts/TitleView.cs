using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleView : MonoBehaviour
{

    [SerializeField] GameObject _titlePanel;
    float _showTitlePanelSec = 2;

    private void Start()
    {
        StartCoroutine(nameof(ShowCaution));
    }

    IEnumerator ShowCaution()
    {
        _titlePanel.SetActive(true);
        yield return new WaitForSeconds(_showTitlePanelSec);
        _titlePanel.SetActive(false);
    }
}
