using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaunchToCosmosWindow : UIWindowBase<LaunchToCosmosWindowModel>
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private Image _powerImageLeft;
    [SerializeField] private Image _powerImageRight;
    [SerializeField] private AnimationCurve _textPulse;
    [SerializeField] private float _timeOfPulse;

    private float _time;

    public void ApplyPower(float perc)
    {
        _powerImageLeft.fillAmount = perc;
        _powerImageRight.fillAmount = perc;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        _textMeshProUGUI.rectTransform.localScale = Vector3.one * (_textPulse.Evaluate(_time) + 1f);
        if (_time > _timeOfPulse)
            _time = 0f;
    }
}