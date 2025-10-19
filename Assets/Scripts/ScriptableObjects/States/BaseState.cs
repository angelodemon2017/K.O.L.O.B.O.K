using System;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public abstract class BaseState : ScriptableObject, IState
{
    [SerializeField] private List<BaseState> _availableState;
    private List<IState> _cashAvailableState;

    protected bool _isFinish = false;
    public IFSMRoot Root { get; private set; }

    public virtual bool AvailableBySpace => false;
    public virtual bool AvailableByForce => false;
    private Type _cashType;
    public Type GetStateType
    {
        get
        {
            if (_cashType == null)
            {
                _cashType = GetType();
            }
            return _cashType;
        }
    }
    public List<IState> AvailableState 
    {
        get 
        {
            if (_cashAvailableState == null || _cashAvailableState.Count == 0)
            {
                _cashAvailableState = _availableState.ConvertAll(state => state as IState);
            }
            return _cashAvailableState;
        }
    }

    public virtual void TransAction(bool? space, bool? force)
    {

    }

    public virtual void Init(IFSMRoot fSMRoot)  
    {
        Root = fSMRoot;
        _isFinish = false;
    }
    public abstract bool CheckCondition();
    public virtual void EnterState() { }
    public virtual void Run() { }
    public virtual void ExitState() 
    {
        _isFinish = false;
    }
    public virtual bool IsFinished()
    {
        return _isFinish;
    }
}