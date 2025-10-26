using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionsContainer : MonoBehaviour
{
    public bool IsCheckPoint;
    public int Id;
    [SerializeField] private List<StepComponentBase> _stepByState;

    private void OnValidate()
    {
        StepsValidate();
    }

    private void StepsValidate()
    {
        _stepByState.Clear();
        foreach (var item in GetComponents<StepComponentBase>())
        {
            _stepByState.Add(item);
        }
    }

    public void ExecuteSteps()
    {
        _stepByState.ForEach(step => step.Execute());
    }
}