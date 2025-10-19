using Zenject;

public class LoadingState : AppStateWithUIBase<LoadingWindow, LoadingWindowModel>
{
    public LoadingState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService)
    {

    }
}