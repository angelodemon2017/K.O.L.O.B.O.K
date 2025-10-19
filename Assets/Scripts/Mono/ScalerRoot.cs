using DG.Tweening;
using UnityEngine;

public class ScalerRoot : MonoBehaviour
{
    [SerializeField] private float _stretch;
    [SerializeField] private float _duration = 0.5f;

    public void Up()
    {
        DOTween.To(() => _stretch, x => SetStretch(x), 1.5f, _duration)
            .OnComplete(() => DOTween.To(() => _stretch, y => SetStretch(y), 1f, _duration));
    }

    public void Down()
    {
        DOTween.To(() => _stretch, x => SetStretch(x), 0.5f, _duration)
            .OnComplete(() => DOTween.To(() => _stretch, y => SetStretch(y), 1f, _duration));
    }

    private void SetStretch(float value)
    {
        _stretch = Mathf.Clamp(value, 0, 2);
        transform.localScale = new Vector3(2 - _stretch, _stretch, 2 - _stretch);
    }
}