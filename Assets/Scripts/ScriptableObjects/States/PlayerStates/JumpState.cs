using UnityEngine;

[CreateAssetMenu(menuName = "FSM/PlayerState/JumpState", order = 1)]
public class JumpState : BaseState
{
    private SimpleBallController _simpleBallController;
    private GravityController _gravityController;
    private PhysicMovementController _movementController;

    public override bool AvailableBySpace => true;

    public override void Init(IFSMRoot fSMRoot)
    {
        base.Init(fSMRoot);
        _simpleBallController = fSMRoot as SimpleBallController;
        _gravityController = _simpleBallController.GravityController;
        _movementController = _simpleBallController.MovementController;
    }

    public override void EnterState()
    {
        _gravityController.Jump();
    }

    public override void TransAction(bool? space, bool? force)
    {
        if (space == true && _gravityController.CanJump)
        {
            _gravityController.Jump();
        }
    }

    public override void Run()
    {
        if (_gravityController.IsGrounded)
        {
            _isFinish = true;
        }
        _movementController.Move(_simpleBallController.Axises * 0.3f);
    }

    public override bool CheckCondition()
    {
        return false;
    }
}