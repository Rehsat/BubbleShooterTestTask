using UnityEngine;
using System;

public class PlayerTouchDetector : MonoBehaviour
{
    public Action<Vector3> OnTouchDetected;
    private Vector3 _lastTouchPosition;
    private Camera _mainCamera;
    public GameObject testSquare;
    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            _lastTouchPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _lastTouchPosition.z = 0f;
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            OnTouchDetected?.Invoke(_lastTouchPosition);
            //Instantiate(testSquare, _lastTouchPosition, Quaternion.identity);
        }
    }
}
