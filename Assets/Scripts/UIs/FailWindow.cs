﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class FailWindow : UIWindowBase<FailWindowModel>
{
    [SerializeField] private Button _repeatButton;
    [SerializeField] private Button _exitButton;

    public Action OnExit;

    public override void Show()
    {
        base.Show();
        _repeatButton.onClick.AddListener(OnRepeatButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnRepeatButtonClicked()
    {
        _model.Repeat();
    }

    private void OnExitButtonClicked()
    {
        OnExit?.Invoke();
        Debug.Log("Exit button clicked");
    }

    public override void Hide()
    {
        base.Hide();
        _repeatButton.onClick.RemoveListener(OnRepeatButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        _canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}