using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [Header("MonoServices")]    
    [SerializeField] private PostEffectController _postEffectController;
    [SerializeField] private InputService _inputService;
    [SerializeField] private MusicService _musicService;

    [Header("Prefabs")]
    [SerializeField] private Canvas _canvasPrefab;
    [SerializeField] private CinematicWindow _cinematicWindow;
    [SerializeField] private DialogWindow _dialogWindow;
    [SerializeField] private DisclaimerWindow _disclaimerWindow;
    [SerializeField] private FailWindow _failWindow;
    [SerializeField] private GameplayWindow _gameplayWindow;
    [SerializeField] private LaunchToCosmosWindow _launchToCosmosWindow;
    [SerializeField] private LoadingWindow _loadingWindow;
    [SerializeField] private MainMenuWindow _mainMenuWindow;
    [SerializeField] private PauseWindow _pauseWindow;

    [Header("Configs")]
    [SerializeField] private CharactersConfig _charactersConfig;
    [SerializeField] private MusicConfig _musicConfig;

    public override void InstallBindings()
    {
        InstallSignals();
        InstallBrefabs();
        InstallConfigs();
        InstallModels();
        InstallSerives();
        InitMonoServices();
        InstallUI();
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
        Container.BindInstance(_musicConfig).AsSingle();
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
        Container.BindInterfacesAndSelfTo<WindowModelBase>().AsSingle();
    }

    private void InstallSerives()
    {
        Container.BindInterfacesAndSelfTo<LevelService>().AsSingle();
        Container.BindInterfacesAndSelfTo<UIService>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameStateMachineService>().AsSingle();
    }

    private void InitMonoServices()
    {
        Container.BindInstance(_postEffectController).AsSingle();
        Container.BindInstance(_inputService).AsSingle();
        Container.BindInstance(_musicService).AsSingle();
    }

    private void InstallUI()
    {
        Container.Bind<CinematicWindow>().FromComponentInNewPrefab(_cinematicWindow).AsSingle();
        Container.Bind<DialogWindow>().FromComponentInNewPrefab(_dialogWindow).AsSingle();
        Container.Bind<DisclaimerWindow>().FromComponentInNewPrefab(_disclaimerWindow).AsSingle();
        Container.Bind<FailWindow>().FromComponentInNewPrefab(_failWindow).AsSingle();
        Container.Bind<GameplayWindow>().FromComponentInNewPrefab(_gameplayWindow).AsSingle();
        Container.Bind<LaunchToCosmosWindow>().FromComponentInNewPrefab(_launchToCosmosWindow).AsSingle();
        Container.Bind<LoadingWindow>().FromComponentInNewPrefab(_loadingWindow).AsSingle();
        Container.Bind<MainMenuWindow>().FromComponentInNewPrefab(_mainMenuWindow).AsSingle();
        Container.Bind<PauseWindow>().FromComponentInNewPrefab(_pauseWindow).AsSingle();
    }

    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<ChangeSceneSignal>();
        Container.DeclareSignal<PlayClipSignal>();

        Container.DeclareSignal<DebugEffectSignal>();
        Container.DeclareSignal<CinemaEffectSignal>();

        Container.DeclareSignal<AppStateSignal>();
    }

    private void InstallAppStates()
    {
        Container.BindInterfacesAndSelfTo<CatapultState>().AsSingle();
        Container.BindInterfacesAndSelfTo<CinematicState>().AsSingle();
        Container.BindInterfacesAndSelfTo<CosmosState>().AsSingle();
        Container.BindInterfacesAndSelfTo<DialogState>().AsSingle();
        Container.BindInterfacesAndSelfTo<DisclaimerState>().AsSingle();
        Container.BindInterfacesAndSelfTo<FailState>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameplayState>().AsSingle();
        Container.BindInterfacesAndSelfTo<LaunchToCosmosState>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoadingState>().AsSingle();
        Container.BindInterfacesAndSelfTo<MainMenuState>().AsSingle();
        Container.BindInterfacesAndSelfTo<PauseMenuState>().AsSingle();
    }

    private void InstallFSMStates()
    {
    }
}