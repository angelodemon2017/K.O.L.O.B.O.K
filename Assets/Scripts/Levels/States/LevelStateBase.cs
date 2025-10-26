using System.Collections.Generic;
using UnityEngine;
using Zenject;

[System.Serializable]
public abstract class LevelStateBase
{
    [SerializeField] private GameObject _stepsContainer;

    protected DiContainer _diContainer;
    protected SignalBus _signalBus;

    public virtual void Enter(
        DiContainer diContainer,
        SignalBus signalBus)
    {
        _diContainer = diContainer;
        _signalBus = signalBus;
    }

    public virtual void Run()
    {

    }

    public virtual void TriggerActivate(int id)
    {

    }

    public virtual void TriggerActivate(PlayerTrigger playerTrigger)
    {

    }

    public virtual void Exit()
    {

    }
}