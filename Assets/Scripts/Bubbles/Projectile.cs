using Zenject;
using UnityEngine;

namespace Bubbles
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private PauseController _injectedPauseController;
        public PauseController InjectedPauseController
        {
            get => _injectedPauseController;
            set
            {
                _injectedPauseController = value;
                InjectedPauseController.OnPauseStateChanged += HandlePause;
            }
        }

        private Vector3 _velocityBeforePause;

        private bool _isMoving;
        private Rigidbody2D _myRigidBody;

        private void Awake()
        {
            _myRigidBody = GetComponent<Rigidbody2D>();
        }
        public void StartMove(Vector3 direction)
        {
            if (_isMoving) return;
            _myRigidBody.AddForce(direction * _speed);
            _isMoving = true;
        }
        public void StopMoving()
        {
            _myRigidBody.velocity *= 0;
        }
        private void HandlePause(bool pauseState)
        {
            if (pauseState == true)
            {
                _velocityBeforePause = _myRigidBody.velocity;
                StopMoving();
            }
            else
            {
                _myRigidBody.velocity = _velocityBeforePause;
            }
        }
        private void OnDisable()
        {
            InjectedPauseController.OnPauseStateChanged -= HandlePause;
        }
    }
}
