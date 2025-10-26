using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public abstract class BaseMonoLevel : MonoBehaviour
{
    [SerializeField] private List<ActionsContainer> _cashContainers;
    [SerializeField] protected PlayableDirector _standingDirector;
    public SimpleBallController simpleBallController;
    private Dictionary<int, ActionsContainer> _mapContainers = new();

    protected ActionsContainer _currentMonoStateBase;

    protected abstract string SceneByEnd { get; }
    public virtual Inputer GetInputer => simpleBallController;

    public Action<int> ChangeCheckpoint;

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
        SceneManager.LoadScene(SceneByEnd);
    }
}