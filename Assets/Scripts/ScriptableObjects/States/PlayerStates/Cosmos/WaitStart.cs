using UnityEngine;

[CreateAssetMenu(menuName = "FSM/PlayerState/Cosmos/WaitStart", order = 1)]
public class WaitStart : BaseState
{
    public override bool CheckCondition()
    {
        return Root.CurrentState.IsFinished();
    }
}