using Zenject;

public class PauseMenuState : AppStateWithUIBase<PauseWindow, PauseWindowModel>
{
    public PauseMenuState(
        SignalBus signalBus,
        InputService inputService)
         : base(signalBus, inputService)
    {

    }

    protected override void EnterAppState()
    {
        base.EnterAppState();
    }

    protected override void InputSubscribe()
    {
        _inputService.EscapeAction += OnContinue;
    }

    protected override void SubscribeUICallBacks()
    {
        _uIWindow.OnContinue += OnContinue;
    }

    private void OnContinue()
    {
        _signalBus.Fire(new AppGameplaySignal());
    }

    protected override void ExitAppState()
    {
        base.ExitAppState();
    }

    protected override void InputUnsubscribe()
    {
        _inputService.EscapeAction -= OnContinue;
    }

    protected override void UnSubscribeUICallBacks()
    {
        _uIWindow.OnContinue -= OnContinue;
    }
}