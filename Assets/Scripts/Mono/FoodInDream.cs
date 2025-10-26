using UnityEngine;

public class FoodInDream : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private float _saturation = 1f;
    [SerializeField] private Collider _collider;

    public int Level => _level;
    public float Saturation => _saturation;

    private void OnValidate()
    {
        if(_collider == null)
            _collider = GetComponent<Collider>();
    }

    private void Awake()
    {
        HungryDreamLevel.OnLevelChanged += CheckAvailables;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Dicts.Tags.Player)
        {
            TryEating();
        }
    }

    private void TryEating()
    {
        HungryDreamLevel.Instance.AddFood(this);
    }

    private void CheckAvailables(int val)
    {
        _collider.isTrigger = val >= _level;
    }

    private void OnDestroy()
    {
        HungryDreamLevel.OnLevelChanged -= CheckAvailables;
    }
}