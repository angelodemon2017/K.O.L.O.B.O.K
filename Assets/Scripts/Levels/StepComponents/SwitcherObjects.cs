using System.Collections.Generic;
using UnityEngine;

public class SwitcherObjects : StepComponentBase
{
    [SerializeField] private List<SwitchTarget> switchTargets;

    public override void Execute()
    {
        switchTargets.ForEach(t => t.Execute());
    }
}

[System.Serializable]
public class SwitchTarget
{
    [SerializeField] private GameObject _target;
    [SerializeField] private bool _state;

    public void Execute()
    {
        _target.SetActive(_state);
    }
}