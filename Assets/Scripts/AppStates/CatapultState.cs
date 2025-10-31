using Zenject;

public class CatapultState : AppStateWithUIBase<GameplayWindow, GameplayWindowModel>
{
    public CatapultState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService)
    {

    }
}