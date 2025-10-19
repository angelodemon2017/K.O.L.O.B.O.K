using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaunchToCosmosWindow : UIWindowBase<LaunchToCosmosWindowModel>
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [SerializeField] private Image _powerImageLeft;
    [SerializeField] private Image _powerImageRight;

    public void ApplyPower(float perc)
    {
        _powerImageLeft.fillAmount = perc;
        _powerImageRight.fillAmount = perc;
    }
}