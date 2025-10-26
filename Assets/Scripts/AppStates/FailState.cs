using Zenject;

public class FailState : AppStateWithUIBase<FailWindow, FailWindowModel>
{
    private LevelService _levelService;

    public FailState(
        LevelService levelService,
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService)
    {
        _levelService = levelService;
    }

    protected override void SubscribeUICallBacks()
    {
        base.SubscribeUICallBacks();
        _model.OnRepeat += Repeat;
    }

    private void Repeat()
    {
        _levelService.RunByCheckpoint();
    }

    protected override void UnSubscribeUICallBacks()
    {
        base.UnSubscribeUICallBacks();
        _model.OnRepeat -= Repeat;
    }
}