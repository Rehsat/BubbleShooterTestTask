using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerTouchDetector))]
public class BubbleGun : MonoBehaviour
{
    [SerializeField] private BubbleColorType[] _projectilesQueue;
    [SerializeField] private ProjectileBubble _projectileBubble;
    private PlayerTouchDetector _playerTouchDetector;

    private void Awake()
    {
        _playerTouchDetector = GetComponent<PlayerTouchDetector>();
    }
    private void OnEnable()
    {

        _playerTouchDetector.OnTouchDetected += TryThrowBubble;
    }
    private void TryThrowBubble(Vector3 playerTouchPosition)
    {
        var moveDirection = playerTouchPosition - transform.position;
        _projectileBubble.StartMoving(moveDirection.normalized);
    }
    private void OnDisable()
    {

        _playerTouchDetector.OnTouchDetected -= TryThrowBubble;
    }
}
