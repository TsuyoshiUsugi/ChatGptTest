using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    [SerializeField] Image _fadePanel;
    [SerializeField] float _fadeTime;
    [SerializeField] Ease ease;

    private void Start()
    {
        Color a = new(0, 0, 0, 1);
        _fadePanel.color = a;
    }

    public void DoFadeIn()
    {
        _fadePanel.DOFade(0f, _fadeTime).SetEase(ease);
    }

    public void DoFadeOut()
    {
        _fadePanel.DOFade(1f, _fadeTime).SetEase(ease);
    }
}
