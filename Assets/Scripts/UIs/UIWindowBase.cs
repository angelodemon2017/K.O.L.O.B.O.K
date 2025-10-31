using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class UIWindowBase<T> : MonoBehaviour, IWindowBase where T : WindowModelBase
{
    [SerializeField] protected CanvasGroup _canvasGroup;

    private bool _calcOn = false;
    private bool _isShow = false;
    [Inject] protected T _model;

    public Transform GetTransform => transform;

    private void OnValidate()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Awake()
    {
        _canvasGroup.alpha = 0f;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
        _isShow = true;
        _calcOn = true;
        //_canvasGroup.DOFade(1f, 1f)
        //    .OnComplete(() => CallCallBacksAfterShowed());
    }

    protected virtual void Update()
    {
        CalcVisibility();
    }

    private void CalcVisibility()
    {
        if (!_calcOn) return;

        if (_isShow && _canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += Time.deltaTime;
            if (_canvasGroup.alpha >= 1f)
            {
                CallCallBacksAfterShowed();
                _calcOn = false;
            }
        }
        if (!_isShow && _canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= Time.deltaTime;
            if (_canvasGroup.alpha <= 0)
            {
                _calcOn = false;
                gameObject.SetActive(false);
            }
        }
    }

    protected virtual void CallCallBacksAfterShowed()
    {
        //Debug.LogError($"CallCallBacksAfterShowed-{gameObject.name}");
        _model.Showed?.Invoke();
    }

    public virtual void Hide()
    {
        _isShow = false;
        _calcOn = true;
        //_canvasGroup.DOFade(0f, 1f)
        //.OnComplete(() => gameObject.SetActive(false));
    }
}