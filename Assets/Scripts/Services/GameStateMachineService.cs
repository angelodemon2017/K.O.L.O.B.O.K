using System;
using Zenject;

public class GameStateMachineService<T> : IDisposable, ITickable
    where T : IAppState
{
    private SignalBus _signalBus;
    private DiContainer _container;
    private IAppState _currentState;

    public GameStateMachineService(
        SignalBus signalBus,
        DiContainer container)
    {
        _signalBus = signalBus;
        _container = container;

        _signalBus.Subscribe<AppStateSignal>(SetStateSignal);
    }

    private void SetStateSignal(AppStateSignal signal)
    {
        EnterState(signal.State);
    }

    private void EnterState(IAppState appState)
    {
        _currentState?.Exit();
        _currentState = appState;
        _currentState.Enter();
    }

    private void EnterState<T2>() where T2 : IAppState
    {
        _currentState?.Exit();
        _currentState = _container.Resolve<T2>();
        _currentState.Enter();
    }

    public void Tick()
    {
        _currentState?.Run();
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<AppStateSignal>(SetStateSignal);
    }
}