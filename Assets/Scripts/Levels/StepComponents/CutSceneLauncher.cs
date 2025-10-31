using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using Zenject;

public class CutSceneLauncher : StepComponentBase
{
    [SerializeField] protected PlayableDirector _standingDirector;
    [SerializeField] private PlayableAsset _clip;
    [SerializeField] private UnityEvent _callBackAfterStoppedCutScene;

    [Inject] private CinematicWindowModel _cinematicWindowModel;

    public override void Execute()
    {
        _cinematicWindowModel.Showed += RunCutScene;
        _standingDirector.stopped += StandingDirector_stopped;
    }

    private void StandingDirector_stopped(PlayableDirector obj)
    {
        _callBackAfterStoppedCutScene?.Invoke();
        _standingDirector.stopped -= StandingDirector_stopped;
    }

    private void RunCutScene()
    {
        _cinematicWindowModel.Showed -= RunCutScene;
        _standingDirector.Play(_clip);
    }
}