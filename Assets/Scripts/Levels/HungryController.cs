using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HungryController : BaseMonoLevel
{
    public static HungryController Instance { get; private set; }

    [SerializeField] private float _hungry;
    [SerializeField] private float _finalHungry;
    [SerializeField] private Transform _scaleTarget;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _minPos;
    [SerializeField] private Transform _maxPos;
    [SerializeField] private int _level = 0;
    [SerializeField] private List<float> _borders;

    private float _baseScale = 1f;
    
    protected override string SceneByEnd => Dicts.Scenes.Bunny;
    
    public static Action<int> OnLevelChanged;
    public static Action<float> OnHungryChanged;

    private void Awake()
    {
    }

    public override void StartLevel(int checkPoint = 0)
    {
        Instance = this;
        ApplyHungry();
    }

    public void AddFood(FoodInDream foodInDream)
    {
        if (foodInDream.Saturation < _hungry + 2)
        {
            _hungry += foodInDream.Saturation;
            ApplyHungry();
            if (_hungry > _finalHungry)
            {
                _hungry = _finalHungry;
            }
            Destroy(foodInDream.gameObject);
            CheckLevel();
            OnHungryChanged?.Invoke(_hungry);
        }
    }

    private void CheckLevel()
    {
        if (_borders[_level] <= _hungry)
        {
            _level++;
            OnLevelChanged?.Invoke(_level);
        }
    }
#if UNITY_EDITOR
    [ContextMenu("CalcAllFoods")]
    private void CalcAllFoods()
    {
        var foods = FindObjectsOfType<FoodInDream>();
        _borders.Clear();
        for (int i = 0; i < 5; i++)
        {
            _borders.Add(foods.Where(f => f.Level <= i).Sum(f => f.Saturation));
        }
    }
#endif
    private void ApplyHungry()
    {
        _scaleTarget.localScale = Vector3.one * (_baseScale + _hungry);
        _camera.transform.localPosition = Vector3.Lerp(_minPos.localPosition, _maxPos.localPosition, _hungry / _finalHungry);
    }
}