using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Zenject;

public class DialogWindow : UIWindowBase<DialogWindowModel>
{
    [SerializeField] private Image _characterAvatar;
    [SerializeField] private TextMeshProUGUI _textCharacter;
    [SerializeField] private TextMeshProUGUI _textOfDialog;
    [SerializeField] private Button _buttonSkip;
    [SerializeField] private Button _buttonContinue;

    [Inject] private CharactersConfig _charactersConfig;

    public Action OnSkip;
    public Action OnContinue;

    public override void Show()
    {
        base.Show();
        _buttonSkip.onClick.AddListener(Skip);
        _buttonContinue.onClick.AddListener(Continue);
    }

    private void Skip()
    {
        OnSkip?.Invoke();
    }

    private void Continue()
    {
        OnContinue?.Invoke();
    }

    public void Apply(DialogStepEntity dialogStep)
    {
        //_characterAvatar.sprite = //avatarService.GetAvatar(dialogStep.character)
        _textCharacter.text = dialogStep.character.ToString();
        _textOfDialog.text = dialogStep.KeyOfDialogStep;
        _textOfDialog.color = _charactersConfig.GetCharacterConfig(dialogStep.character).DebugColor;
    }

    public override void Hide()
    {
        base.Hide();
        _buttonSkip.onClick.RemoveListener(Skip);
        _buttonContinue.onClick.RemoveListener(Continue);
    }
}