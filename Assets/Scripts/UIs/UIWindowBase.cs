using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class UIWindowBase<T> : MonoBehaviour, IWindowBase where T : WindowModelBase
{
    [SerializeField] protected CanvasGroup _canvasGroup;

    [Inject] protected T _model;

    public Transform GetTransform => transform;

    private void OnValidate()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.DOFade(1f, 1f)
            .OnComplete(() => CallCallBacksAfterShowed());
    }

    protected virtual void CallCallBacksAfterShowed()
    {
        _model.Showed?.Invoke();
    }

    public virtual void Hide()
    {
        _canvasGroup.DOFade(0f, 1f)
            .OnComplete(() => gameObject.SetActive(false));
    }
}