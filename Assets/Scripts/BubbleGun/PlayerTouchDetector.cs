using UnityEngine;
using System.Collections;
using System;
using Zenject;

namespace Bubbles.BubbleGun
{
    public class PlayerTouchDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask _restrictedAreaLayer;
        [Inject] private PlayerInput _playerInput;
        [Inject] private PauseController _pauseController;

        private Vector3 _lastTouchPosition;
        private Camera _mainCamera;

        public Action<Vector3> OnTouchFineshed;
        public Action<Vector3> OnLastTouchPositionChanged;

        private bool _isPaused;
        private void Awake()
        {
            _mainCamera = Camera.main;
        }
        private void OnEnable()
        {
            _playerInput.OnTouch += UpdateLastTouchPositonInfo;
            _playerInput.OnTouchFinished += InvokeFinshTouchAction;
            _pauseController.OnPauseStateChanged += ChangeIsPausedState;
        }

        private void UpdateLastTouchPositonInfo()
        {
            if (CheckThatTouchNotInRestrictedArea() && _isPaused == false)
            {
                _lastTouchPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                _lastTouchPosition.z = 0f;
                OnLastTouchPositionChanged?.Invoke(_lastTouchPosition);
            }
        }

        private bool CheckThatTouchNotInRestrictedArea()
        {
            var origin = _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            var hit = Physics2D.Raycast(origin, Vector2.zero, 10f, _restrictedAreaLayer);
            return hit.collider == null;
        }

        private void InvokeFinshTouchAction()
        {
            if (CheckThatTouchNotInRestrictedArea() && _isPaused == false)
                OnTouchFineshed?.Invoke(_lastTouchPosition);
        }
        private void ChangeIsPausedState(bool state)
        {
            if (state == false) StartCoroutine(SetIsPausedFalse());
            else _isPaused = state;
        }
        IEnumerator SetIsPausedFalse()
        {
            yield return null;
            _isPaused = false;
        }
        private void OnDisable()
        {
            _playerInput.OnTouch -= UpdateLastTouchPositonInfo;
            _playerInput.OnTouchFinished -= InvokeFinshTouchAction;
            _pauseController.OnPauseStateChanged -= ChangeIsPausedState;
        }
    }
}