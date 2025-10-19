using System;
using UnityEngine;
using Zenject;

public class GameStateMachineService : IDisposable, ITickable
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

        _signalBus.Subscribe<AppCinematicSignal>(EnterState<CinematicState>);
        _signalBus.Subscribe<AppCosmosSignal>(EnterState<CosmosState>);
        _signalBus.Subscribe<AppDialogSignal>(EnterState<DialogState>);
        _signalBus.Subscribe<AppFailSignal>(EnterState<FailState>);
        _signalBus.Subscribe<AppGameplaySignal>(EnterState<GameplayState>);
        _signalBus.Subscribe<AppLaunchToCosmosSignal>(EnterState<LaunchToCosmosState>);
        _signalBus.Subscribe<AppLoadingSignal>(EnterState<LoadingState>);
        _signalBus.Subscribe<AppMainMenuSignal>(EnterState<MainMenuState>);
        _signalBus.Subscribe<AppPauseSignal>(EnterState<PauseMenuState>);
    }

    private void EnterState<TState>() where TState : IAppState
    {
        _currentState?.Exit();
        _currentState = _container.Resolve<TState>();
        _currentState.Enter();
    }

    public void Tick()
    {
        _currentState?.Run();
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<AppCinematicSignal>(EnterState<CinematicState>);
        _signalBus.Unsubscribe<AppCosmosSignal>(EnterState<CosmosState>);
        _signalBus.Unsubscribe<AppDialogSignal>(EnterState<DialogState>);
        _signalBus.Unsubscribe<AppFailSignal>(EnterState<FailState>);
        _signalBus.Unsubscribe<AppGameplaySignal>(EnterState<GameplayState>);
        _signalBus.Unsubscribe<AppLaunchToCosmosSignal>(EnterState<LaunchToCosmosState>);
        _signalBus.Unsubscribe<AppLoadingSignal>(EnterState<LoadingState>);
        _signalBus.Unsubscribe<AppMainMenuSignal>(EnterState<MainMenuState>);
        _signalBus.Unsubscribe<AppPauseSignal>(EnterState<PauseMenuState>);
    }
}