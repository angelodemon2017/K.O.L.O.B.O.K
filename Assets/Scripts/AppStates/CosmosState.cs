using Zenject;

public class CosmosState : AppStateWithUIBase<GameplayWindow, GameplayWindowModel>
{
    [Inject] private LevelService _levelService;

    private Inputer _inputer => _levelService.CurrentAvatarController;
    public CosmosState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService)
    {

    }

    protected override void EnterAppState()
    {
        base.EnterAppState();
        _inputer.SetKinematic(true);
    }

    public override void Run()
    {
        _inputer.HandleMoving(_inputService.Axises);
    }
}