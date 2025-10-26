using System;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelService : IDisposable
{
    private DiContainer _container;
    private LoadingWindowModel _loadingWindowModel;
    private SignalBus _signalBus;

    private BaseMonoLevel _baseMonoLevel;

    private string _targetScene;
    private string _currentLevelName = string.Empty;
    private int _checkPoint = 0;

    public Inputer CurrentAvatarController => _baseMonoLevel.GetInputer;

    public LevelService(
        DiContainer container,
        LoadingWindowModel loadingWindowModel,
        SignalBus signalBus)
    {
        _container = container;
        _signalBus = signalBus;
        _loadingWindowModel = loadingWindowModel;

        _loadingWindowModel.Showed += LoadingWindowShowed;
        _signalBus.Subscribe<ChangeSceneSignal>(LoadLevel);
    }

    public void SetLevel(BaseMonoLevel baseMonoLevel)
    {
        if (_baseMonoLevel != null)
        {
            _baseMonoLevel.ChangeCheckpoint -= UpdateCheckPoint;
            _baseMonoLevel.ChangeLevel -= LoadLevel;
        }
        _baseMonoLevel = baseMonoLevel;
        _baseMonoLevel.Init();
        _baseMonoLevel.StartLevel(_checkPoint);
        _baseMonoLevel.ChangeCheckpoint += UpdateCheckPoint;
        _baseMonoLevel.ChangeLevel += LoadLevel;
    }

    public void RunByCheckpoint()
    {
        _baseMonoLevel.StartLevel(_checkPoint);
    }

    private void UpdateCheckPoint(int checkPoint)
    {
        _checkPoint = checkPoint;
    }

    private void LoadLevel(ChangeSceneSignal sceneSignal)
    {
        LoadLevel(sceneSignal.SceneName);
    }

    private void LoadLevel(string sceneName)
    {
        if (sceneName != _currentLevelName)
            UpdateCheckPoint(0);
        _targetScene = sceneName;
        _signalBus.Fire(new AppStateSignal(_container.Resolve<LoadingState>()));
    }

    private void LoadingWindowShowed()
    {
        SceneManager.LoadScene(_targetScene);
    }

    public void Dispose()
    {
        _loadingWindowModel.Showed -= LoadingWindowShowed;
        _signalBus.Unsubscribe<ChangeSceneSignal>(LoadLevel);
    }
}