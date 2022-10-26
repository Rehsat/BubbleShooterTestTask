using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    private bool _isMoving;
    private Rigidbody2D myRigidBody;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }
    public void StartMove(Vector3 direction)
    {
        myRigidBody.AddForce(direction * _speed);

        Debug.Log(direction);
    }
    public void StopMoving()
    {
        myRigidBody.velocity = Vector2.zero;
    }
}
