using System;
using UnityEngine;

[Serializable]
public class GravityController
{
    [SerializeField] private ScalerRoot _scalerRoot;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;

    [SerializeField] private LayerMask _targetObjects;
    [SerializeField] private float _speedForceDown;
    [SerializeField] private float _explodeForce = 10f;
    [SerializeField] private float _explodeRadius = 5f;
    [SerializeField] private float _explodeUpForce = 1f;
    [SerializeField] private float _verticalCenterExplode;
    [SerializeField] private Rigidbody _rb;

    private Transform _cashTransform;
    private float _startingMass;
    private bool _isGrounded;
    private int _bonusJumpMax = 1;
    private int _bonusJump = 0;
    private float _notCheckingGround = 0;
    private bool _isForcingDown = false;

    public bool CanJump => _isGrounded || _bonusJump < _bonusJumpMax;
    public bool IsGrounded => _isGrounded;

    public Action OnJumpFromGround;
    public Action<float> OnGroundConnect;
    public Action OnForceConnect;

    public void Init()
    {
        _startingMass = _rb.mass;
        _cashTransform = _rb.transform;
    }

    public void Update()
    {
        CheckGrounded();
        ForceFalling();
    }

    private void CheckGrounded()
    {
        if (_notCheckingGround > 0)
        {
            _notCheckingGround -= Time.deltaTime;
            return;
        }

        var _lastIsGrounded = _isGrounded;
        _isGrounded = Physics.Raycast(_cashTransform.position, Vector3.down, groundCheckDistance, groundLayer);
        if (!_isGrounded)
        {
            _rb.mass++;
        }

        if (_isGrounded && !_lastIsGrounded)
        {
            _rb.AddForce(Vector3.down * _speedForceDown * 0.1f, ForceMode.VelocityChange);
            _bonusJump = 0;
            if (_isForcingDown)
            {
                Explode();
                OnForceConnect?.Invoke();
                _scalerRoot.Down();
            }
            else
            {
                OnGroundConnect?.Invoke(_rb.linearVelocity.y);
                if (_rb.linearVelocity.y < -2f)
                {
                    _scalerRoot.Down();
                }
            }
            _isForcingDown = false;
            _rb.mass = _startingMass;
        }
    }

    private void Explode()
    {
        var overlapColliders = Physics.OverlapSphere(_cashTransform.position, _explodeRadius, _targetObjects);
        foreach (var collider in overlapColliders)
        {
            var rb = collider.attachedRigidbody;
            if (rb)
            {
                rb.AddExplosionForce(_explodeForce, _cashTransform.position - Vector3.up * _verticalCenterExplode, _explodeRadius, _explodeUpForce, ForceMode.Force);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="power">multipler gravity jump</param>
    public void Jump(float power = 1f)
    {
        _rb.mass = _startingMass;
        _rb.AddForce(Vector3.up * power * jumpForce, ForceMode.Impulse);
        if (_isGrounded)
        {
            _notCheckingGround = 0.1f;
            OnJumpFromGround?.Invoke();
        }
        else
        {
            _bonusJump++;
        }
        _isGrounded = false;
        _cashTransform.rotation = Quaternion.Euler(0, _cashTransform.rotation.eulerAngles.y, 0);
        _scalerRoot.Up();
    }

    public void ForceDown()
    {
        if (!_isGrounded && !_isForcingDown)
        {
            _rb.linearVelocity = Vector3.zero;
            _rb.AddForce(Vector3.down * _speedForceDown, ForceMode.Impulse);
            _isForcingDown = true;
        }
    }

    private void ForceFalling()
    {
        if (_isForcingDown)
        {
            _rb.AddForce(Vector3.down * _speedForceDown * 0.1f, ForceMode.VelocityChange);
            _rb.mass += 2;
        }
    }
}