using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public class CutSceneLauncher : StepComponentBase
{
    [SerializeField] protected PlayableDirector _standingDirector;
    [SerializeField] private PlayableAsset _clip;

    [Inject] private CinematicWindowModel _cinematicWindowModel;

    public override void Execute()
    {
        _cinematicWindowModel.Showed = RunCutScene;
    }

    private void RunCutScene()
    {
        _standingDirector.Play(_clip);
    }
}