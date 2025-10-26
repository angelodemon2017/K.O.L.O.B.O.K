using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class DialogLauncher : StepComponentBase
{
    [SerializeField] private BaseDialogConfig _dialog;
    [SerializeField] private UnityEvent _callBackAfterEnd;

    [Inject] private GameStateMachineService _gameStateMachine;
    [Inject] private DialogWindowModel _dialogWindowModel;

    public override void Execute()
    {
        _dialogWindowModel.SetDialogEntity(_dialog, CallBack);
    }

    private void CallBack()
    {
        _callBackAfterEnd?.Invoke();
    }
}