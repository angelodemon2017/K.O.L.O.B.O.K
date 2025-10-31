using Zenject;

public class CosmosState : AppStateWithUIBase<GameplayWindow, GameplayWindowModel>
{
    [Inject] private LevelService _levelService;
    [Inject] private PauseWindowModel _pauseWindowModel;

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

    protected override void InputSubscribe()
    {
        base.InputSubscribe();
        _inputService.EscapeAction += OnPauseBtnClick;
    }

    public override void Run()
    {
        _inputer.HandleMoving(_inputService.Axises);
    }

    private void OnPauseBtnClick()
    {
        _pauseWindowModel.ReturningState = this;
        _signalBus.Fire(new AppStateSignal(_diContainer.Resolve<PauseMenuState>()));
    }

    protected override void InputUnsubscribe()
    {
        base.InputUnsubscribe();
        _inputService.EscapeAction -= OnPauseBtnClick;
    }
}