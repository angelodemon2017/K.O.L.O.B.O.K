using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : UIWindowBase<MainMenuWindowModel>
{
    [SerializeField] private Button _newGameButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _exitButton;

    public Action OnNewGame;
    public Action OnSetting;
    public Action OnExit;

    public override void Show()
    {
        base.Show();
        _newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        _settingButton.onClick.AddListener(OnSettingButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnNewGameButtonClicked()
    {
        OnNewGame?.Invoke();
        Time.timeScale = 1f;
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
        Time.timeScale = 1f;
    }

    public override void Hide()
    {
        base.Hide();
        _newGameButton.onClick.RemoveListener(OnNewGameButtonClicked);
        _settingButton.onClick.RemoveListener(OnSettingButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
    }
}