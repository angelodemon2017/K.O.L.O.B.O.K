using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    [SerializeField] private KeyCode _escapeKey = KeyCode.Escape;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField] private KeyCode _forceKey = KeyCode.F;

    private Vector2 _axises;
    private Vector2 _mouseAxises;

    public Vector2 Axises => _axises;
    public Vector2 MouseAxises => _mouseAxises;

    public Action EscapeAction;
    public Action SpaceAction;
    public Action InterAction;
    public Action ForceAction;

    private void Update()
    {
        if (Input.GetKeyDown(_jumpKey))
        {
            SpaceAction?.Invoke();
        }
        if (Input.GetKeyDown(_interactKey))
        {
            InterAction?.Invoke();
        }
        if (Input.GetKeyDown(_escapeKey))
        {
            EscapeAction?.Invoke();
        }
        if (Input.GetKeyDown(_forceKey))
        {
            ForceAction?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        _axises = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _mouseAxises = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}