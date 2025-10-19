using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PeopleController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private poses _poses;
    [SerializeField] private float _radiusNextPoint;
    [SerializeField] private float _targetDistance;
    [SerializeField] private float _idleTime;

    private float _idleTimer = 0;

    private float GetRandomPoint => Random.Range(-_radiusNextPoint, _radiusNextPoint);

    private void OnValidate()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _poses = GetComponentInChildren<poses>();
    }

    private void Update()
    {
        RunBehaviour();
    }

    private void RunBehaviour()
    {
        if (_idleTimer > 0)
        {
            _idleTimer -= Time.deltaTime;
            if (_idleTimer <= 0f)
            {
                GoTo(SearchNewTarget());
                _poses.SetAndPlayPose(20);
            }
        }
        else if (Vector3.Distance(transform.position, _navMeshAgent.destination) < _targetDistance)
        {
            _idleTimer = _idleTime;
            GoTo(transform.position);
            _poses.SetAndPlayPose(Random.Range(1, 17));
        }
    }

    private Vector3 SearchNewTarget()
    {
        Vector3 swift = Vector3.zero;
        while (swift.magnitude < 1f && _radiusNextPoint > 1f)
        {
            swift = new Vector3(GetRandomPoint, 0, GetRandomPoint);
        }
        return transform.position + swift;
    }

    public void GoTo(Vector3 vector3)
    {
        _navMeshAgent.SetDestination(vector3);
    }
}