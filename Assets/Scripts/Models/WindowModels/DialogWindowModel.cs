using System;

public class DialogWindowModel : WindowModelBase
{
    private int _currentDialogStep;
    private DialogEntity _dialogEntity = new();

    public DialogStepEntity Step => _dialogEntity.GetDialogStep(_currentDialogStep);

    public Action ActionAfterEnd;
    public Action OnNext;
    public Action OnSkip;

    public void SetDialogEntity(BaseDialogConfig dialogEntity, Action callBackAfterEndOfDialog)
    {
        _currentDialogStep = 0;
        _dialogEntity.SetDialog(dialogEntity);
        ActionAfterEnd = callBackAfterEndOfDialog;
    }

    public bool NextDialogStep()
    {
        _currentDialogStep++;
        OnNext?.Invoke();
        return !_dialogEntity.IsEndStep(_currentDialogStep);
    }

    public void SkipDialog()
    {
        ActionAfterEnd?.Invoke();
        OnSkip?.Invoke();
    }
}