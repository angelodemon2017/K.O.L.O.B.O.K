using System;
using UnityEngine;
using UnityEngine.UI;

public class CinematicWindow : UIWindowBase<CinematicWindowModel>
{
    public override void Show()
    {
        base.Show();
        _model.Showed?.Invoke();
    }

    protected override void CallCallBacksAfterShowed() { }
}