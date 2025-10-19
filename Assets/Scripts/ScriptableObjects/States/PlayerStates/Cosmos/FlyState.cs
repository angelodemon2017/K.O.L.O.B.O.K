using UnityEngine;

[CreateAssetMenu(menuName = "FSM/PlayerState/Cosmos/FlyState", order = 1)]
public class FlyState : BaseState
{
    [SerializeField] private float _sideSpeed = 1f;
    [SerializeField] private float _forwardSpeed = 2f;
    [SerializeField] private float _maxSpeed = 10f;
    private CosmoController _cosmoController;

    private float _currentSpeed;
    private float _positionBetweenBorders = 0.5f;

    public override void Init(IFSMRoot fSMRoot)
    {
        base.Init(fSMRoot);
        _cosmoController = fSMRoot as CosmoController;
    }

    public override void EnterState()
    {
        base.EnterState();
        _currentSpeed = _forwardSpeed;
        CheckPosAndInterpol();
    }

    public override void Run()
    {
        if (_cosmoController.XControl > 0)
        {
            _positionBetweenBorders += Time.deltaTime * _sideSpeed;
            CheckPosAndInterpol();
        }
        else if (_cosmoController.XControl < 0)
        {
            _positionBetweenBorders -= Time.deltaTime * _sideSpeed;
            CheckPosAndInterpol();
        }
        _cosmoController.transform.Translate(0f, 0f, -_currentSpeed * Time.deltaTime);
        if (_currentSpeed < _maxSpeed)
        {
            _currentSpeed += Time.deltaTime;
        }
    }

    private void CheckPosAndInterpol()
    {
        _positionBetweenBorders = Mathf.Clamp(_positionBetweenBorders, 0, 1);
        _cosmoController.SetInterpol(_positionBetweenBorders);
    }

    public override bool CheckCondition()
    {
        return Root.CurrentState.IsFinished();
    }
}