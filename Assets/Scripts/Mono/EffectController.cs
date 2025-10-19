using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] private SimpleBallController _simpleBallController;
    [SerializeField] private Transform _pointOfDown;
    [SerializeField] private GameObject _connectGroundEffect;
    [SerializeField] private List<GameObject> _forceDownEffects;
    [SerializeField] private GameObject _poweringEffect;
    [SerializeField] private GameObject _trail;
    [SerializeField] private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _simpleBallController.GravityController.OnJumpFromGround += JumpFromGround;
        _simpleBallController.GravityController.OnGroundConnect += GroundConnect;
        _simpleBallController.GravityController.OnForceConnect += ForceDown;
        _simpleBallController.OnStartPower += PowerStart;
        _simpleBallController.OnFinishPower += PowerFinish;

        _connectGroundEffect.transform.parent = null;
        _forceDownEffects.ForEach(g => g.transform.parent = null);
    }

    private void FixedUpdate()
    {
        transform.position = _simpleBallController.transform.position;
    }

    private void GroundConnect(float velocity = 0f)
    {
        if (velocity < -2f)
        {
            ActivateEffect(_connectGroundEffect, _pointOfDown.position);
        }
        _trailRenderer.time = 4f;
    }

    private void ForceDown()
    {
        _forceDownEffects.ForEach(e => ActivateEffect(e, _pointOfDown.position));
    }

    private void PowerStart()
    {
        _poweringEffect.SetActive(true);
    }

    private void PowerFinish()
    {
        _poweringEffect.SetActive(false);
    }

    private void ActivateEffect(GameObject effectObject, Vector3 position)
    {
        effectObject.SetActive(false);
        effectObject.SetActive(true);
        effectObject.transform.position = position;
    }

    private void JumpFromGround()
    {
        _trailRenderer.time = 0f;
    }

    private void OnDestroy()
    {
        _simpleBallController.GravityController.OnJumpFromGround -= JumpFromGround;
        _simpleBallController.GravityController.OnGroundConnect -= GroundConnect;
        _simpleBallController.GravityController.OnForceConnect -= ForceDown;
        _simpleBallController.OnStartPower -= PowerStart;
        _simpleBallController.OnFinishPower -= PowerFinish;
    }
}