using UnityEngine;
using UnityEngine.Events;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _unityEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Dicts.Tags.Player)
        {
            _unityEvent?.Invoke();
        }
    }
}