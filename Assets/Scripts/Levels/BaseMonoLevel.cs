using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public abstract class BaseMonoLevel : MonoBehaviour
{
    [SerializeField] private List<ActionsContainer> _cashContainers;
    public SimpleBallController simpleBallController;
    private Dictionary<int, ActionsContainer> _mapContainers = new();
    [SerializeField] private SceneAsset _nextLevel;

    protected ActionsContainer _currentMonoStateBase;

    public virtual Inputer GetInputer => simpleBallController;

    public Action<int> ChangeCheckpoint;
    public Action<string> ChangeLevel;

    private void OnValidate()
    {
        _cashContainers.Clear();
        var _tempContainers = GetComponentsInChildren<ActionsContainer>();
        for (var i = 0; i < _tempContainers.Count(); i++)
        {
            _tempContainers[i].Id = i;
            _cashContainers.Add(_tempContainers[i]);
        }
    }

    public void Init()
    {
        _mapContainers.Clear();
        _cashContainers.ForEach(c => _mapContainers.Add(c.Id, c));
    }

    public virtual void StartLevel(int checkPoint = 0)
    {
        RunContainer(_mapContainers[checkPoint]);
    }

    public void RunContainer(ActionsContainer stepComponent)
    {
        if (stepComponent.IsCheckPoint)
        {
            ChangeCheckpoint?.Invoke(stepComponent.Id);
        }
        stepComponent.ExecuteSteps();
    }

    public virtual void EndLevel()
    {
        ChangeLevel?.Invoke(_nextLevel.name);
    }
}