using Zenject;

public class DialogState : AppStateWithUIBase<DialogWindow, DialogWindowModel>
{
    public DialogState(
        SignalBus signalBus,
        InputService inputService)
        : base(signalBus, inputService) { }

    protected override void EnterAppState()
    {
        base.EnterAppState();
        _uIWindow.Apply(_model.Step);
    }

    protected override void InputSubscribe()
    {
        base.InputSubscribe();
        _inputService.EscapeAction += Skip;
        _inputService.SpaceAction += Continue;
    }

    protected override void SubscribeUICallBacks()
    {
        base.SubscribeUICallBacks();
        _uIWindow.OnSkip += Skip;
        _uIWindow.OnContinue += Continue;
    }

    private void Skip()
    {
        _model.SkipDialog();
    }

    private void Continue()
    {
        if (_model.NextDialogStep())
        {
            _uIWindow.Apply(_model.Step);
        }
        else
        {
            Skip();
        }
    }

    protected override void UnSubscribeUICallBacks()
    {
        base.UnSubscribeUICallBacks();
        _uIWindow.OnSkip -= Skip;
        _uIWindow.OnContinue -= Continue;
    }

    protected override void InputUnsubscribe()
    {
        base.InputUnsubscribe();
        _inputService.EscapeAction -= Skip;
        _inputService.SpaceAction -= Continue;
    }

    protected override void ExitAppState()
    {
        base.ExitAppState();
    }
}