using System;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelService : IDisposable
{
    private LoadingWindowModel _loadingWindowModel;
    private SignalBus _signalBus;

    private BaseMonoLevel _baseMonoLevel;

    private string _targetScene;
    private string _currentLevelName = string.Empty;
    private int _checkPoint = 0;

    public Inputer CurrentAvatarController => _baseMonoLevel.GetInputer;

    public LevelService(
        LoadingWindowModel loadingWindowModel,
        SignalBus signalBus)
    {
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
        }
        _baseMonoLevel = baseMonoLevel;
        _baseMonoLevel.StartLevel(_checkPoint);
        _baseMonoLevel.ChangeCheckpoint += UpdateCheckPoint;
    }

    private void UpdateCheckPoint(int checkPoint)
    {
        _checkPoint = checkPoint;
    }

    private void LoadLevel(ChangeSceneSignal sceneSignal)
    {
        if (sceneSignal.SceneName != _currentLevelName)
            UpdateCheckPoint(0);
        _targetScene = sceneSignal.SceneName;
        _baseMonoLevel?.EndLevel();
        _signalBus.Fire(new AppLoadingSignal());
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