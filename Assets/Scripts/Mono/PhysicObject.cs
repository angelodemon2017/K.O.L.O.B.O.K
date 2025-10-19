using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicObject : MonoBehaviour
{
    [SerializeField] private float _mass = 1f;
    [SerializeField] private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        ApplyProps();
    }

    public void SetMass(float mass)
    {
        _mass = mass;
        ApplyProps();
    }

    private void ApplyProps()
    {
        _rb.mass = _mass;
        transform.localScale = Vector3.one * _mass;
    }
}