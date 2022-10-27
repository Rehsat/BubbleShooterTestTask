using UnityEngine;
using System;
using Zenject;

public class PlayerTouchDetector : MonoBehaviour
{
    [Inject] private PlayerInput _playerInput;

    private Vector3 _lastTouchPosition;
    private Camera _mainCamera;

    public Action<Vector3> OnTouchFineshed;
    public Action<Vector3> OnLastTouchPositionChanged;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        _playerInput.OnTouch += UpdateLastTouchPositonInfo;
        _playerInput.OnTouchFinished += InvokeFinshTouchAction;
    }
    private void UpdateLastTouchPositonInfo()
    {
        _lastTouchPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _lastTouchPosition.z = 0f;
        OnLastTouchPositionChanged?.Invoke(_lastTouchPosition);
    }
    private void InvokeFinshTouchAction()
    {
        OnTouchFineshed?.Invoke(_lastTouchPosition);
    }
    private void OnDisable()
    {
        _playerInput.OnTouch -= UpdateLastTouchPositonInfo;
        _playerInput.OnTouchFinished -= InvokeFinshTouchAction;
    }

}
