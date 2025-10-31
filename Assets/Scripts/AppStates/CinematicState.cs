using Zenject;

public class CinematicState : AppStateWithUIBase<CinematicWindow, CinematicWindowModel>
{
    public CinematicState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService)
    {

    }

    protected override void EnterAppState()
    {
        base.EnterAppState();
        _signalBus.Fire(new CinemaEffectSignal(true));
    }

    protected override void ExitAppState()
    {
        _signalBus.Fire(new CinemaEffectSignal(false));
        base.ExitAppState();
    }
}