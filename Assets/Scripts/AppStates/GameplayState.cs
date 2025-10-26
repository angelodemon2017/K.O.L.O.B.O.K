using Zenject;

public class GameplayState : AppStateWithUIBase<GameplayWindow, GameplayWindowModel>
{
    [Inject] private LevelService _levelService;

    private Inputer _inputer => _levelService.CurrentAvatarController;
    protected override bool _cursorIsAvaiable => false;

    public GameplayState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService) { }

    protected override void EnterAppState()
    {
        base.EnterAppState();
        _inputer.SetKinematic(false);
    }

    protected override void InputSubscribe()
    {
        _inputService.EscapeAction += OnPauseButtonClick;
        _inputService.SpaceAction += OnSpace;
        _inputService.ForceAction += OnForce;
    }

    private void OnPauseButtonClick()
    {
        _signalBus.Fire(new AppStateSignal(_diContainer.Resolve<PauseMenuState>()));
    }

    private void OnSpace()
    {
        _inputer.TransAction(true, false);
    }

    private void OnForce()
    {
        _inputer.TransAction(false, true);
    }

    public override void Run()
    {
        _inputer?.HandleMoving(_inputService.Axises);
        _inputer?.HandleMouseLook(_inputService.MouseAxises);
    }

    protected override void InputUnsubscribe()
    {
        _inputService.EscapeAction -= OnPauseButtonClick;
        _inputService.SpaceAction -= OnSpace;
        _inputService.ForceAction -= OnForce;
    }

    protected override void ExitAppState()
    {
        base.ExitAppState();
        _inputer.SetKinematic(true);
    }
}