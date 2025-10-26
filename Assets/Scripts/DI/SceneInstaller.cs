using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private BaseMonoLevel _baseMonoLevel;
    
    public override void InstallBindings()
    {
    }

    public override void Start()
    {
        base.Start();
        var ls = Container.Resolve<LevelService>();
        ls.SetLevel(_baseMonoLevel);
    }
}