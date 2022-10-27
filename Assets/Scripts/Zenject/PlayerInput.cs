using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action OnTouch;
    public Action OnTouchFinished;
    public Action OnPauseStateChange;

    private bool _pauseState;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)) OnTouch?.Invoke();
        if (Input.GetKeyUp(KeyCode.Mouse0)) OnTouchFinished?.Invoke();
    }
}
