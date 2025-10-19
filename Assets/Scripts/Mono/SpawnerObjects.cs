using System.Collections.Generic;
using UnityEngine;

public class SpawnerObjects : MonoBehaviour
{
    [SerializeField] private PhysicObject _prefab;
    [SerializeField] private float _radius = 10;
    [SerializeField] private List<PhysicObject> _cashList;
    [SerializeField] private int _maxCount = 10;
    [SerializeField] private float _minMass = 1f;
    [SerializeField] private float _maxMass = 1f;

    [ContextMenu("Spawn")]
    private void Spawn()
    {
        transform.DestroyChildrens();
        _cashList.Clear();
        for (int i = 0; i < _maxCount; i++)
        {
            var p = Instantiate(_prefab, GetRandomPosition(), Quaternion.identity, transform);
            p.SetMass(Random.Range(_minMass, _maxMass));
            _cashList.Add(p);
        }
    }

    private void OnDrawGizmos()
    {
        DrawGizmosHelper.DrawLabel(transform, 1f, "Spawner");
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-_radius, _radius), 1f, Random.Range(-_radius, _radius)) + transform.position;
    }
}