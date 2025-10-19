using Zenject;

public class CinematicState : AppStateWithUIBase<GameplayWindow, GameplayWindowModel>
{
    public CinematicState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService)
    {

    }
}