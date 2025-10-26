using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class AppStateSwitcher : StepComponentBase
{
    [SerializeField] private EAppStates _appState;
    [SerializeField] private UnityEvent _callBackAfterEndState;

    [Inject] private GameStateMachineService _gameStateMachine;
    [Inject] private LaunchToCosmosWindowModel _launchToCosmosWindowModel;
    [Inject] private CinematicWindowModel _cinematicWindowModel;
    
    public override void Execute()
    {
        switch (_appState)
        {
            case EAppStates.GameplayState:
                _gameStateMachine.EnterState<GameplayState>();
                break;
            case EAppStates.DialogState:
                _gameStateMachine.EnterState<DialogState>();
                break;
            case EAppStates.Cinema:
                _cinematicWindowModel.CallBackAfterEnd = CallBack;
                _gameStateMachine.EnterState<CinematicState>();
                break;
            case EAppStates.LaunchToCosmosState:
                _launchToCosmosWindowModel.CallBackAfterPowerToLaunch = CallBack;
                _gameStateMachine.EnterState<LaunchToCosmosState>();
                break;
            case EAppStates.CosmosState:
                _gameStateMachine.EnterState<CosmosState>();
                break;
        }
    }

    private void CallBack()
    {
        _callBackAfterEndState?.Invoke();
    }

    public enum EAppStates
    {
        GameplayState = 1,
        DialogState = 2,
        Cinema = 3,
        LaunchToCosmosState = 10,
        CosmosState = 11,
    }
}