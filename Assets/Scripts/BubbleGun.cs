using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerTouchDetector))]
[RequireComponent(typeof(RandomBubbleGenerator))]
public class BubbleGun : MonoBehaviour
{
    [SerializeField] private float _secondsToGenerateBubble;

    [SerializeField] private BubbleColorType[] _projectilesQueue;
    [SerializeField] private ProjectileBubble _projectileBubblePrefab;
    [SerializeField] private ProjectileBubble _projectileBubble;
    

    private PlayerTouchDetector _playerTouchDetector;
    private IBubblesGenerator _bubbleGenerator;
    private void Awake()
    {
        _playerTouchDetector = GetComponent<PlayerTouchDetector>();
        _bubbleGenerator = GetComponent<IBubblesGenerator>();
    }
    private void OnEnable()
    {

        _playerTouchDetector.OnTouchDetected += TryThrowBubble;
    }
    private void TryThrowBubble(Vector3 playerTouchPosition)
    {
        if (_projectileBubble == null) return;
        var moveDirection = playerTouchPosition - transform.position;
        _projectileBubble.StartMoving(moveDirection.normalized);
        _projectileBubble = null;
        StartCoroutine(GeneratingBubble());
    }
    private IEnumerator GeneratingBubble()
    {
        yield return new WaitForSeconds(_secondsToGenerateBubble);
        var newBubbleColor = _bubbleGenerator.GenerateBubble();

        var newBubble = Instantiate(_projectileBubblePrefab, transform.position, Quaternion.identity);
        newBubble.BubbleColor = newBubbleColor;
        _projectileBubble= newBubble;
    }
    private void OnDisable()
    {
        _playerTouchDetector.OnTouchDetected -= TryThrowBubble;
    }
}
