using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [Header("MonoServices")]
    [SerializeField] private InputService _inputService;

    [Header("Prefabs")]
    [SerializeField] private Canvas _canvasPrefab;
    [SerializeField] private LaunchToCosmosWindow _launchToCosmosWindow;
    [SerializeField] private LoadingWindow _loadingWindow;
    [SerializeField] private PauseWindow _pauseWindow;
    [SerializeField] private MainMenuWindow _mainMenuWindow;
    [SerializeField] private GameplayWindow _gameplayWindow;
    [SerializeField] private DialogWindow _dialogWindow;
    [SerializeField] private FailWindow _failWindow;
    [SerializeField] private CinematicWindow _cinematicWindow;

    [Header("Configs")]
    [SerializeField] private CharactersConfig _charactersConfig;

    public override void InstallBindings()
    {
        InstallBrefabs();
        InstallConfigs();
        InstallModels();
        InstallSerives();
        InitMonoServices();
        InstallUI();
        InstallSignal();
        InstallAppStates();
        InstallFSMStates();
    }

    private void InstallBrefabs()
    {
        Container.BindInstance(_canvasPrefab).WithId(Dicts.DiPrefabIds.Canvas);
    }

    private void InstallConfigs()
    {
        Container.BindInstance(_charactersConfig).AsSingle();
    }

    private void InstallModels()
    {
        Container.BindInterfacesAndSelfTo<CinematicWindowModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<DialogWindowModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<FailWindowModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameplayWindowModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<LaunchToCosmosWindowModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoadingWindowModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<MainMenuWindowModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PauseWindowModel>().AsSingle();
    }

    private void InstallSerives()
    {
        Container.BindInterfacesAndSelfTo<LevelService>().AsSingle();
        Container.BindInterfacesAndSelfTo<UIService>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameStateMachineService>().AsSingle();
    }

    private void InitMonoServices()
    {
        Container.BindInstance(_inputService).AsSingle();
    }

    private void InstallUI()
    {
        Container.Bind<CinematicWindow>().FromComponentInNewPrefab(_cinematicWindow).AsSingle();
        Container.Bind<FailWindow>().FromComponentInNewPrefab(_failWindow).AsSingle();
        Container.Bind<LaunchToCosmosWindow>().FromComponentInNewPrefab(_launchToCosmosWindow).AsSingle();
        Container.Bind<LoadingWindow>().FromComponentInNewPrefab(_loadingWindow).AsSingle();
        Container.Bind<PauseWindow>().FromComponentInNewPrefab(_pauseWindow).AsSingle();
        Container.Bind<MainMenuWindow>().FromComponentInNewPrefab(_mainMenuWindow).AsSingle();
        Container.Bind<GameplayWindow>().FromComponentInNewPrefab(_gameplayWindow).AsSingle();
        Container.Bind<DialogWindow>().FromComponentInNewPrefab(_dialogWindow).AsSingle();
    }

    private void InstallSignal()
    {        
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<ChangeSceneSignal>();
        
        Container.DeclareSignal<AppCinematicSignal>();
        Container.DeclareSignal<AppCosmosSignal>();
        Container.DeclareSignal<AppDialogSignal>();
        Container.DeclareSignal<AppFailSignal>();
        Container.DeclareSignal<AppGameplaySignal>();
        Container.DeclareSignal<AppLaunchToCosmosSignal>();
        Container.DeclareSignal<AppLoadingSignal>();
        Container.DeclareSignal<AppMainMenuSignal>();
        Container.DeclareSignal<AppPauseSignal>();
    }

    private void InstallAppStates()
    {        
        Container.BindInterfacesAndSelfTo<FailState>().AsSingle();
        Container.BindInterfacesAndSelfTo<CinematicState>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoadingState>().AsSingle();
        Container.BindInterfacesAndSelfTo<LaunchToCosmosState>().AsSingle();
        Container.BindInterfacesAndSelfTo<CosmosState>().AsSingle();
        Container.BindInterfacesAndSelfTo<DialogState>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameplayState>().AsSingle();
        Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
        Container.BindInterfacesAndSelfTo<PauseMenuState>().AsSingle();
    }

    private void InstallFSMStates()
    {
    }
}