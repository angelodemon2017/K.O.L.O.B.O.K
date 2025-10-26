using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CosmoController : MonoBehaviour, IFSMRoot, Inputer
{
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private string _currentStateName;
    [SerializeField] private BaseState _initialState;
    [SerializeField] private FlyState _flyState;
    [SerializeField] private Transform _model;
    [SerializeField] private Transform _camera;
    [SerializeField] private BaseState _stateByDeath;

    [Inject] private GameStateMachineService _gameStateMachine;
    private Dictionary<Type, IState> _cashStates = new Dictionary<Type, IState>();
    private IState _currentState;
    private float _xControl = 0f;

    public IState CurrentState => _currentState;
    public float XControl => _xControl;

    private void InitAndRun()
    {
        if (_currentState == null)
        {
            _initialState.Init(this);
            _currentState = _initialState;
            ChangeState(_initialState);
        }
        Run();
    }

    [ContextMenu("Run")]
    public void Run()
    {
        _model.gameObject.SetActive(true);

        ChangeState(_flyState);
    }

    private void Update()
    {
        RunByFSM();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Dicts.Tags.Enemy)
        {
            ChangeState(_stateByDeath);
            _gameStateMachine.EnterState<FailState>();
        }
    }

    public void HandleMoving(Vector2 vector2)
    {
        _xControl = vector2.x;
    }

    public void TransAction(bool? space, bool? force)
    {
    }

    public void HandleMouseLook(Vector2 vector2)
    {
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos">0 - 1</param>
    public void SetInterpol(float pos)
    {
        _model.position = Vector3.Lerp(_leftBorder.position, _rightBorder.position, pos);
        _camera.position = Vector3.Lerp(_leftBorder.position, _rightBorder.position, (pos / 2) + 0.25f);
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

    public void SetKinematic(bool isTrue)
    {
        if (isTrue)
        {
            InitAndRun();
        }
    }
}