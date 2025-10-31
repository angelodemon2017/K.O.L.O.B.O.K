using Zenject;
using UnityEngine;

public class WolfLevel : BaseMonoLevel
{
    [SerializeField] private CatapultController _catapultController;

    private MusicConfig _musicConfig;

    private bool _isCatapultMode;

    public override Inputer GetInputer =>
        _isCatapultMode ? _catapultController : base.GetInputer;

    [Inject]
    private void Constructor(
        MusicConfig musicConfig)
    {
        _musicConfig = musicConfig;
    }

    public void SwitchCatapult(bool isOn)
    {
        _isCatapultMode = isOn;
    }
}