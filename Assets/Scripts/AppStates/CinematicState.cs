using Zenject;

public class CinematicState : AppStateWithUIBase<CinematicWindow, CinematicWindowModel>
{
    public CinematicState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService)
    {

    }
}