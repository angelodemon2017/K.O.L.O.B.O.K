using Beautify.Universal;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

[RequireComponent(typeof(Volume))]
public class PostEffectController : MonoBehaviour
{
    [SerializeField] private Volume _volume;
    [SerializeField] private BeautifySettings _beautifySettings;

    private ChromaticAberration _chromaticAberration;
    private Beautify.Universal.Beautify _beautify;
    private Dictionary<(string, string), IEffectContainer> _mapComps = new();

    private SignalBus _signalBus;

    private void OnValidate()
    {
        _volume = GetComponent<Volume>();
        _beautifySettings = GetComponent<BeautifySettings>();
    }

    [Inject]
    private void Construct(
        SignalBus signalBus)
    {
        _signalBus = signalBus;

        InitComponents();
        _signalBus.Subscribe<CinemaEffectSignal>(Handle);
    }

    private void InitComponents()
    {
        _volume.profile.TryGet(out _beautify);
        _mapComps.Add((nameof(_beautify), nameof(_beautify.frameBandVerticalSize)),
            new EffectContainer(_beautify.frameBandVerticalSize, 1f));
    }

    private void Handle(CinemaEffectSignal cinemaEffect)
    {
        PlayCinemaEffect(cinemaEffect.IsOn);
    }

    private void PlayCinemaEffect(bool isOn)
    {
        if (_mapComps.TryGetValue((nameof(_beautify), nameof(_beautify.frameBandVerticalSize)),
            out IEffectContainer effect))
        {
            effect.SetTarget(isOn ? 0.2f : 0f);
        }
    }

    private void OffAllEffects()
    {
        foreach (var ef in _mapComps)
        {
            ef.Value.TargetDefValue();
        }
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<CinemaEffectSignal>(Handle);
    }

    private class EffectContainer : IEffectContainer
    {
        private VolumeParameter<float> _parameter;
        private Tween _tween;
        private float _durationChanging;
        private float _defValue;

        public EffectContainer(VolumeParameter<float> volumeParameter,
            float duration = 1f, float defVal = 0f)
        {
            _parameter = volumeParameter;
            _durationChanging = duration;
            _defValue = defVal;
        }

        public void SetValue(float value)
        {
            _parameter.value = value;
//            _parameter.Override(value);
        }

        public void SetTarget(float target)
        {
            _tween?.Kill();
            var curVal = _parameter.value;
            var duration = Mathf.Abs(curVal - target) * _durationChanging;
            _tween = DOTween.To(
                () => curVal,
                x => _parameter.Override(x),
                target,
                duration)
                .SetEase(Ease.InOutSine);
        }

        public void TargetDefValue()
        {
            SetTarget(_defValue);
        }
    }

    private interface IEffectContainer
    {
        void SetValue(float value);
        void SetTarget(float target);
        void TargetDefValue();
    }
}