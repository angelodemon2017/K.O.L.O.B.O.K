using UnityEngine;

public class SmoothPositionFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _lerpSpeed = 10f;
    [SerializeField] private bool _useFixedUpdate = false;

    private void Update()
    {
        if (_useFixedUpdate)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _lerpSpeed);
    }

    private void FixedUpdate()
    {
        if (!_useFixedUpdate)
        {
            return;
        }
        transform.position = Vector3.Lerp(transform.position, _target.position, Time.fixedDeltaTime * _lerpSpeed);
    }
}