using Zenject;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;

    [Inject] private PauseController _pauseController;
    private Vector3 _velocityBeforePause;

    private bool _isMoving;
    private Rigidbody2D _myRigidBody;

    private void Awake()
    {
        _myRigidBody = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        _pauseController.OnPauseStateChanged += HandlePause;
    }
    public void StartMove(Vector3 direction)
    {
        if (_isMoving) return;
        _myRigidBody.AddForce(direction * _speed);
    }
    public void StopMoving()
    {
        _myRigidBody.velocity = Vector2.zero;
    }
    private void HandlePause(bool pauseState)
    {
        if(pauseState == true)
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
        _pauseController.OnPauseStateChanged -= HandlePause;
    }
}
