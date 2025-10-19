using UnityEngine;

[CreateAssetMenu(menuName = "FSM/PlayerState/RollingState", order = 1)]
public class RollingState : BaseState
{
    private SimpleBallController _simpleBallController;
    private PhysicMovementController _movementController;

    public override void Init(IFSMRoot fSMRoot)
    {
        base.Init(fSMRoot);
        _simpleBallController = (SimpleBallController)fSMRoot;
        _movementController = _simpleBallController.MovementController;
    }

    public override void Run()
    {
        _movementController.Move(_simpleBallController.Axises);
    }

    public override bool CheckCondition()
    {
        return _simpleBallController.Axises != Vector2.zero;
    }
}