using UnityEngine;

[CreateAssetMenu(menuName = "FSM/PlayerState/IdleState", order = 1)]
public class IdleState : BaseState
{
    public override bool CheckCondition()
    {
        return Root.CurrentState.IsFinished();
    }
}