using UnityEngine;
using UnityEngine.Events;

public class UnityEventsStep : StepComponentBase
{
    [SerializeField] private UnityEvent _simpleActions;

    public override void Execute()
    {
        _simpleActions?.Invoke();
    }
}