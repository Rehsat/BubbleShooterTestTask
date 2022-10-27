using System;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public Action<bool> OnPauseStateChanged;

    private bool _pauseState;
    public void ChangePauseState()
    {        
        SetPauseState(!_pauseState);
    }
    public void SetPauseState(bool newState)
    {
        if (_pauseState == newState) return;
        _pauseState = newState;
        OnPauseStateChanged?.Invoke(_pauseState);
    }
}

