using System;
using UnityEngine;
using Zenject;

public class PauseController : MonoBehaviour
{
    public Action<bool> OnPauseStateChanged;

    private bool _pauseState;
    public void ChangePauseState()
    {
        ChangePauseState(!_pauseState);
    }
    public void ChangePauseState(bool newState)
    {
        _pauseState = newState;
        OnPauseStateChanged.Invoke(_pauseState);
    }
}

