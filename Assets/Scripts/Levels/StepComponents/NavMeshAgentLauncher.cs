using UnityEngine;

public class NavMeshAgentLauncher : StepComponentBase
{
    [SerializeField] private BaseCharacterController _character;
    [SerializeField] private Transform _target;

    public override void Execute()
    {
        _character.SetTarget(_target);
    }
}