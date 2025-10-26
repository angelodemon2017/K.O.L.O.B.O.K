using System.Collections.Generic;
using UnityEngine;

public class ObjectSetPosition : StepComponentBase
{
    [SerializeField] private List<PareGoTarget> _pares;

    public override void Execute()
    {
        _pares.ForEach(p => p.Execute());
    }

    [System.Serializable]
    public class PareGoTarget
    {
        [SerializeField] private GameObject _go;
        [SerializeField] private Transform _target;

        public void Execute()
        {
            _go.transform.position = _target.position;
        }
    }
}