using Zenject;
using UnityEngine;

public class BunnyLevel : BaseMonoLevel
{
    [SerializeField] private BunnyController _bunnyController;

    [Header("Start")]
    [SerializeField] private BaseDialogConfig _startDialog;
    [SerializeField] private GameObject _dialogTrigger;

    [Header("Start race")]
    [SerializeField] private GameObject _startRaceTrigger;

    [Header("Middle race")]
    [SerializeField] private BaseDialogConfig _middleRaceDialog;
    [SerializeField] private GameObject _middleOfRaceTrigger;

    [Header("Space")]
    [SerializeField] private CosmoController _cosmoController;

    [Header("Camp race")]
    [SerializeField] private GameObject _campTrigger;
    [SerializeField] private Transform _pointOfColobokAfterSpace;

    private bool _isCosmos = false;
    private SignalBus _signalBus;
    private DialogWindowModel _dialogWindowModel;

    public override Inputer GetInputer =>
        _isCosmos ?
        _cosmoController :
        base.GetInputer;

    [Inject]
    private void Constructor(
        SignalBus signalBus,
        DialogWindowModel dialogWindowModel)
    {
        _signalBus = signalBus;
        _dialogWindowModel = dialogWindowModel;

        _signalBus.Subscribe<AppCosmosSignal>(Handle);
    }

    public override void StartLevel(int checkPoint = 0)
    {
        base.StartLevel(checkPoint);
        RunPart(checkPoint);
    }

    private void RunPart(int checkPoint = 0)
    {
        switch (checkPoint)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    public void TriggerFirstDialog()
    {
        _dialogTrigger.SetActive(false);
        _dialogWindowModel.SetDialogEntity(_startDialog, EndOfDialog);
        _signalBus.Fire(new AppDialogSignal());
    }

    private void EndOfDialog()
    {
        //rabbit go to start
        _signalBus.Fire(new AppGameplaySignal());
        _startRaceTrigger.SetActive(true);
        _bunnyController.SetTarget(_startRaceTrigger.transform);
    }

    public void TriggerStartRace()
    {
        _startRaceTrigger.SetActive(false);
        _middleOfRaceTrigger.SetActive(true);
        _bunnyController.SetTarget(_middleOfRaceTrigger.transform);
    }

    public void TriggerMiddleRacing()
    {
        ChangeCheckpoint?.Invoke(1);
        _middleOfRaceTrigger.SetActive(false);
        _dialogWindowModel.SetDialogEntity(_middleRaceDialog, StepAfterDialogMiddleRace);
        _signalBus.Fire(new AppDialogSignal());
    }

    private void StepAfterDialogMiddleRace()
    {
        //launch music
        //_signalBus.Fire(new AppGameplaySignal());
        _signalBus.Fire(new AppLaunchToCosmosSignal());
        _campTrigger.SetActive(true);
        _bunnyController.SetTarget(_campTrigger.transform);
        _isCosmos = true;
    }

    private void Handle(AppCosmosSignal appCosmos)
    {
        EnterToCosmos();
    }

    private void EnterToCosmos()
    {
        _cosmoController.gameObject.SetActive(true);
        simpleBallController.gameObject.SetActive(false);
    }

    public void ExitFromCosmos()
    {
        Debug.Log("ExitFromCosmos");
        _isCosmos = false;
        ChangeCheckpoint?.Invoke(2);
        _cosmoController.gameObject.SetActive(false);
        simpleBallController.gameObject.SetActive(true);
        _signalBus.Fire(new AppGameplaySignal());
        simpleBallController.transform.position = _pointOfColobokAfterSpace.position;
    }

    public void DebugTest()
    {
        StepAfterDialogMiddleRace();
    }

    public void TriggerCamp()
    {
        _campTrigger.SetActive(false);
    }

    public void CallLoadNextLevel()
    {
        _signalBus.Unsubscribe<AppCosmosSignal>(Handle);
    }
}