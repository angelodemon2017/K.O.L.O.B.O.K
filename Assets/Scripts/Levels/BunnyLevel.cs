using Zenject;
using UnityEngine;

public class BunnyLevel : BaseMonoLevel
{
    [SerializeField] private CosmoController _cosmoController;

    private bool _isCosmos = false;
    private MusicConfig _musicConfig;

    protected override string SceneByEnd => Dicts.Scenes.Wolf;
    public override Inputer GetInputer =>
        _isCosmos ?
        _cosmoController :
        base.GetInputer;

    [Inject]
    private void Constructor(
        MusicConfig musicConfig)
    {
        _musicConfig = musicConfig;
    }

    public override void StartLevel(int checkPoint = 0)
    {
        base.StartLevel(checkPoint);
    }

    public void SwitchInCosmos(bool isOn)
    {
        _isCosmos = isOn;
    }
}