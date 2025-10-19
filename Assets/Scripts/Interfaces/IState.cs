using System;
using System.Collections.Generic;

public interface IState
{
    bool AvailableBySpace { get; }
    bool AvailableByForce { get; }
    List<IState> AvailableState { get; }
    Type GetStateType { get; }

    void TransAction(bool? space, bool? force);
    void EnterState();
    void Run();
    void ExitState();
    bool CheckCondition();
    bool IsFinished();
}