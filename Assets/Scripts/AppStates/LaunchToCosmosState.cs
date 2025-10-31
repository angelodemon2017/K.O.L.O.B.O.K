using UnityEngine;
using Zenject;

public class LaunchToCosmosState : AppStateWithUIBase<LaunchToCosmosWindow, LaunchToCosmosWindowModel>
{
    protected override bool _cursorIsAvaiable => false;

    public LaunchToCosmosState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService)
    {

    }

    protected override void EnterAppState()
    {
        base.EnterAppState();
        _uIWindow.ApplyPower(_model.GetPercent);
    }

    protected override void InputSubscribe()
    {
        _inputService.SpaceAction += OnTap;
    }

    private void OnTap()
    {
        _model.PowerToLaunch += _model.PowerByTap;
        _uIWindow.ApplyPower(_model.GetPercent);
    }

    public override void Run()
    {
        if (_model.PowerToLaunch >= _model.PowerFromLaunch)
            _model.CallBackAfterPowerToLaunch?.Invoke();

        if (_model.PowerToLaunch > 0)
        {
            _model.PowerToLaunch -= Time.deltaTime * 10;
            _uIWindow.ApplyPower(_model.GetPercent);
        }
    }

    protected override void InputUnsubscribe()
    {
        _inputService.SpaceAction += OnTap;
    }
}