using Zenject;

public class FailState : AppStateWithUIBase<FailWindow, FailWindowModel>
{
    public FailState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService)
    {

    }
}