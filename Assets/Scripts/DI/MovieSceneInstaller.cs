using UnityEngine;
using Zenject;

public class MovieSceneInstaller : MonoInstaller
{
    [SerializeField] private BaseMonoLevel _baseMonoLevel;
    
    public override void InstallBindings()
    {
        var ls = Container.Resolve<LevelService>();
        ls.SetLevel(_baseMonoLevel);

        var sb = Container.Resolve<SignalBus>();
        sb.Fire(new AppStateSignal(Container.Resolve<CinematicState>()));
    }
}