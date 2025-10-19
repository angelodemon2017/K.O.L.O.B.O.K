using UnityEngine;
using Zenject;

public class LaunchToCosmosState : AppStateWithUIBase<LaunchToCosmosWindow, LaunchToCosmosWindowModel>
{
    [Inject] private LaunchToCosmosWindowModel _launchToCosmosWindowModel;

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
        _uIWindow.ApplyPower(_launchToCosmosWindowModel.GetPercent);
    }

    protected override void InputSubscribe()
    {
        _inputService.SpaceAction += OnTap;
    }

    private void OnTap()
    {
        _launchToCosmosWindowModel.PowerToLaunch += _launchToCosmosWindowModel.PowerByTap;
        _uIWindow.ApplyPower(_launchToCosmosWindowModel.GetPercent);
    }

    public override void Run()
    {
        if (_launchToCosmosWindowModel.PowerToLaunch >= _launchToCosmosWindowModel.PowerFromLaunch)
            _signalBus.Fire(new AppCosmosSignal());

        if (_launchToCosmosWindowModel.PowerToLaunch > 0)
        {
            _launchToCosmosWindowModel.PowerToLaunch -= Time.deltaTime * 10;
            _uIWindow.ApplyPower(_launchToCosmosWindowModel.GetPercent);
        }
    }

    protected override void InputUnsubscribe()
    {
        _inputService.SpaceAction += OnTap;
    }
}