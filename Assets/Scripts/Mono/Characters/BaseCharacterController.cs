using UnityEngine;
using UnityEngine.AI;

public class BaseCharacterController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _target;
    [SerializeField] private float _targetDistance;

    private void FixedUpdate()
    {
        Moving();
    }

    private void Moving()
    {
        if (_target)
        {
            if (Vector3.Distance(transform.position, _target.position) < _targetDistance)
            {
                _target = null;
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
        _agent.SetDestination(_target.position);
    }
}