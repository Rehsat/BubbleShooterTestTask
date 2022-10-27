using UnityEngine;
using System;

public class PlayerTouchDetector : MonoBehaviour
{
    private Vector3 _lastTouchPosition;
    private Camera _mainCamera;


    public Action<Vector3> OnTouchFineshed;
    public Action<Vector3> OnLastTouchPositionChanged;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            _lastTouchPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _lastTouchPosition.z = 0f;
            OnLastTouchPositionChanged?.Invoke(_lastTouchPosition);
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnTouchFineshed?.Invoke(_lastTouchPosition);
        }
    }
}
