using UnityEngine;

[CreateAssetMenu(menuName = "FSM/PlayerState/ForceDownState", order = 1)]
public class ForceDownState : BaseState
{
    private GravityController _gravityController;

    public override bool AvailableByForce => true;

    public override void Init(IFSMRoot fSMRoot)
    {
        base.Init(fSMRoot);
        _gravityController = (fSMRoot as SimpleBallController).GravityController;
    }

    public override void EnterState()
    {
        _gravityController.ForceDown();
    }

    public override void Run()
    {
        if (_gravityController.IsGrounded)
        {
            _isFinish = true;
        }
    }

    public override bool CheckCondition()
    {
        return false;
    }
}