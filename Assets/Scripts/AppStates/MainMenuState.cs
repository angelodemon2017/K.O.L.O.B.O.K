using Zenject;

public class MainMenuState : AppStateWithUIBase<MainMenuWindow, MainMenuWindowModel>
{
    public MainMenuState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService)
    {


    }
}