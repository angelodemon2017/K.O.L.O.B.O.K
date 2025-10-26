using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Volume))]
public class PostProcessHandler : MonoBehaviour
{
    [SerializeField] private Volume _volume;

    private UnityEngine.Rendering.Universal.Vignette _vignette;
    private UnityEngine.Rendering.Universal.LensDistortion _lensDistortion;

    private void OnValidate()
    {
        _volume = GetComponent<Volume>();
    }

    private void Awake()
    {
        InitComponents();
    }

    private void InitComponents()
    {
        if (_volume.profile.TryGet(out _lensDistortion))
        {
            SetLensDistor(0);
        }
    }

    public void SetLensDistor(float _intes)
    {
        _lensDistortion.intensity.Override(_intes);
    }

    #region effects

    public void PlayLens()
    {
        DOTween.To(() => 1, x => SetLensDistor(x), 0, 5f);
    }

    #endregion
}