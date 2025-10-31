using Zenject;

public class DisclaimerState : AppStateWithUIBase<DisclaimerWindow, WindowModelBase>
{
    public DisclaimerState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService)
    {

    }
}