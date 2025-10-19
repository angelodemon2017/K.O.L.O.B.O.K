using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleBallController : MonoBehaviour, IFSMRoot, Inputer
{
    [SerializeField] private PhysicMovementController _movementController;
    [SerializeField] private GravityController _gravityController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float mouseSensitivityX;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _speed;
    [SerializeField] private BaseState _initialState;
    [SerializeField] private Rigidbody _rb;

    private Dictionary<Type, IState> _cashStates = new Dictionary<Type, IState>();
    private IState _currentState;
    private string _currentStateName;
    private float _smoothTime = 0.1f;
    private float _currentMouseDeltaVelocity;
    private float _yRotation = 0f;

//    [HideInInspector] 
    public Vector2 Axises;

    public Action OnStartPower;
    public Action OnFinishPower;

    public GravityController GravityController => _gravityController;
    public PhysicMovementController MovementController => _movementController;
    public IState CurrentState => _currentState;

    private void Awake()
    {
        _initialState.Init(this);
        _currentState = _initialState;
        ChangeState(_initialState);
        _gravityController.Init();
        cameraTransform.parent = null;
    }

    private void Update()
    {
        _gravityController.Update();
        RunByFSM();
    }

    private void RunByFSM()
    {
        _currentState?.Run();
        foreach (var avs in _currentState.AvailableState)
        {
            if (GetState(avs).CheckCondition())
            {
                ChangeState(avs);
                break;
            }
        }
    }

    public void TransAction(bool? space, bool? force)
    {
        foreach (var avs in _currentState.AvailableState)
        {
            if (space == true && GetState(avs).AvailableBySpace)
            {
                ChangeState(avs);
                break;
            }
            if (force == true && GetState(avs).AvailableByForce)
            {
                ChangeState(avs);
                break;
            }
        }
        _currentState.TransAction(space, force);
    }

    public void HandleMoving(Vector2 vector2)
    {
        Axises = vector2;
    }

    public void HandleMouseLook(Vector2 vector2)
    {
        float mouseX = vector2.x * mouseSensitivityX * Time.deltaTime;
        _yRotation = Mathf.SmoothDamp(_yRotation, mouseX, ref _currentMouseDeltaVelocity, _smoothTime);
        cameraTransform.rotation *= Quaternion.Euler(0, _yRotation * rotationSpeed, 0);
    }

    public void SetKinematic(bool isTrue)
    {
        _rb.isKinematic = isTrue;
    }

    public void ChangeState(IState state)
    {
        _currentState?.ExitState();
        _currentState = GetState(state);
        _currentStateName = _currentState.GetStateType.Name;
        _currentState.EnterState();
    }

    public IState GetState<T>(T state) where T : IState
    {
        var type = state.GetType();
        if (!_cashStates.TryGetValue(type, out var result))
        {
            BaseState nextState = (BaseState)_currentState.AvailableState.FirstOrDefault(s => s.GetStateType == type);
            var newState = Instantiate(nextState);
            newState.Init(this);
            _cashStates.Add(type, newState);
        }

        return _cashStates[type];
    }

    void FixedUpdate()
    {
        Speedometr();
    }

    private void OnDisable()
    {
        try
        {
            cameraTransform?.gameObject?.SetActive(false);
        }
        catch { }
    }

    private void OnEnable()
    {
        cameraTransform.gameObject.SetActive(true);
    }

    private void Speedometr()
    {
        _velocity = _rb.linearVelocity;
        _speed = _velocity.magnitude;
    }
}
