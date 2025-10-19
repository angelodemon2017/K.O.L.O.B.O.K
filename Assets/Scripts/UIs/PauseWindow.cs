using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : UIWindowBase<PauseWindowModel>
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _exitButton;

    public Action OnContinue;
    public Action OnSetting;
    public Action OnExit;

    public override void Show()
    {
        base.Show();
        _continueButton.onClick.AddListener(OnContinueButtonClicked);
        _settingButton.onClick.AddListener(OnSettingButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnContinueButtonClicked()
    {
        OnContinue?.Invoke();
//        Time.timeScale = 1f;
    }

    private void OnSettingButtonClicked()
    {
        OnSetting?.Invoke();
        Debug.Log("Settings button clicked");
    }

    private void OnExitButtonClicked()
    {
        OnExit?.Invoke();
        Debug.Log("Exit button clicked");
//        Time.timeScale = 1f;
    }

    public override void Hide()
    {
        base.Hide();
        _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        _settingButton.onClick.RemoveListener(OnSettingButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }
}